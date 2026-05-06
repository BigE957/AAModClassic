using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic.DiscordSupport;

public sealed class DiscordClient : IDisposable
{
    private const string PipePrefix = "discord-ipc-";
    private const string ClientID = "1500954635790323722";
    private static readonly string TokenCachePath = Path.Combine(Main.SavePath, "AAModClassic", "discord_token.json");

    private NamedPipeClientStream _pipe;
    private string _userId;
    private string _accessToken;

    private readonly Channel<byte[]> _outgoingFrames = Channel.CreateUnbounded<byte[]>();
    private CancellationTokenSource _writerCts;
    private Task _writerTask;

    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingRequests = new();

    private CancellationTokenSource _readerCts;
    private Task _readerTask;

    public bool IsConnected => _pipe?.IsConnected == true;

    private static readonly string[] Scopes = ["rpc"];

    public async Task<bool> ConnectAsync()
    {
        return await ConnectInternalAsync();
    }

    public async Task<bool> IsStreamingAsync()
    {
        try
        {
            await EnsureConnectedAsync();
            var response = await SendCommandAsync(new { cmd = "GET_SELECTED_VOICE_CHANNEL", args = new { } });
            LogInfo($"Voice channel data: {response}");
            return IsStreaming(response, _userId!);
        }
        catch (Exception ex)
        {
            LogWarn($"IsStreamingAsync error: {ex.Message}");
            await ReconnectAsync();
            return false;
        }
    }

    public async Task SetActivityAsync(DiscordActivity activity)
    {
        try
        {
            await EnsureConnectedAsync();
            LogInfo("Sending SET_ACTIVITY...");
            var response = await SendCommandAsync(new
            {
                cmd = "SET_ACTIVITY",
                args = new
                {
                    pid = Environment.ProcessId,
                    activity = activity.ToPayload()
                }
            });
            LogInfo($"SET_ACTIVITY response: {response}");
        }
        catch (Exception ex)
        {
            LogError($"SetActivityAsync error: {ex}");
            await ReconnectAsync();
        }
    }

    public async Task<string> SetActivityWithResponseAsync(DiscordActivity activity)
    {
        await EnsureConnectedAsync();
        return await SendCommandAsync(new
        {
            cmd = "SET_ACTIVITY",
            args = new
            {
                pid = Environment.ProcessId,
                activity = activity.ToPayload()
            }
        });
    }

    public async Task ClearActivityAsync()
    {
        try
        {
            await EnsureConnectedAsync();
            LogInfo("Sending CLEAR_ACTIVITY...");
            var response = await SendCommandAsync(new
            {
                cmd = "SET_ACTIVITY",
                args = new { pid = Environment.ProcessId, activity = (object)null }
            });
            LogInfo($"CLEAR_ACTIVITY response: {response}");
        }
        catch (Exception ex)
        {
            LogError($"ClearActivityAsync error: {ex}");
            await ReconnectAsync();
        }
    }

    public void Dispose()
    {
        StopReader();
        StopWriter();
        _pipe?.Dispose();
        foreach (var tcs in _pendingRequests.Values)
            tcs.TrySetCanceled();
        _pendingRequests.Clear();
    }

    private async Task<string> SendCommandAsync(object args)
    {
        string nonce = Guid.NewGuid().ToString();

        var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pendingRequests[nonce] = tcs;

        var payload = JsonSerializer.SerializeToNode(args)?.AsObject()
                      ?? throw new InvalidOperationException("Failed to serialize command payload.");

        payload["nonce"] = nonce;
        SendFrame(1, payload);

        using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
        try
        {
            return await tcs.Task.WaitAsync(timeoutCts.Token);
        }
        catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
        {
            throw new TimeoutException($"Command {nonce} timed out after 15 seconds. Discord received data but never replied.");
        }
        finally
        {
            _pendingRequests.TryRemove(nonce, out _);
        }
    }

    private void SendFrame(int opcode, object payload)
    {
        var frame = BuildFrame(opcode, payload);
        if (!_outgoingFrames.Writer.TryWrite(frame))
            LogWarn("[Discord] Failed to enqueue frame – channel closed?");
    }

