using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.WorldGen;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    public class Oblivion : ModNPC
    {
        private static string steamPath = null;
        private static string accountID = null;
        private static string localConfigPath = null;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oblivion");
            Main.npcFrameCount[NPC.type] = 14;

            InitializeSteamSearch();
        }

        private static bool InitializeSteamSearch()
        {
            if (!SteamAPI.IsSteamRunning())
                return false;

            steamPath = null;
            if (OperatingSystem.IsWindows())
                steamPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null) as string;
            else if (OperatingSystem.IsMacOS())
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library/Application Support/Steam");
            else if (OperatingSystem.IsLinux())
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/Steam");
            else
                return false;

            accountID = SteamUser.GetSteamID().GetAccountID().ToString();

            if (!string.IsNullOrEmpty(steamPath) && !string.IsNullOrEmpty(accountID))
            {
                localConfigPath = Path.Combine(steamPath, "userdata", accountID, "config", "localconfig.vdf");
                return true;
            }
            return false;
        }

        public override void SetDefaults()
        {
            NPC.width = 1;
            NPC.height = 1;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            Music = MusicManagementSystem.MusicSlots["IZDeath"];
            NPC.boss = true;
            OblivionSpeech = 0;

            if (localConfigPath == null)
                InitializeSteamSearch();
        }

        private static int OblivionSpeech = 0;
        private static int SpeechRand = 0;

        public override void AI()
        {
            Color color1 = Color.DarkRed;
            NPC.velocity.X = 0;
            NPC.velocity.Y = 0;
            Player player = Main.LocalPlayer;
            OblivionSpeech++;

            switch(AAPlayer.IZKills)
            {
                case 1:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.1"), color1);
                            break;
                        case 360:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.2"), color1);
                            break;
                        case 540:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.3"), color1);
                            break;
                        case 720:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.4"), color1);
                            break;
                        case 900:
                            if (player.difficulty == 2)
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.5.Hardcore"), color1);
                            else
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.5.Normal"), color1);
                            break;
                        case 1080:
                            if (player.difficulty == 2)
                            {
                                if (IsPlayerStreaming())
                                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Hardcore.Streaming"), color1);
                                else
                                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Hardcore.Normal", Environment.UserName), color1);
                                Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, ModContent.ItemType<Sticker>());
                            }
                            else if(IsPlayerStreaming())
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Streaming"), color1);
                            else
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Normal", Environment.UserName), color1);
                            break;
                        case 1260:
                            if (player.difficulty == 2)
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.7.Hardcore"), color1);
                            else
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.7.Normal"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 1420)
                        NPC.alpha += 5;
                    break;
                case 2:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.1"), color1);
                            break;
                        case 360:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.2"), color1);
                            break;
                        case 540:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.3"), color1);
                            break;
                        case 720:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.4"), color1);
                            break;
                        case 900:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.5"), color1);
                            break;
                        case 1080:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.6"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 1080)
                        NPC.alpha += 5;
                    break;
                case 3:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.1"), color1);
                            break;
                        case 360:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.2"), color1);
                            break;
                        case 540:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.3"), color1);
                            break;
                        case 720:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.4"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 720)
                        NPC.alpha += 5;
                    break;
                case 4:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.1"), color1);
                            break;
                        case 360:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.2"), color1);
                            break;
                        case 540:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.3"), color1);
                            break;
                        case 720:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.4"), color1);
                            break;
                        case 900:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.5"), color1);
                            break;
                        case 1080:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.6"), color1);
                            break;
                        case 1260:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.7"), color1);
                            break;
                        case 1440:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.8"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 1440)
                        NPC.alpha += 5;
                    break;
                case 10:
                    switch(OblivionSpeech)
                    {
                        case 90:
                            if(player.difficulty != 2)
                                player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.Kill", player.name)), player.statLifeMax + 10, 0, false);
                            break;
                        case 180:
                            if (player.difficulty != 2)
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.1.Normal"), color1);
                            else
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.1.Hardcore"), color1);
                            break;
                        case 360:
                            if (player.difficulty != 2)
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.2.Normal"), color1);
                            else
                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.2.Hardcore"), color1);
                            break;
                        case 540:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.3"), color1);
                            break;
                    }

                    if (OblivionSpeech >= 540)
                        NPC.alpha += 5;
                    break;
                default:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.1", AAPlayer.IZKills), color1);
                            SpeechRand = Main.rand.Next(7);
                            break;
                        case 360:
                            switch(SpeechRand)
                            {
                                case 0:
                                    if (SteamAPI.IsSteamRunning())
                                    {
                                        int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
                                        if(friendCount <= 0)
                                        {
                                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.0.Friends.Friendless"), color1);
                                            break;
                                        }

                                        List<string> onlineFriends = [];
                                        for (int i = 0; i < friendCount; i++)
                                        {
                                            CSteamID friendID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);

                                            // Get the friend's current status
                                            EPersonaState state = SteamFriends.GetFriendPersonaState(friendID);

                                            // Check if they are explicitly "Online" (State 1)
                                            if (state == EPersonaState.k_EPersonaStateOnline)
                                                onlineFriends.Add(SteamFriends.GetFriendPersonaName(friendID));
                                        }

                                        if (onlineFriends.Count > 0)
                                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.0.Friends.Online", onlineFriends[Main.rand.Next(onlineFriends.Count)]), color1);
                                        else
                                        {
                                            CSteamID randomFriend = SteamFriends.GetFriendByIndex(Main.rand.Next(friendCount), EFriendFlags.k_EFriendFlagImmediate);
                                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.0.Friends.Offline", SteamFriends.GetFriendPersonaName(randomFriend)), color1);
                                        }
                                    }
                                    else
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2." + SpeechRand), color1);
                                    break;
                                case 1:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.1.Job"), color1);
                                        Platform.Get<IPathService>().OpenURL("https://www.linkedin.com/jobs");
                                    }
                                    else
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.1.Default"), color1);
                                    break;
                                case 2:
                                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.2.1"), color1);
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        string dialogue = GetSteamGameDialogue();
                                        if (dialogue != null)
                                            Main.NewText(dialogue, color1);
                                    }
                                    break;
                                case 3:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.3.Computer", Environment.MachineName), color1);
                                    else
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.1.Default"), color1);
                                    break;
                                case 4:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        MetaPopup.TriggerSystemError(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Notification"));
                                        NPC.active = false;
                                    }
                                    //SendSystemPopup(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Notification.Title"), Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Notification.Message"));
                                    else
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Default"), color1);
                                    break;
                                case 5:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        var (text, status) = GetDiscordContext();
                                        switch(status)
                                        {
                                            case DiscordStatus.None:
                                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Default"), color1);
                                                break;
                                            case DiscordStatus.DirectMessage:
                                                Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Discord.DM", text), color1);
                                                break;
                                            case DiscordStatus.Server:
                                                switch(text)
                                                {
                                                    case "Calamity Dev Server":
                                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Discord.Server.CalDev"), color1);
                                                        break;
                                                    default:
                                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Discord.Server.Default", text), color1);
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                        Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Default"), color1);
                                    break;
                                case 6:
                                    Main.NewText(GetCrossModDialogue(), color1);
                                    break;
                                default:
                                    Main.NewText(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2." + SpeechRand), color1);
                                    break;
                            }
                            break;
                    }
                    if (OblivionSpeech >= 360)
                    {
                        NPC.alpha += 5;
                    }
                    break;
            }
            if (NPC.alpha >= 255)
            {
                NPC.active = false;
            }
        }

        private enum DiscordStatus
        {
            None,
            DirectMessage,
            Server
        }
        private static (string text, DiscordStatus status) GetDiscordContext()
        {
            var discordProcs = Process.GetProcesses();

            string discordInfo = null;

            foreach (var proc in discordProcs)
            {
                string title = proc.MainWindowTitle;

                if (!string.IsNullOrEmpty(title) && title.Contains(" - Discord"))
                {
                    discordInfo = title.Replace(" - Discord", "");
                    break;
                }
            }

            if (discordInfo == null)
                return (null, DiscordStatus.None);

            if (discordInfo.Contains('@'))
                return (discordInfo.Replace("@", ""), DiscordStatus.DirectMessage);
            else
                return (discordInfo.Remove(0, discordInfo.IndexOf('|') + 2), DiscordStatus.Server);
        }

        private static void SendNotification(string title, string message)
        {
            // Only run on Windows to avoid crashing Mac/Linux players
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            // This PowerShell script creates a basic Windows Toast notification
            string psCommand = $"-Command \"& {{ " +
                "Add-Type -AssemblyName System.Windows.Forms; " +
                "Add-Type -AssemblyName System.Drawing; " +
                "$notify = New-Object System.Windows.Forms.NotifyIcon; " +
                "$notify.Icon = [System.Drawing.SystemIcons]::Warning; " +
                "$notify.Visible = $true; " +
                $"$notify.ShowBalloonTip(5000, '{title}', '{message}', [System.Windows.Forms.ToolTipIcon]::Warning); " +
                "Start-Sleep -s 6; " + // Wait for it to display
                "$notify.Dispose(); " +
                "}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = psCommand,
                CreateNoWindow = true,      // Keep it invisible to the player
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(psi);
        }

        private static bool IsPlayerStreaming()
        {
            // List of common streaming executables DDLC checks for
            string[] streamApps = { "obs", "obs64", "obs32", "xsplit.core", "livehime" };

            // Get all running processes and check if any match our list
            var runningProcesses = Process.GetProcesses();
            return runningProcesses.Any(p => streamApps.Contains(p.ProcessName.ToLower()));
        }

        private static readonly HashSet<string> ExcludedAppIDs = [
            "105600", "1281930", // Terraria & tMod
            "228980",            // Steamworks Common Redistributables
            "250820",            // SteamVR
            "1113010",           // Steam Networking
            "1150650",           // Steam Cloud (often appears as an app)
            "41300",             // Steam Dedicated Server
            "232250"             // Steam VR Room
        ];

        private static readonly Dictionary<string, string> SpecialGameKeys = new() {
            { "620", "Portal2" },
            { "413150", "StardewValley" },
            { "367520", "HollowKnight" },
            { "219740", "DontStarve" },
            { "632360", "RoR2" },
            { "250900", "TBoI" },
            { "391540", "Undertale" },
            { "1671210", "Deltarune" },
            { "504230", "Celeste" },
            { "264710", "Subnautica" },
            { "1229490", "Ultrakill" },
            { "588650", "DeadCells" },
            { "548430", "DeepRock" },
            { "698780", "DDLC" },
            { "1966720", "LethalCompany" },
            { "242760", "TheStanleyParable" },
            { "1996550", "Bonelords" },
            { "3176850", "SecondStellar" }
        };

        private static string GetSteamGameDialogue()
        {
            if ((!string.IsNullOrEmpty(steamPath) && File.Exists(localConfigPath)) || InitializeSteamSearch())
            {
                try
                {
                    string vdfContent = File.ReadAllText(localConfigPath);
                    var playHistory = new Dictionary<string, long>();

                    // Fixed: Use capture groups [1] and [2]
                    var matches = Regex.Matches(vdfContent, @"\""(\d+)\""\s*\{\s*[^}]*\""LastPlayed\""\s*\""(\d+)\""");

                    foreach (Match match in matches)
                    {
                        string appId = match.Groups[1].Value;
                        if (long.TryParse(match.Groups[2].Value, out long lastPlayed))
                        {
                            if (!ExcludedAppIDs.Contains(appId))
                                playHistory[appId] = lastPlayed;
                        }
                    }

                    var recentGames = playHistory.OrderByDescending(x => x.Value).Take(10).ToList();
                    string locPath = "Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.2.Games.";

                    if (recentGames.Count > 0)
                    {
                        var randomGame = recentGames[Main.rand.Next(recentGames.Count)];

                        if (SpecialGameKeys.TryGetValue(randomGame.Key, out string keySuffix))
                        {
                            switch (keySuffix)
                            {
                                case "Undertale":
                                    GenocideState state = GetUndertaleGenocideState();
                                    return state switch
                                    {
                                        GenocideState.Erased => Language.GetTextValue(locPath + keySuffix + ".Erased"),
                                        GenocideState.Sold => Language.GetTextValue(locPath + keySuffix + ".Sold"),
                                        _ => Language.GetTextValue(locPath + keySuffix + ".Normal"),
                                    };
                                case "SecondStellar":
                                    if (ModLoader.HasMod("StarsAbove"))
                                        return Language.GetTextValue(locPath + keySuffix + ".TsaEnabled");
                                    else
                                        return Language.GetTextValue(locPath + keySuffix + ".Normal");
                                default:
                                    return Language.GetTextValue(locPath + keySuffix);
                            }
                        }

                        bool hasMinecraft = Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft"));
                        if (Main.rand.NextBool(4) && hasMinecraft)
                            return Language.GetTextValue(locPath + "Minecraft");

                        string gameName = GetGameNameFromID(GetAllLibraryPaths(steamPath), randomGame.Key);
                        if (!string.IsNullOrEmpty(gameName))
                        {
                            // Check if the game was played in the last 7 days (604,800 seconds)
                            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                            bool isRecent = (now - randomGame.Value) <= 604800;
                            string timeSuffix = isRecent ? "Recent" : "Old";

                            return Language.GetTextValue(locPath + "Default." + timeSuffix + "." + Main.rand.Next(3), gameName);
                        }
                    }
                    else if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft")))
                        return Language.GetTextValue(locPath + "Minecraft");
                }
                catch { }
            }
            return null;
        }

        private static string GetGameNameFromID(List<string> libraryPaths, string appId)
        {
            foreach (var path in libraryPaths)
            {
                string manifestPath = Path.Combine(path, $"appmanifest_{appId}.acf");
                if (File.Exists(manifestPath))
                {
                    string content = File.ReadAllText(manifestPath);
                    var match = Regex.Match(content, @"\""name\""\s*\""(.*)\""");
                    if (match.Success) return match.Groups[1].Value;
                }
            }
            return null;
        }

        private static List<string> GetAllLibraryPaths(string steamPath)
        {
            List<string> paths = [];
            paths.Add(Path.Combine(steamPath, "steamapps"));

            string vdfPath = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
            if (File.Exists(vdfPath))
            {
                string content = File.ReadAllText(vdfPath);
                var matches = Regex.Matches(content, @"\""path\""\s*\""(.*)\""");
                foreach (Match m in matches)
                {
                    string libraryPath = m.Groups[1].Value.Replace("\\\\", "\\");
                    string steamappsPath = Path.Combine(libraryPath, "steamapps");
                    if (Directory.Exists(steamappsPath) && !paths.Contains(steamappsPath))
                    {
                        paths.Add(steamappsPath);
                    }
                }
            }
            return paths;
        }

        private enum GenocideState
        {
            None,
            Erased,
            Sold
        }
        private static GenocideState GetUndertaleGenocideState()
        {
            try
            {
                string utPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UNDERTALE");
                if (Directory.Exists(utPath))
                {
                    // If either of these "stain" files exist, they've done it.
                    if (File.Exists(Path.Combine(utPath, "system_information_962")))
                        return GenocideState.Erased;
                    if(File.Exists(Path.Combine(utPath, "system_information_963")))
                        return GenocideState.Sold;

                    return GenocideState.None;
                }
            }
            catch { }
            return GenocideState.None;
        }

        public static bool AddInfinityZeroCrossmodDialogue(string key, LocalizedText text, Func<bool> condition) => CrossModDialogue.TryAdd(key, new(text, condition));

        private static string GetCrossModDialogue()
        {
            List<LocalizedText> crossModText = [];
            foreach (var (text, condition) in CrossModDialogue.Values)
            {
                if (condition.Invoke())
                    crossModText.Add(text);
            }

            if (crossModText.Count > 0)
                return crossModText[Main.rand.Next(crossModText.Count)].Value;

            return Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.Mods.NoMod");
        }

        public static readonly Dictionary<string, (LocalizedText text, Func<bool> condition)> CrossModDialogue = [];

        public static int MajorModCount()
        {
            int ModCount = 0;

            if (AAMod_Unreleased.calamityLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.thoriumLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.spiritLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.fargoLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.redemptionLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.tremorLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.sacredToolsLoaded)
            {
                ModCount++;
            }
            if (AAMod_Unreleased.grealmLoaded)
            {
                ModCount++;
            }

            return ModCount;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter < 5)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 7 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
            }
            else if (NPC.frameCounter < 10)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 8 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
            }
            else if (NPC.frameCounter < 15)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 9 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
            }
            else if (NPC.frameCounter < 20)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 10 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
            }
            else if (NPC.frameCounter < 25)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 11 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 4 * frameHeight;
                }
            }
            else if (NPC.frameCounter < 30)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 12 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 5 * frameHeight;
                }
            }
            else if (NPC.frameCounter < 35)
            {
                if (Main.rand.NextBool(9))
                {
                    NPC.frame.Y = 13 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 6 * frameHeight;
                }
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }

        public static Texture2D glowTex = null;
        public static Texture2D glitchTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (glowTex == null)
            {
                glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            }
            if (glitchTex == null)
            {
                glitchTex = ModContent.Request<Texture2D>(Texture + "_Glitch").Value;
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(NPC, NPC.Center + new Vector2(0, -30), true, 0f), drawColor));
            BaseDrawing.DrawAura(spriteBatch, glowTex, 0, NPC, auraPercent, 1f, 0f, 0f, Color.White);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, Color.White);
            BaseDrawing.DrawAura(spriteBatch, glitchTex, 0, NPC, auraPercent, 1f, 0f, 0f, AAColor.Oblivion);
            BaseDrawing.DrawTexture(spriteBatch, glitchTex, 0, NPC, AAColor.Oblivion);

            return false;
        }
    }

    internal static class MetaPopup
    {
        // Imports the standard Windows Message Box function
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        internal static void TriggerSystemError(string message)
        {
            // Only run on Windows
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;

            // 0x00000010 is the code for the "Critical Error" (Red X) icon
            // 0x00000000 is the code for a simple "OK" button
            uint MB_ICONERROR = 0x00000010;

            _ = MessageBox(IntPtr.Zero, message, "System Error", MB_ICONERROR);
        }
    }
}