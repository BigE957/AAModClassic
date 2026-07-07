using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.DiscordSupport;

public class DiscordSystem : ModSystem
{
    public static DiscordSystem Instance => ModContent.GetInstance<DiscordSystem>();

    private readonly DiscordClient _discord = new();
    private readonly CancellationTokenSource _cts = new();

    private volatile DiscordActivity _currentActivity;
    public static bool IsStreaming { get; private set; }
    private long _sessionStart;
    private string _lastSentActivityJson;

    private static bool IsEnabled => false;

    public override void OnModLoad()
    {
        if(IsEnabled)
            Task.Run(() => DiscordLoopAsync(_cts.Token));
    }

    public override void OnWorldLoad()
    {
        if (IsEnabled)
        {
            _sessionStart = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            AAMod.instance.Logger.Info($"World loaded, session start timestamp: {_sessionStart}");
        }
    }

    public override void OnWorldUnload()
    {
        _currentActivity = null;
    }

    public override void OnModUnload()
    {
        if (IsEnabled)
        {
            _cts.Cancel();
            try { _discord.ClearActivityAsync().Wait(2000); } catch { }
            _discord.Dispose();
            _cts.Dispose();
        }
    }

    public override void PostUpdateEverything()
    {
        if (!IsEnabled || Main.gameMenu || !Main.PlayerLoaded)
            return;

        var player = Main.LocalPlayer;
        if (!player.active)
            return;

        if (_sessionStart == 0)
            _sessionStart = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        string currentClass = GetClassName(player);
        string worldState = NPC.downedMoonlord ? "Post-Hardmode" : Main.hardMode ? "Hardmode" : "Pre-Hardmode";

        _currentActivity = new DiscordActivity
        {
            Details = GetDetails(player),
            State = $"{player.statLife}/{player.statLifeMax2} HP  •  {Main.worldName}",
            Timestamps = new() { Start = _sessionStart },
            Assets = new()
            {
                LargeImage = "classic_icon",
                LargeText = "Ancients Awakened: Classic",
                SmallImage = currentClass.ToLower(),
                SmallText = currentClass + " - " + worldState
            },
            Party = new()
            {

            }
        };
    }

    private async Task DiscordLoopAsync(CancellationToken ct)
    {
        AAMod.instance.Logger.Info("Discord loop starting, connecting...");
        bool connected = await _discord.ConnectAsync();
        AAMod.instance.Logger.Info(connected ? "Initial connection succeeded." : "Initial connection FAILED.");

        while (!ct.IsCancellationRequested)
        {
            try
            {
                IsStreaming = await _discord.IsStreamingAsync();

                var activity = _currentActivity;
                if (activity != null)
                {
                    string newJson = JsonSerializer.Serialize(activity.ToPayload(),
                        new JsonSerializerOptions { WriteIndented = false });

                    if (newJson != _lastSentActivityJson)
                    {
                        AAMod.instance.Logger.Info($"Activity changed, sending:\n{newJson}");
                        AAMod.instance.Logger.Info("[Discord] About to call SetActivityWithResponseAsync...");
                        try
                        {
                            string response = await _discord.SetActivityWithResponseAsync(activity);
                            AAMod.instance.Logger.Info($"[Discord] Response arrived: {response}");
                            _lastSentActivityJson = newJson;
                        }
                        catch (TimeoutException)
                        {
                            AAMod.instance.Logger.Warn("[Discord] SET_ACTIVITY timed out – will retry next cycle.");
                        }
                        catch (Exception ex)
                        {
                            AAMod.instance.Logger.Error($"SetActivityAsync error: {ex.Message}");
                        }
                    }
                    else
                    {
                        AAMod.instance.Logger.Info("Activity unchanged, skipping update.");
                    }
                }
                else
                {
                    if (_lastSentActivityJson != null)
                    {
                        AAMod.instance.Logger.Info("Clearing activity.");
                        await _discord.ClearActivityAsync();
                        _lastSentActivityJson = null;
                    }
                }

                await Task.Delay(5000, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) 
            { 
                break; 
            }
            catch (Exception ex)
            {
                AAMod.instance.Logger.Error($"Loop error: {ex}");
                await Task.Delay(5000, ct);
            }
        }
    }

    private static string GetDetails(Player player)
    {
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            var npc = Main.npc[i];
            if (npc.active && (npc.type == ModContent.NPCType<RajahRabbit>() || npc.type == ModContent.NPCType<RajahRabbitA>()))
                return "Fighting Earth's Greatest Defender";

            if (npc.active && npc.boss)
                return $"Fighting {npc.FullName}";
        }

        ZAAPlayer aaPlayer = player.GetModPlayer<ZAAPlayer>();
        if (aaPlayer.ZoneInferno) return "In The Inferno";
        if (aaPlayer.ZoneMire) return "In The Mire";
        if (aaPlayer.ZoneVoid) return "Lost in The Void";
        if (aaPlayer.Terrarium) return "In The Terrarium";
        if (player.ZoneUnderworldHeight) return "In The Underworld";
        if (player.ZoneDungeon) return "In The Dungeon";
        if (player.ZoneJungle) return "Exploring The Jungle";
        if (player.ZoneHallow) return "In The Hallow";
        if (player.ZoneCrimson) return "In The Crimson";
        if (player.ZoneCorrupt) return "In The Corruption";
        if (player.ZoneSnow) return "In The Tundra";
        if (player.ZoneDesert) return "In The Desert";
        if (player.ZoneBeach) return "At The Ocean";
        if (player.ZoneSkyHeight) return "Floating in Space";
        if (player.ZoneRockLayerHeight) return "Deep Underground";
        if (player.ZoneDirtLayerHeight) return "Underground";
        return "On The Surface";
    }

    private static string GetClassName(Player player)
    {
        float melee = player.GetTotalDamage(DamageClass.Melee).Additive;
        float ranged = player.GetTotalDamage(DamageClass.Ranged).Additive;
        float magic = player.GetTotalDamage(DamageClass.Magic).Additive;
        float summon = player.GetTotalDamage(DamageClass.Summon).Additive;

        return (melee, ranged, magic, summon) switch
        {
            var (me, ra, ma, su) when me >= ra && me >= ma && me >= su => "Warrior",
            var (me, ra, ma, su) when ra >= me && ra >= ma && ra >= su => "Ranger",
            var (me, ra, ma, su) when ma >= me && ma >= ra && ma >= su => "Sorcerer",
            _ => "Summoner"
        };
    }
}