    private void WriteFrameDirect(int opcode, object payload)
    {
        var frame = BuildFrame(opcode, payload);
        _pipe.Write(frame, 0, frame.Length);
        _pipe.Flush();
    }

    private static byte[] BuildFrame(int opcode, object payload)
    {
        string json = payload switch
        {
            System.Text.Json.Nodes.JsonNode node => node.ToJsonString(),
            not null => JsonSerializer.Serialize(payload),
            null => ""
        };

        byte[] content = !string.IsNullOrEmpty(json)
            ? Encoding.UTF8.GetBytes(json)
            : Array.Empty<byte>();

        byte[] frame = new byte[8 + content.Length];
        BitConverter.GetBytes(opcode).CopyTo(frame, 0);
        BitConverter.GetBytes(content.Length).CopyTo(frame, 4);
        if (content.Length > 0)
            content.CopyTo(frame, 8);

        return frame;
    }

    private void StartWriter()
    {
        StopWriter();
        _writerCts = new CancellationTokenSource();
        _writerTask = Task.Run(() => WriterLoop(_writerCts.Token));
    }

    private void StopWriter()
    {
        _writerCts?.Cancel();
        _writerCts?.Dispose();
        _writerCts = null;

        if (_writerTask != null)
        {
            try { _writerTask.Wait(2000); } catch { }
            _writerTask = null;
        }
    }

    private async Task WriterLoop(CancellationToken ct)
    {
        LogInfo("[Discord] Writer loop started.");
        while (await _outgoingFrames.Reader.WaitToReadAsync(ct))
        {
            while (_outgoingFrames.Reader.TryRead(out byte[] frame))
            {
                int opcode = BitConverter.ToInt32(frame, 0);
                int length = BitConverter.ToInt32(frame, 4);
                LogInfo($"[Discord] Writer sending opcode={opcode}, length={length}");

                var pipe = _pipe;
                if (pipe?.IsConnected != true)
                {
                    LogWarn("[Discord] Writer skipping frame – pipe not connected.");
                    continue;
                }

                using var writeCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                writeCts.CancelAfter(TimeSpan.FromSeconds(5));

                try
                {
                    await pipe.WriteAsync(frame, 0, frame.Length, writeCts.Token);
                    await pipe.FlushAsync(writeCts.Token);
                    LogInfo($"[Discord] Writer sent opcode={opcode} successfully.");
                }
                catch (OperationCanceledException) when (writeCts.IsCancellationRequested)
                {
                    LogWarn($"[Discord] Write timed out for opcode={opcode}. Triggering reconnect.");
                    _ = Task.Run(() => ReconnectAsync(), ct);
                    return;
                }
                catch (OperationCanceledException) { return; }
                catch (Exception ex)
                {
                    LogWarn($"[Discord] Write error: {ex.Message}");
                }
            }
        }
        LogInfo("[Discord] Writer loop ended.");
    }

    private void StartReader()
    {
        StopReader();
        _readerCts = new CancellationTokenSource();
        _readerTask = Task.Run(() => ReaderLoop(_readerCts.Token));
    }

    private void StopReader()
    {
        _readerCts?.Cancel();
        _readerCts?.Dispose();
        _readerCts = null;

        if (_readerTask != null)
        {
            try { _readerTask.Wait(2000); } catch { }
            _readerTask = null;
        }
    }

    private async Task ReaderLoop(CancellationToken ct)
    {
        LogInfo("[Discord] Reader loop started.");
        while (!ct.IsCancellationRequested)
        {
            try
            {
                if (!IsConnected)
                {
                    await Task.Delay(500, ct);
                    continue;
                }

                var (opcode, payload) = await ReadFrameAsync(ct);
                switch (opcode)
                {
                    case 1: // Command / Event
                        ProcessIncomingFrame(payload);
                        break;

                    case 3: // PING
                        LogInfo("Received PING, sending PONG directly.");
                        WriteFrameDirect(4, null);
                        break;

                    case 2: // CLOSE
                        LogWarn("Received CLOSE frame from Discord.");
                        _pipe?.Close();
                        return;

                    default:
                        LogWarn($"Received unknown opcode: {opcode}");
                        break;
                }
            }
            catch (OperationCanceledException) { return; }
            catch (Exception ex) when (!ct.IsCancellationRequested)
            {
                LogWarn($"Reader loop error: {ex.Message}");
                await Task.Delay(1000, ct);
            }
        }
    }

