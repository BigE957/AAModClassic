using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.DiscordSupport;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Core;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Humanizer;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    public class Oblivion : ModNPC, IBestiaryCritterNPC
    {
        private static string steamPath = null;
        private static string accountID = null;
        private static string localConfigPath = null;

        private static FieldInfo ChatMessageList = null;
        private static FieldInfo MessageTimeLeft = null;
        private static FieldInfo MessageColor = null;

        public bool UnlockWhenNearby => false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oblivion");
            Main.npcFrameCount[NPC.type] = 14;

            InitializeSteamSearch();

            var field = Main.chatMonitor.GetType().GetField("_messages", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                ChatMessageList = field;

            field = typeof(ChatMessageContainer).GetField("_timeLeft", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                MessageTimeLeft = field;

            field = typeof(ChatMessageContainer).GetField("_color", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
                MessageColor = field;

            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        private static bool InitializeSteamSearch()
        {
            if (OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
                return false;

            try
            {
                if (!SteamAPI.IsSteamRunning())
                    return false;
            }
            catch
            {
                return false;
            }

            steamPath = null;
            if (OperatingSystem.IsWindows())
                steamPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null) as string;
            else if (OperatingSystem.IsMacOS())
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library/Application Support/Steam");
            else if (OperatingSystem.IsLinux())
                steamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/Steam");
            else
                return false;

            try
            {
                accountID = SteamUser.GetSteamID().GetAccountID().ToString();
            }
            catch
            {
                return false;
            }

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
            NPC.lifeMax = 20;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            Music = MusicManagementSystem.MusicSlots["InfinityZero_Outro"];
            NPC.boss = true;
            OblivionSpeech = 0;

            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            Main.npcFrameCount[NPC.type] = unofficial ? 10 : 14;
            Asset<Texture2D> tex = unofficial ? ModContent.Request<Texture2D>(Texture + "_Resprite") : TextureAssets.Npc[NPC.type];
            NPC.frame.Height = tex.Height() / Main.npcFrameCount[NPC.type];
            NPC.frame.Width = tex.Width();

            if (localConfigPath == null)
                InitializeSteamSearch();

            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.RemoveAll(e => e is NPCKillCounterInfoElement || e is NPCStatsReportInfoElement);
            bestiaryEntry.Info.Add(new ColoredFlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.Oblivion", AAColor.OblivionDialogue));
        }

        private static int OblivionSpeech = 0;
        private static int SpeechRand = 0;

        public override void AI()
        {
            Color color1 = AAColor.OblivionDialogue;
            NPC.velocity.X = 0;
            NPC.velocity.Y = 0;
            Player player = Main.LocalPlayer;
            OblivionSpeech++;

            for (int i = 0; i < RateOfChange; i++)
                UpdateMessage();

            switch (ZAAPlayer.IZKills)
            {
                case 1:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.1"), color1);
                            break;
                        case 360:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.2"), color1);
                            break;
                        case 540:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.3"), color1);
                            break;
                        case 720:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.4"), color1);
                            break;
                        case 900:
                            if (player.difficulty == 2)
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.5.Hardcore"), color1);
                            else
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.5.Normal"), color1);
                            break;
                        case 1080:
                            if (player.difficulty == 2)
                            {
                                if (IsPlayerStreaming())
                                    StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Hardcore.Streaming"), color1);
                                else
                                    StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Hardcore.Normal", PlayerIdentityHelper.GetRealName()), color1);
                                Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, ModContent.ItemType<Sticker>());
                            }
                            else if(IsPlayerStreaming())
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Streaming"), color1);
                            else
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.6.Normal", PlayerIdentityHelper.GetRealName()), color1);
                            break;
                        case 1260:
                            if (player.difficulty == 2)
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.7.Hardcore"), color1);
                            else
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.First.7.Normal"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 1420)
                        NPC.alpha += 5;
                    break;
                case 2:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.1"), color1);
                            break;
                        case 360:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.2"), color1);
                            break;
                        case 540:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.3"), color1);
                            break;
                        case 720:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.4"), color1);
                            break;
                        case 900:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.5"), color1);
                            break;
                        case 1080:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Second.6"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 1080)
                        NPC.alpha += 5;
                    break;
                case 3:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.1"), color1);
                            break;
                        case 360:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.2"), color1);
                            break;
                        case 540:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.3"), color1);
                            break;
                        case 720:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Third.4"), color1);
                            break;
                    }
                    if (OblivionSpeech >= 720)
                        NPC.alpha += 5;
                    break;
                case 4:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.1"), color1);
                            break;
                        case 360:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.2"), color1);
                            break;
                        case 540:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.3"), color1);
                            break;
                        case 720:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.4"), color1);
                            break;
                        case 900:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.5"), color1);
                            break;
                        case 1080:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.6"), color1);
                            break;
                        case 1260:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.7"), color1);
                            break;
                        case 1440:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Fourth.8"), color1);
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
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.1.Normal"), color1);
                            else
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.1.Hardcore"), color1);
                            break;
                        case 360:
                            if (player.difficulty != 2)
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.2.Normal"), color1);
                            else
                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.2.Hardcore"), color1);
                            break;
                        case 540:
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Tenth.3"), color1);
                            break;
                    }

                    if (OblivionSpeech >= 540)
                        NPC.alpha += 5;
                    break;
                default:
                    switch(OblivionSpeech)
                    {
                        case 180:
                            string number = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ZAAPlayer.IZKills.ToWords());
                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.1", number), color1);
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
                                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.0.Friends.Friendless"), color1);
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
                                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.0.Friends.Online", onlineFriends[Main.rand.Next(onlineFriends.Count)]), color1);
                                        else
                                        {
                                            CSteamID randomFriend = SteamFriends.GetFriendByIndex(Main.rand.Next(friendCount), EFriendFlags.k_EFriendFlagImmediate);
                                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.0.Friends.Offline", SteamFriends.GetFriendPersonaName(randomFriend)), color1);
                                        }
                                    }
                                    else
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2." + SpeechRand), color1);
                                    break;
                                case 1:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.1.Job"), color1);
                                        Platform.Get<IPathService>().OpenURL("https://www.linkedin.com/jobs");
                                    }
                                    else
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.1.Default"), color1);
                                    break;
                                case 2:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        string dialogue = GetSteamGameDialogue();
                                        if (dialogue != null)
                                            StartMessage(dialogue, color1);
                                        else
                                            StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.2.1"), color1);
                                    }
                                    else
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.2.1"), color1);
                                    break;
                                case 3:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.3.Computer", Environment.MachineName), color1);
                                    else
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.1.Default"), color1);
                                    break;
                                case 4:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        MetaPopup.TriggerSystemError(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Notification"));
                                        NPC.active = false;
                                    }
                                    //SendSystemPopup(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Notification.Title"), Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Notification.Message"));
                                    else
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.4.Default"), color1);
                                    break;
                                case 5:
                                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                                    {
                                        var (text, status) = GetDiscordContext();
                                        switch(status)
                                        {
                                            case DiscordStatus.None:
                                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Default"), color1);
                                                break;
                                            case DiscordStatus.DirectMessage:
                                                StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Discord.DM", text), color1);
                                                break;
                                            case DiscordStatus.Server:
                                                switch(text)
                                                {
                                                    case "Calamity Dev Server":
                                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Discord.Server.CalDev"), color1);
                                                        break;
                                                    default:
                                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Discord.Server.Default", text), color1);
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                        StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2.5.Default"), color1);
                                    break;
                                case 6:
                                    StartMessage(GetCrossModDialogue(), color1);
                                    break;
                                default:
                                    StartMessage(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.InfinityZero.Defeat.Other.2." + SpeechRand), color1);
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
                Main.BestiaryTracker.Sights.RegisterWasNearby(NPC);
                NPC.active = false;
            }
        }

        private bool MessageSwapComplete => PositionsToChange.Count <= 0;
        private string CurrentMessage = "";
        private List<int> PositionsToChange = [];
        private bool firstMessage = true;
        private List<string> FirstMessageCache = [];
        private int RateOfChange = 4;

        private void StartMessage(string message, Color color)
        {
            if(!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Main.NewText(message, color);
                return;
            }

            CurrentMessage = message;
            List<ChatMessageContainer> messages = (List<ChatMessageContainer>)(ChatMessageList.GetValue(Main.chatMonitor));
            if ((int)MessageTimeLeft.GetValue(messages[0]) <= 0)
            {
                Main.NewText("".PadRight(CurrentMessage.Length), color);
                PositionsToChange = [];
                for (int i = 0; i < MathHelper.Max(CurrentMessage.Length, messages[0].OriginalText.Length); i++)
                    PositionsToChange.Add(i);
                firstMessage = false;
            }
            else
            {
                string displayed = messages[0].OriginalText;
                if (displayed.Length < CurrentMessage.Length)
                    displayed = displayed.PadRight(CurrentMessage.Length);
                PositionsToChange = [];
                RateOfChange = (int)MathHelper.Max(CurrentMessage.Length, displayed.Length) / 16;
                for (int i = 0; i < MathHelper.Max(CurrentMessage.Length, displayed.Length); i++)
                {
                    if (i < displayed.Length)
                        FirstMessageCache.Add(displayed[i].ToString());
                    else
                        FirstMessageCache.Add(" ");
                    PositionsToChange.Add(i);
                }

                messages[0].SetContents(displayed, (Color)MessageColor.GetValue(messages[0]), -1);

                ChatMessageList.SetValue(Main.chatMonitor, messages);
            }
        }

        private void UpdateMessage()
        {
            if (MessageSwapComplete)
                return;

            int posSlot = Main.rand.Next(PositionsToChange.Count);
            int messageSlot = PositionsToChange[posSlot];
            PositionsToChange.RemoveAt(posSlot);

            List<ChatMessageContainer> messages = (List<ChatMessageContainer>)ChatMessageList.GetValue(Main.chatMonitor);
            char[] arr;
            if (firstMessage)
            {
                if (messageSlot >= CurrentMessage.Length)
                    FirstMessageCache[messageSlot] = " ";
                else
                    FirstMessageCache[messageSlot] = "[C/8B0000:" + CurrentMessage[messageSlot].ToString() + "]";

                string str = "";
                foreach (string s in FirstMessageCache)
                    str += s;
                arr = str.ToCharArray();
            }
            else
            {
                arr = messages[0].OriginalText.ToCharArray();
                if (messageSlot >= CurrentMessage.Length)
                    arr[messageSlot] = ' ';
                else
                    arr[messageSlot] = CurrentMessage[messageSlot];
            }

            if (MessageSwapComplete)
            {
                messages[0].SetContents(CurrentMessage, AAColor.OblivionDialogue, -1);
                firstMessage = false;
            }
            else if (firstMessage)
            {
                messages[0].SetContents(new string(arr), (Color)MessageColor.GetValue(messages[0]), -1);
            }
            else
                messages[0].SetContents(new string(arr), AAColor.OblivionDialogue, -1);

            ChatMessageList.SetValue(Main.chatMonitor, messages);
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

        public static bool IsPlayerStreaming()
        {
            if (DiscordSystem.IsStreaming)
                return true;

            HashSet<string> streamApps = new HashSet<string>() {
                "obs", "obs64", "obs32", "xsplit.core", "livehime",
                "streamlabs desktop", "vmix64", "prism", "prismlivestudio",
                "meldstudio", "radeonsoftware", "action", "outplayed",
                "bandicam", "medal", "insightscapture", "ascent",
                "twitchstudio", "loom"
            };

            return Process.GetProcesses().Any(p => streamApps.Contains(p.ProcessName.ToLower()));
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

        public static readonly Dictionary<string, (LocalizedText text, Func<bool> condition)> CrossModDialogue = [];

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

        public override void FindFrame(int frameHeight)
        {
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            Main.npcFrameCount[NPC.type] = unofficial ? 10 : 14;
            Asset<Texture2D> tex = unofficial ? ModContent.Request<Texture2D>(Texture + "_Resprite") : TextureAssets.Npc[NPC.type];
            NPC.frame.Height = tex.Height() / Main.npcFrameCount[NPC.type];
            NPC.frame.Width = tex.Width();

            NPC.frameCounter++;
            int realFrames = unofficial ? 10 : 7;
            if (unofficial)
            {
                NPC.frame.Y = (int)((NPC.frameCounter / 5) % realFrames);
                if (Main.rand.NextBool(9))
                {
                    if (!StaticActive && Main.rand.NextBool())
                    {

                    }
                    else
                        StaticActive = !StaticActive;
                }
            }
            else
                switch ((NPC.frameCounter / 5) % realFrames)
                {
                    case 0:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 7 : 0);
                        break;
                    case 1:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 8 : 1);
                        break;
                    case 2:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 9 : 2);
                        break;
                    case 3:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 10 : 3);
                        break;
                    case 4:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 11 : 4);
                        break;
                    case 5:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 12 : 5);
                        break;
                    case 6:
                        NPC.frame.Y = (Main.rand.NextBool(9) ? 13 : 6);
                        break;
                }
            
            NPC.frame.Y *= NPC.frame.Height;
            if ((NPC.frameCounter / 5) % realFrames == 0 && NPC.frameCounter != 0)
                NPC.frameCounter = 0;
        }

        public static Texture2D glowTex = null;
        public static Texture2D glitchTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        private bool StaticActive = false;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool unofficial = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);

            if (glowTex == null)
            {
                glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            }
            if (glitchTex == null)
            {
                glitchTex = ModContent.Request<Texture2D>(Texture + "_Glitch").Value;
            }
            Texture2D tex = unofficial ? ModContent.Request<Texture2D>(Texture + "_Resprite").Value : TextureAssets.Npc[NPC.type].Value;
            Texture2D glow = unofficial ? ModContent.Request<Texture2D>(Texture + "_Resprite_Glow").Value : glowTex;

            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(NPC, NPC.Center + new Vector2(0, -30), true, 0f), drawColor) * NPC.Opacity, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            DrawingUtils.DrawAura(spriteBatch, glow, NPC, auraPercent, 1f, 0f, 0f, Color.White * NPC.Opacity, true);
            spriteBatch.Draw(glow, NPC.Center - screenPos, NPC.frame, Color.White * NPC.Opacity, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            if(!NPC.IsABestiaryIconDummy && unofficial && StaticActive)
            {
                Effect effect = Terraria.Graphics.Effects.Filters.Scene["AAModClassic:Mask"].GetShader().Shader;
                effect.Parameters["offset"].SetValue(Main.rand.NextVector2Square(0, 600));
                effect.Parameters["noiseScale"].SetValue(new Vector2(0.2f, 1f));

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect, Main.GameViewMatrix.TransformationMatrix);

                Main.instance.GraphicsDevice.Textures[1] = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/StaticNoise").Value;
                Main.instance.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;

                Texture2D mask = ModContent.Request<Texture2D>(Texture + "_Resprite_Mask").Value;

                Main.EntitySpriteDraw(mask, NPC.position - screenPos, NPC.frame, Color.White * NPC.Opacity, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
            }
            //BaseDrawing.DrawAura(spriteBatch, glitchTex, 0, NPC, auraPercent, 1f, 0f, 0f, AAColor.Oblivion);
            //spriteBatch.Draw(glitchTex, NPC.Center - screenPos, NPC.frame, AAColor.Oblivion, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            return false;
        }
    }

    internal static class PlayerIdentityHelper
    {
        // NameDisplay (3) = the user's "friendly" display name, e.g. "John Smith"
        private const int NameDisplay = 3;

        private static string _cachedName;

        [DllImport("netapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int NetUserGetInfo(string servername, string username, int level, out IntPtr bufPtr);

        [DllImport("netapi32.dll")]
        private static extern int NetApiBufferFree(IntPtr buffer);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct USER_INFO_10
        {
            public string usri10_name;
            public string usri10_comment;
            public string usri10_usr_comment;
            public string usri10_full_name; // This is what we're after
        }

        [DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetUserNameEx(int nameFormat, StringBuilder userName, ref uint userNameSize);

        public static string GetRealName()
        {
            if (ModContent.GetInstance<AAConfigClient>().HideIdentifiableInfo)
                return TryGetSteamName() ?? Main.LocalPlayer.name;

            if (_cachedName is not null)
                return _cachedName;

            _cachedName =
                TryGetDisplayName()             // secur32 display name
                ?? TryGetNetUserName()          // netapi32 full name
                ?? TryGetRegistryOwner()        // Windows install-time owner
                ?? TryGetPasswdName()           // Linux GECOS
                ?? TryGetMacOsRealName()        // macOS dscl
                ?? TryGetEnvironmentVariables() // DEBFULLNAME / GIT_AUTHOR_NAME
                ?? TryGetGitName()              // git config user.name
                ?? TryGetEnvironmentName()      // login name fallback
                ?? TryGetSteamName()            // Steam persona
                ?? Main.LocalPlayer.name
                ?? "dumb fucking bitch";

            return _cachedName;
        }

        private static string TryGetRegistryOwner()
        {
            if (!OperatingSystem.IsWindows()) return null;
            try
            {
                using var key = Microsoft.Win32.Registry.LocalMachine
                    .OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                var name = key?.GetValue("RegisteredOwner") as string;
                return string.IsNullOrWhiteSpace(name) ? null : name;
            }
            catch { return null; }
        }

        private static string TryGetDisplayName()
        {
            if (!OperatingSystem.IsWindows())
                return null;

            try
            {
                uint size = 0;
                _ = GetUserNameEx(NameDisplay, null, ref size);

                if (size == 0)
                    return null;

                var sb = new StringBuilder((int)size);
                return GetUserNameEx(NameDisplay, sb, ref size) != 0 ? sb.ToString() : null;
            }
            catch
            {
                return null;
            }
        }

        private static string TryGetNetUserName()
        {
            if (!OperatingSystem.IsWindows())
                return null;

            try
            {
                // null servername = local machine
                if (NetUserGetInfo(null, Environment.UserName, 10, out IntPtr bufPtr) == 0)
                {
                    var info = Marshal.PtrToStructure<USER_INFO_10>(bufPtr);
                    NetApiBufferFree(bufPtr); // Always free the buffer
                    return string.IsNullOrWhiteSpace(info.usri10_full_name) ? null : info.usri10_full_name;
                }
            }
            catch { }

            return null;
        }

        private static string TryGetMacOsRealName()
        {
            if (!OperatingSystem.IsMacOS()) return null;
            Process process = null;
            try
            {
                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dscl",
                        Arguments = $". -read /Users/{Environment.UserName} RealName",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                var result = process.StandardOutput.ReadToEnd().Trim();
                if (!process.WaitForExit(1000))
                    process.Kill();

                var lines = result.Split('\n');
                var name = lines.Length > 1 ? lines[1].Trim() : null;
                return string.IsNullOrWhiteSpace(name) ? null : name;
            }
            catch { return null; }
            finally { process?.Dispose(); }
        }

        private static string TryGetPasswdName()
        {
            if (!OperatingSystem.IsLinux()) return null;
            try
            {
                var username = Environment.UserName;
                foreach (var line in File.ReadAllLines("/etc/passwd"))
                {
                    var parts = line.Split(':');
                    if (parts.Length >= 5 && parts[0] == username)
                    {
                        var gecos = parts[4].Split(',')[0].Trim();
                        return string.IsNullOrWhiteSpace(gecos) ? null : gecos;
                    }
                }
            }
            catch { }
            return null;
        }

        private static string TryGetEnvironmentVariables()
        {
            var deb = Environment.GetEnvironmentVariable("DEBFULLNAME");
            if (!string.IsNullOrWhiteSpace(deb))
                return deb;

            var gitAuthor = Environment.GetEnvironmentVariable("GIT_AUTHOR_NAME");
            if (!string.IsNullOrWhiteSpace(gitAuthor))
                return gitAuthor;

            return null;
        }

        private static string TryGetGitName()
        {
            Process process = null;
            try
            {
                process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "git",
                        Arguments = "config --global user.name",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                var result = process.StandardOutput.ReadToEnd().Trim();

                if (!process.WaitForExit(1000))
                    process.Kill();

                return string.IsNullOrWhiteSpace(result) ? null : result;
            }
            catch { return null; }
            finally { process?.Dispose(); }
        }

        private static string TryGetSteamName()
        {
            try
            {
                if (!SteamAPI.IsSteamRunning())
                    return null;

                var name = SteamFriends.GetFriendPersonaName(SteamUser.GetSteamID());
                return string.IsNullOrWhiteSpace(name) ? null : name;
            }
            catch
            {
                return null;
            }
        }
    
        private static string TryGetEnvironmentName()
        {
            if(Environment.UserName.Equals("user", StringComparison.InvariantCultureIgnoreCase))
                return null;
            return Environment.UserName;
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

    public class DiscordIpcDetector
    {
        private const string PipePrefix = "discord-ipc-";
        private const string ClientID = "1500954635790323722D";

        private static readonly string TokenCachePath = Path.Combine(Main.SavePath,"AAModClassic", "discord_token.json");

        public static async Task<bool> IsUserStreamingAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using var pipe = new NamedPipeClientStream(".", PipePrefix + i, PipeDirection.InOut);
                    pipe.Connect(500);

                    SendFrame(pipe, 0, new { v = 1, client_id = ClientID });
                    string readyJson = ReadFrame(pipe);
                    string userId = ParseUserId(readyJson);
                    if (userId == null) continue;

                    string accessToken = await GetValidTokenAsync(pipe);
                    if (accessToken == null) return false;

                    SendFrame(pipe, 1, new
                    {
                        cmd = "GET_SELECTED_VOICE_CHANNEL",
                        args = new { },
                        nonce = Guid.NewGuid().ToString()
                    });

                    return IsStreaming(ReadFrame(pipe), userId);
                }
                catch { continue; }
            }

            return false;
        }

        private static async Task<string> GetValidTokenAsync(NamedPipeClientStream pipe)
        {
            // 1. Try cached token
            var cached = LoadCachedToken();
            if (cached != null)
            {
                // Try to use it
                SendFrame(pipe, 1, new
                {
                    cmd = "AUTHENTICATE",
                    args = new { access_token = cached.AccessToken },
                    nonce = Guid.NewGuid().ToString()
                });

                string result = ReadFrame(pipe);
                if (!result.Contains("\"ERROR\""))
                    return cached.AccessToken;

                // Try to refresh it silently
                string refreshed = await RefreshTokenAsync(cached.RefreshToken);
                if (refreshed != null) return refreshed;
            }

            // 2. Full PKCE flow (happens once, user sees a Discord consent popup)
            return await FullPkceFlowAsync(pipe);
        }

        private static async Task<string> FullPkceFlowAsync(NamedPipeClientStream pipe)
        {
            // Generate PKCE challenge — no secret needed, security comes from these
            string codeVerifier = GenerateCodeVerifier();
            string codeChallenge = GenerateCodeChallenge(codeVerifier);

            // AUTHORIZE — Discord shows a one-time user consent popup
            SendFrame(pipe, 1, new
            {
                cmd = "AUTHORIZE",
                args = new
                {
                    client_id = ClientID,
                    scopes = new[] { "rpc" },
                    prompt = "none",
                    code_challenge = codeChallenge,
                    code_challenge_method = "S256"
                },
                nonce = Guid.NewGuid().ToString()
            });

            string authorizeJson = ReadFrame(pipe);
            string code = null;
            try
            {
                using var doc = JsonDocument.Parse(authorizeJson);
                code = doc.RootElement.GetProperty("data").GetProperty("code").GetString();
            }
            catch { return null; }

            if (code == null) return null;

            return await ExchangeCodePkceAsync(code, codeVerifier);
        }

        private static async Task<string> ExchangeCodePkceAsync(string code, string codeVerifier)
        {
            try
            {
                using var http = new HttpClient();
                var resp = await http.PostAsync(
                    "https://discord.com/api/oauth2/token",
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["client_id"] = ClientID,
                        ["grant_type"] = "authorization_code",
                        ["code"] = code,
                        ["redirect_uri"] = "http://127.0.0.1",
                        ["code_verifier"] = codeVerifier   // <-- replaces client_secret
                    }));

                string json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                string accessToken = root.GetProperty("access_token").GetString();
                string refreshToken = root.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : null;
                int expiresIn = root.TryGetProperty("expires_in", out var exp) ? exp.GetInt32() : 604800;

                if (accessToken != null)
                    SaveToken(accessToken, refreshToken, expiresIn);

                return accessToken;
            }
            catch { return null; }
        }

        private static async Task<string> RefreshTokenAsync(string refreshToken)
        {
            if (refreshToken == null) return null;
            try
            {
                using var http = new HttpClient();
                var resp = await http.PostAsync(
                    "https://discord.com/api/oauth2/token",
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["client_id"] = ClientID,
                        ["grant_type"] = "refresh_token",
                        ["refresh_token"] = refreshToken
                        // Still no client_secret needed for PKCE-issued tokens
                    }));

                string json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                string newAccess = root.GetProperty("access_token").GetString();
                string newRefresh = root.TryGetProperty("refresh_token", out var rt)
                                         ? rt.GetString() : refreshToken;
                int expiresIn = root.TryGetProperty("expires_in", out var exp)
                                    ? exp.GetInt32() : 604800;

                if (newAccess != null)
                    SaveToken(newAccess, newRefresh, expiresIn);

                return newAccess;
            }
            catch { return null; }
        }

        // ---- PKCE Helpers ----
        private static string GenerateCodeVerifier()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(32);
            return Base64UrlEncode(bytes);
        }

        private static string GenerateCodeChallenge(string verifier)
        {
            byte[] hash = SHA256.HashData(Encoding.ASCII.GetBytes(verifier));
            return Base64UrlEncode(hash);
        }

        private static string Base64UrlEncode(byte[] data) => Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');

        // ---- Token Cache ----
        private record TokenCache(string AccessToken, string RefreshToken, DateTime ExpiresAt);

        private static void SaveToken(string access, string refresh, int expiresInSeconds)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(TokenCachePath)!);
            var cache = new
            {
                access_token = access,
                refresh_token = refresh,
                expires_at = DateTime.UtcNow.AddSeconds(expiresInSeconds)
            };
            File.WriteAllText(TokenCachePath, JsonSerializer.Serialize(cache));
        }

        private static TokenCache LoadCachedToken()
        {
            try
            {
                if (!File.Exists(TokenCachePath)) return null;
                using var doc = JsonDocument.Parse(File.ReadAllText(TokenCachePath));
                var root = doc.RootElement;

                var expiresAt = root.GetProperty("expires_at").GetDateTime();
                if (expiresAt < DateTime.UtcNow.AddMinutes(5)) // Consider expired if < 5 min left
                {
                    // Don't discard — we still have a refresh token
                    return new TokenCache(
                        root.GetProperty("access_token").GetString()!,
                        root.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : null,
                        expiresAt);
                }

                return new TokenCache(
                    root.GetProperty("access_token").GetString()!,
                    root.TryGetProperty("refresh_token", out var rt2) ? rt2.GetString() : null,
                    expiresAt);
            }
            catch { return null; }
        }

        // ---- IPC Helpers ----
        private static string ParseUserId(string json) { /* same as before */ return null; }
        private static bool IsStreaming(string json, string userId) { /* same as before */ return false; }

        private static void SendFrame(NamedPipeClientStream pipe, int opcode, object payload)
        {
            byte[] content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));
            byte[] header = new byte[8];
            BitConverter.GetBytes(opcode).CopyTo(header, 0);
            BitConverter.GetBytes(content.Length).CopyTo(header, 4);
            pipe.Write(header);
            pipe.Write(content);
        }

        private static string ReadFrame(NamedPipeClientStream pipe)
        {
            byte[] header = new byte[8];
            ReadExact(pipe, header, 8);
            int length = BitConverter.ToInt32(header, 4);
            byte[] buffer = new byte[length];
            ReadExact(pipe, buffer, length);
            return Encoding.UTF8.GetString(buffer);
        }

        private static void ReadExact(Stream stream, byte[] buffer, int count)
        {
            int offset = 0;
            while (offset < count)
            {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0) throw new EndOfStreamException();
                offset += read;
            }
        }
    }
}