    private void ProcessIncomingFrame(byte[] payload)
    {
        if (payload == null || payload.Length == 0) return;

        try
        {
            string json = Encoding.UTF8.GetString(payload);
            LogInfo($"IPC in: {json}");

            string nonce = null;
            using (var doc = JsonDocument.Parse(json))
            {
                if (doc.RootElement.TryGetProperty("nonce", out var nonceEl))
                    nonce = nonceEl.GetString();
            }

            if (nonce != null && _pendingRequests.TryRemove(nonce, out var tcs))
            {
                tcs.TrySetResult(json);
                return;
            }

            LogInfo($"Unhandled IPC frame: {json}");
        }
        catch (Exception ex)
        {
            LogWarn($"Error processing frame: {ex.Message}");
        }
    }

    private async Task<(int opcode, byte[] payload)> ReadFrameAsync(CancellationToken ct)
    {
        byte[] header = new byte[8];
        await ReadExactAsync(header, 8, ct);
        int opcode = BitConverter.ToInt32(header, 0);
        int length = BitConverter.ToInt32(header, 4);
        byte[] buffer = new byte[length];
        await ReadExactAsync(buffer, length, ct);
        return (opcode, buffer);
    }

    private async Task ReadExactAsync(byte[] buffer, int count, CancellationToken ct)
    {
        int offset = 0;
        while (offset < count)
        {
            int read = await _pipe!.ReadAsync(buffer, offset, count - offset, ct);
            if (read == 0)
                throw new EndOfStreamException("Discord pipe closed.");
            offset += read;
        }
    }

    private async Task ReconnectAsync()
    {
        LogInfo("[Discord] Reconnect started...");
        StopReader();
        StopWriter();

        foreach (var tcs in _pendingRequests.Values)
            tcs.TrySetException(new InvalidOperationException("Reconnecting."));
        _pendingRequests.Clear();
        while (_outgoingFrames.Reader.TryRead(out _)) { }

        _pipe?.Dispose();
        _pipe = null;
        await Task.Delay(2000);

        try
        {
            using var timeoutCts = new CancellationTokenSource(10_000);
            bool success = await ConnectInternalAsync(timeoutCts.Token);
            if (success)
                LogInfo("[Discord] Reconnect succeeded.");
            else
                LogError("[Discord] Reconnect failed (returned false).");
        }
        catch (OperationCanceledException)
        {
            LogError("[Discord] Reconnect timed out after 10 seconds.");
            _pipe?.Dispose();
            _pipe = null;
        }
        catch (Exception ex)
        {
            Main.QueueMainThreadAction(() => LogError($"[Discord] Reconnect threw: {ex.Message}"));
            _pipe?.Dispose();
            _pipe = null;
        }
    }

    private async Task EnsureConnectedAsync()
    {
        if (!IsConnected)
            await ReconnectAsync();
    }

    private static bool IsDiscordRunning() =>
        Process.GetProcesses().Any(p => p.ProcessName.StartsWith("Discord", StringComparison.OrdinalIgnoreCase));

    private async Task<bool> ConnectInternalAsync(CancellationToken ct = default)
    {
        if (!IsDiscordRunning())
        {
            LogWarn("Discord is not running.");
            return false;
        }

        for (int i = 0; i < 10; i++)
        {
            try
            {
                _pipe?.Dispose();
                _pipe = new NamedPipeClientStream(".", PipePrefix + i, PipeDirection.InOut, PipeOptions.Asynchronous);

                using var connectCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                connectCts.CancelAfter(500);
                await _pipe.ConnectAsync(connectCts.Token);
                LogInfo("Connected to pipe: " + i);

                WriteFrameDirect(0, new { v = 1, client_id = ClientID });
                LogInfo("Handshake sent.");

                var (op, payload) = await ReadFrameAsync(ct);
                string readyJson = Encoding.UTF8.GetString(payload);
                LogInfo("Handshake response: " + readyJson);

                _userId = ParseUserId(readyJson);
                if (_userId == null)
                {
                    LogWarn("Failed to parse user id from handshake.");
                    continue;
                }
                LogInfo("Parsed user id: " + _userId);

                _accessToken = await AuthenticateAsync();
                if (_accessToken != null)
                {
                    LogInfo("Authentication successful.");
                    StartWriter();
                    StartReader();
                    return true;
                }

                LogWarn("Authentication failed for pipe " + i);
            }
            catch (OperationCanceledException)
            {
                LogWarn($"Pipe {i} connection timed out.");
            }
            catch (Exception ex)
            {
                LogWarn($"Pipe {i} error: {ex.Message}");
            }
        }
        LogWarn("All pipe connection attempts failed.");
        return false;
    }

    private async Task<string> AuthenticateAsync()
    {
        var cached = LoadCachedToken();
        if (cached != null)
        {
            LogInfo("Attempting cached token...");
            string authNonce = Guid.NewGuid().ToString();
            WriteFrameDirect(1, new { cmd = "AUTHENTICATE", args = new { access_token = cached.AccessToken }, nonce = authNonce });

            using var authTimeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            try
            {
                while (true)
                {
                    var (_, payload) = await ReadFrameAsync(authTimeoutCts.Token);
                    string json = Encoding.UTF8.GetString(payload);
                    using var doc = JsonDocument.Parse(json);

                    if (doc.RootElement.TryGetProperty("nonce", out var nonceEl) &&
                        nonceEl.GetString() == authNonce)
                    {
                        LogInfo($"Cache auth response: {json}");
                        if (!json.Contains("\"ERROR\""))
                        {
                            LogInfo("Cached token accepted.");
                            return cached.AccessToken;
                        }
                        LogWarn("Cached token was rejected.");
                        break;
                    }
                    else
                    {
                        LogInfo($"Discarding unexpected frame during auth: {json}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                LogWarn("Cache auth read timed out after 15 seconds.");
                return null;
            }
            catch (Exception ex)
            {
                LogWarn($"Cache auth read error: {ex.Message}");
                return null;
            }

            string refreshed = await RefreshTokenAsync(cached.RefreshToken);
            if (refreshed != null)
            {
                LogInfo("Token refreshed successfully.");
                return refreshed;
            }
            LogWarn("Token refresh failed.");
        }

        string codeVerifier = GenerateCodeVerifier();
        string codeChallenge = GenerateCodeChallenge(codeVerifier);

        WriteFrameDirect(1, new { cmd = "AUTHORIZE", args = new { client_id = ClientID, scopes = Scopes, prompt = "none", code_challenge = codeChallenge, code_challenge_method = "S256" }, nonce = Guid.NewGuid().ToString() });

        using (var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(15)))
        {
            try
            {
                var (_, authPayload) = await ReadFrameAsync(timeoutCts.Token);
                string authJson = Encoding.UTF8.GetString(authPayload);
                LogInfo($"AUTHORIZE response: {authJson}");
                string code;
                try
                {
                    using var doc = JsonDocument.Parse(authJson);
                    code = doc.RootElement.GetProperty("data").GetProperty("code").GetString();
                }
                catch
                {
                    LogError("Could not extract code from AUTHORIZE response.");
                    return null;
                }
                if (code == null) return null;

                string token = await ExchangeCodeAsync(code, codeVerifier);
                if (token == null) { LogError("Token exchange failed."); return null; }

                WriteFrameDirect(1, new { cmd = "AUTHENTICATE", args = new { access_token = token }, nonce = Guid.NewGuid().ToString() });
                var (_, finalPayload) = await ReadFrameAsync(timeoutCts.Token);
                string finalAuth = Encoding.UTF8.GetString(finalPayload);
                LogInfo($"Final AUTHENTICATE response: {finalAuth}");
                return token;
            }
            catch (OperationCanceledException)
            {
                LogWarn("AUTHORIZE or AUTHENTICATE read timed out after 15 seconds.");
                return null;
            }
            catch (Exception ex)
            {
                LogWarn($"Authorization error: {ex.Message}");
                return null;
            }
        }
    }

    private static async Task<string> ExchangeCodeAsync(string code, string verifier)
    {
        try
        {
            using var http = new HttpClient();
            var resp = await http.PostAsync("https://discord.com/api/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = ClientID,
                    ["grant_type"] = "authorization_code",
                    ["code"] = code,
                    ["redirect_uri"] = "http://127.0.0.1",
                    ["code_verifier"] = verifier
                }));
            using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var root = doc.RootElement;
            string access = root.GetProperty("access_token").GetString();
            string refresh = root.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : null;
            int expires = root.TryGetProperty("expires_in", out var exp) ? exp.GetInt32() : 604800;
            if (access != null) SaveToken(access, refresh, expires);
            return access;
        }
        catch { return null; }
    }

    private static async Task<string> RefreshTokenAsync(string refreshToken)
    {
        if (refreshToken == null) return null;
        try
        {
            using var http = new HttpClient();
            var resp = await http.PostAsync("https://discord.com/api/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = ClientID,
                    ["grant_type"] = "refresh_token",
                    ["refresh_token"] = refreshToken
                }));
            using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var root = doc.RootElement;
            string access = root.GetProperty("access_token").GetString();
            string refresh = root.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : refreshToken;
            int expires = root.TryGetProperty("expires_in", out var exp) ? exp.GetInt32() : 604800;
            if (access != null) SaveToken(access, refresh, expires);
            return access;
        }
        catch { return null; }
    }

    private static string ParseUserId(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("data", out var data) && data.TryGetProperty("user", out var user) && user.TryGetProperty("id", out var id))
                return id.GetString();
        }
        catch { }
        return null;
    }

    private static bool IsStreaming(string json, string userId)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("data", out var data) || data.ValueKind == JsonValueKind.Null) return false;
            if (!data.TryGetProperty("voice_states", out var states)) return false;
            foreach (var state in states.EnumerateArray())
                if (state.TryGetProperty("user", out var user) && user.TryGetProperty("id", out var id) && id.GetString() == userId)
                    return state.TryGetProperty("self_stream", out var s) && s.GetBoolean();
        }
        catch { }
        return false;
    }

    private static string GenerateCodeVerifier() => Base64UrlEncode(RandomNumberGenerator.GetBytes(32));
    private static string GenerateCodeChallenge(string verifier) => Base64UrlEncode(SHA256.HashData(Encoding.ASCII.GetBytes(verifier)));
    private static string Base64UrlEncode(byte[] data) => Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    private record TokenCache(string AccessToken, string RefreshToken);

    private static void SaveToken(string access, string refresh, int expiresIn)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(TokenCachePath)!);
        File.WriteAllText(TokenCachePath, JsonSerializer.Serialize(new { access_token = access, refresh_token = refresh, expires_at = DateTime.UtcNow.AddSeconds(expiresIn) }));
    }

    private static TokenCache LoadCachedToken()
    {
        try
        {
            if (!File.Exists(TokenCachePath)) return null;
            using var doc = JsonDocument.Parse(File.ReadAllText(TokenCachePath));
            var root = doc.RootElement;
            if (root.GetProperty("expires_at").GetDateTime() < DateTime.UtcNow.AddMinutes(5)) return null;
            return new TokenCache(root.GetProperty("access_token").GetString()!, root.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : null);
        }
        catch { return null; }
    }

    private static void LogInfo(string msg) => AAMod.instance?.Logger?.Info(msg);
    private static void LogWarn(string msg) => AAMod.instance?.Logger?.Warn(msg);
    private static void LogError(string msg) => AAMod.instance?.Logger?.Error(msg);
}