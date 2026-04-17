using AAModClassic.NPCs.Bosses.FeudalFungus;
using AAModClassic.NPCs.Bosses.MushroomMonarch;
using AAModClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.CrossMod
{
    public class StarsAbove : ModSystem
    {
        private static Mod Tsa = null;

        private static Dictionary<string, FieldInfo> tsaPlayerFieldInfo = [];

        private static Dictionary<string, MethodInfo> tsaPlayerVoicelines = [];

        public static bool Initialized = false;

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("StarsAbove", out var tsa) && tsa.TryFind<ModPlayer>("StarsAbovePlayer", out ModPlayer tsaPlayer))
            {
                Tsa = tsa;
                var field = tsaPlayer.GetType().GetField("chosenStarfarer", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("chosenStarfarer", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("starfarerPromptCooldown", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("starfarerPromptCooldown", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("disablePrompts", BindingFlags.Static | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("disablePrompts", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("starfarerPromptActiveTimer", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("starfarerPromptActiveTimer", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("promptExpression", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("promptExpression", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("animatedPromptDialogue", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("animatedPromptDialogue", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("promptDialogueScrollNumber", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("promptDialogueScrollNumber", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("promptDialogueScrollTimer", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("promptDialogueScrollTimer", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("promptMoveIn", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("promptMoveIn", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("promptIsActive", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("promptIsActive", field);
                else
                    return;

                field = tsaPlayer.GetType().GetField("promptDialogue", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    tsaPlayerFieldInfo.Add("promptDialogue", field);
                else
                    return;

                Initialized = true;

                MethodInfo method = tsaPlayer.GetType().GetMethod("VoiceExplore", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                    tsaPlayerVoicelines.Add("VoiceExplore", method);

                method = tsaPlayer.GetType().GetMethod("BossVoiceSuprise", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                    tsaPlayerVoicelines.Add("BossVoiceSuprise", method);

                method = tsaPlayer.GetType().GetMethod("BossVoiceNeutral", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                    tsaPlayerVoicelines.Add("BossVoiceNeutral", method);

                method = tsaPlayer.GetType().GetMethod("BossVoiceAngry", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                    tsaPlayerVoicelines.Add("BossVoiceAngry", method);
            }
        }

        private static ModPlayer GetTsaPlayer(Player p)
        {
            ModPlayer tsaPlayer = Tsa.Find<ModPlayer>("StarsAbovePlayer");
            foreach (ModPlayer mp in p.ModPlayers)
            {
                if (mp.Name == tsaPlayer.Name)
                    return mp;
            }
            return null;
        }

        public static TsaPlayer.Starfarer SelectedStarfarer(Player player) => (TsaPlayer.Starfarer)(!Initialized ? -1 : tsaPlayerFieldInfo["chosenStarfarer"].GetValue(GetTsaPlayer(player)));

        public static void StarfarerPromptActive(string key, Player player, int expression = -1, bool force = false)
        {
            if (!Initialized)
                return;

            ModPlayer tsaPlayer = GetTsaPlayer(player);
            int chosenStarfarer = (int)tsaPlayerFieldInfo["chosenStarfarer"].GetValue(tsaPlayer);
            int starfarerPromptCooldown = (int)tsaPlayerFieldInfo["starfarerPromptCooldown"].GetValue(tsaPlayer);
            bool promptIsActive = (bool)tsaPlayerFieldInfo["promptIsActive"].GetValue(tsaPlayer);
            int starfarerPromptActiveTimer = (int)tsaPlayerFieldInfo["starfarerPromptActiveTimer"].GetValue(tsaPlayer);

            if (!(bool)tsaPlayerFieldInfo["disablePrompts"].GetValue(null) && chosenStarfarer != 0 && (force || (starfarerPromptCooldown <= 0 && !promptIsActive && starfarerPromptActiveTimer <= 0)))
            {
                //If the check was successful...
                SoundEngine.PlaySound(SoundID.MenuOpen, player.position); //Menu sound here
                if (!promptIsActive || force)
                {
                    tsaPlayerFieldInfo["promptExpression"].SetValue(tsaPlayer, 0);
                    tsaPlayerFieldInfo["promptDialogue"].SetValue(tsaPlayer, "");
                    tsaPlayerFieldInfo["promptDialogueScrollNumber"].SetValue(tsaPlayer, 0);
                    tsaPlayerFieldInfo["promptDialogueScrollTimer"].SetValue(tsaPlayer, 0);
                    tsaPlayerFieldInfo["animatedPromptDialogue"].SetValue(tsaPlayer, "");

                    tsaPlayerFieldInfo["starfarerPromptActiveTimer"].SetValue(tsaPlayer, 300); //starfarerPromptActiveTimerSetting = 100

                    tsaPlayerFieldInfo["promptIsActive"].SetValue(tsaPlayer, true);
                }

                tsaPlayerFieldInfo["promptMoveIn"].SetValue(tsaPlayer, 15f);

                string starfarerName = "";
                if (chosenStarfarer == 1)
                    starfarerName = "Asphodene";
                else if (chosenStarfarer == 2)
                    starfarerName = "Eridani";

                tsaPlayerFieldInfo["promptExpression"].SetValue(tsaPlayer, 1); // default worried
                tsaPlayerFieldInfo["promptDialogue"].SetValue(tsaPlayer, Language.GetTextValue($"Mods.AAModClassic.StarsAbove.PromptDialogue." + key + "." + starfarerName, player.name));

                // Override expression if provided
                if (expression != -1)
                    tsaPlayerFieldInfo["promptExpression"].SetValue(tsaPlayer, expression);
            }
        }

        public static void PlayVoiceline(string key, Player p)
        {
            if (!Initialized)
                return;

            ModPlayer tsaPlayer = GetTsaPlayer(p);

            if (tsaPlayerVoicelines.TryGetValue(key, out var method))
                method.Invoke(tsaPlayer, null);
        }
    }

    public class TsaPlayer : ModPlayer
    {
        public enum Starfarer
        {
            Error = -1,
            None = 0,
            Asphodene = 1,
            Eridani = 2
        }

        public enum StarfarerExpression
        {
            Neutral = 0,
            Worried = 1,
            Surprised = 2,
            Angry = 3,
            Thinking = 4,
            Smug = 5
        }

        public HashSet<string> SeenBoss = [];

        public struct StarfarerPromptData(Func<string> key, StarfarerExpression aExpr, StarfarerExpression eExpr, string aVoice, string eVoice)
        {
            public Func<string> Key = key;
            public StarfarerExpression AsphodeneExpression = aExpr;
            public StarfarerExpression EridaniExpression = eExpr;
            public string AsphodeneVoiceline = aVoice;
            public string EridaniVoiceline = eVoice;

            public static StarfarerPromptData CustomSeen(
                Func<string> key,
                StarfarerExpression aExpr = 0,
                StarfarerExpression eExpr = 0,
                string aVoice = "BossVoiceSuprise",
                string eVoice = "BossVoiceNeutral"
            ) => new(key, aExpr, eExpr, aVoice, eVoice);

            public static StarfarerPromptData CustomDefeat(
                Func<string> key,
                StarfarerExpression aExpr = 0,
                StarfarerExpression eExpr = 0,
                string aVoice = "BossVoiceNeutral",
                string eVoice = "BossVoiceNeutral"
            ) => new(key, aExpr, eExpr, aVoice, eVoice);

            public static StarfarerPromptData DefaultSeenData(string name) => new(() => name + ".Seen", 0, 0, "BossVoiceSuprise", "BossVoiceNeutral");
            public static StarfarerPromptData DefaultDefeatData(string name) => new(() => name + ".Defeat", 0, 0, null, null);
        }

        internal struct BossData
        {
            public string Name;
            public StarfarerPromptData SeenData;
            public StarfarerPromptData DefeatData;

            internal BossData(string name, StarfarerPromptData seen, StarfarerPromptData defeat)
            {
                Name = name;
                SeenData = seen;
                DefeatData = defeat;
            }

            internal BossData(string name, StarfarerPromptData seen)
            {
                Name = name;
                SeenData = seen;
                DefeatData = StarfarerPromptData.DefaultDefeatData(name);
            }

            internal BossData(string name) : this(name, StarfarerPromptData.DefaultSeenData(name)) { }
        }

        private static readonly List<BossData> BossRegistry = new()
        {
            new("MushroomMonarch",
                StarfarerPromptData.CustomSeen(() => "MushroomMonarch.Seen"),
                StarfarerPromptData.CustomDefeat(() => "MushroomMonarch.Defeat", StarfarerExpression.Thinking)),

            new("FeudalFungus",
                StarfarerPromptData.CustomSeen(() => "FeudalFungus.Seen", StarfarerExpression.Thinking, StarfarerExpression.Worried, "BossVoiceNeutral", "BossVoiceNeutral"),
                StarfarerPromptData.CustomDefeat(() => "FeudalFungus.Defeat", aExpr: StarfarerExpression.Smug)),

            new("GripOfChaosBlue",
                StarfarerPromptData.CustomSeen(() => "GripsOfChaos.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "GripsOfChaos.Defeat", aExpr: StarfarerExpression.Smug)),

            new("TruffleToad",
                StarfarerPromptData.CustomSeen(() => "TruffleToad.Seen", aExpr: StarfarerExpression.Smug),
                StarfarerPromptData.CustomDefeat(() => "TruffleToad.Defeat", aExpr: StarfarerExpression.Smug)),

            new("Broodmother",
                StarfarerPromptData.CustomSeen(() => "Broodmother.Seen", StarfarerExpression.Smug, StarfarerExpression.Worried),
                StarfarerPromptData.CustomDefeat(() => "Broodmother.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Neutral)),

            new("HydraBody",
                StarfarerPromptData.CustomSeen(() => "HydraBody.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "HydraBody.Defeat", StarfarerExpression.Thinking, StarfarerExpression.Neutral)),

            new("SerpentHead",
                StarfarerPromptData.CustomSeen(() => "SubzeroSerpent.Seen", aExpr: StarfarerExpression.Smug),
                StarfarerPromptData.CustomDefeat(() => "SubzeroSerpent.Defeat", aExpr: StarfarerExpression.Smug)),

            new("Djinn",
                StarfarerPromptData.CustomSeen(() => "Djinn.Seen", aExpr: StarfarerExpression.Thinking),
                StarfarerPromptData.CustomDefeat(() => "Djinn.Defeat", StarfarerExpression.Smug, StarfarerExpression.Thinking)),

            new("Sag",
                StarfarerPromptData.CustomSeen(() => "Sagittarius.Seen", StarfarerExpression.Smug, StarfarerExpression.Thinking),
                StarfarerPromptData.CustomDefeat(() => "Sagittarius.Defeat", StarfarerExpression.Thinking, StarfarerExpression.Neutral)),

            new("Anubis",
                StarfarerPromptData.CustomSeen(() => "Anubis.Seen", aExpr: StarfarerExpression.Worried, aVoice: "BossVoiceNeutral"),
                StarfarerPromptData.CustomDefeat(() => "Anubis.Defeat", aExpr: StarfarerExpression.Smug)),

            new("Athena",
                StarfarerPromptData.CustomSeen(() => "Athena.Seen", StarfarerExpression.Angry, StarfarerExpression.Thinking, aVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "Athena.Defeat", aExpr: StarfarerExpression.Smug)),

            new("Greed",
                StarfarerPromptData.CustomSeen(() => "Greed.Seen", StarfarerExpression.Smug, StarfarerExpression.Angry, "BossVoiceNeutral", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "Greed.Defeat", aExpr: StarfarerExpression.Thinking)),

            new("Rajah",
                StarfarerPromptData.CustomSeen(() => "Rajah.Seen", StarfarerExpression.Worried, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "Rajah.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Worried)),

            new("ForsakenAnubis",
                StarfarerPromptData.CustomSeen(() => "ForsakenAnubis.Seen", StarfarerExpression.Angry, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "ForsakenAnubis.Defeat", StarfarerExpression.Smug, StarfarerExpression.Thinking)),

            new("AthenaA",
                StarfarerPromptData.CustomSeen(() => "AthenaA.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "AthenaA.Defeat", StarfarerExpression.Angry, StarfarerExpression.Thinking, aVoice: "BossVoiceAngry")),

            new("GreedA",
                StarfarerPromptData.CustomSeen(() => "GreedA.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "GreedA.Defeat", StarfarerExpression.Angry, StarfarerExpression.Thinking, aVoice: "BossVoiceAngry")),

            new("DaybringerHead",
                StarfarerPromptData.CustomSeen(() => "EquinoxWorms.Seen", aVoice: "BossVoiceNeutral"),
                StarfarerPromptData.CustomDefeat(() => "EquinoxWorms.Defeat", StarfarerExpression.Thinking, StarfarerExpression.Thinking)),

            new("NightcrawlerHead",
                StarfarerPromptData.CustomSeen(() => "EquinoxWorms.Seen", aVoice: "BossVoiceNeutral"),
                StarfarerPromptData.CustomDefeat(() => "EquinoxWorms.Defeat", StarfarerExpression.Thinking, StarfarerExpression.Thinking)),

            new("Ashe",
                StarfarerPromptData.CustomSeen(() => "SistersOfDiscord.Seen", StarfarerExpression.Angry, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "SistersOfDiscord.Defeat", StarfarerExpression.Thinking, StarfarerExpression.Thinking)),

            new("Haruka",
                StarfarerPromptData.CustomSeen(() => "SistersOfDiscord.Seen", StarfarerExpression.Angry, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "SistersOfDiscord.Defeat", StarfarerExpression.Thinking, StarfarerExpression.Thinking)),

            new("Akuma",
                StarfarerPromptData.CustomSeen(() => "Akuma.Seen", aExpr: StarfarerExpression.Smug, aVoice: "BossVoiceNeutral"),
                StarfarerPromptData.CustomDefeat(() => Main.expertMode ? "Akuma.Defeat.Expert" : "Akuma.Defeat.NotExpert", StarfarerExpression.Surprised, StarfarerExpression.Thinking)),

            new("AkumaA",
                StarfarerPromptData.CustomSeen(() => "AkumaA.Seen", StarfarerExpression.Angry, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "AkumaA.Defeat", StarfarerExpression.Smug, StarfarerExpression.Thinking)),

            new("YamataBody",
                StarfarerPromptData.CustomSeen(() => "YamataBody.Seen", StarfarerExpression.Worried, StarfarerExpression.Angry, aVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => Main.expertMode ? "YamataBody.Defeat.Expert" : "YamataBody.Defeat.NotExpert", StarfarerExpression.Surprised, StarfarerExpression.Worried)),

            new("YamataABody",
                StarfarerPromptData.CustomSeen(() => "YamataABody.Seen", StarfarerExpression.Angry, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "YamataABody.Defeat", StarfarerExpression.Smug, StarfarerExpression.Thinking)),

            new("Zero",
                StarfarerPromptData.CustomSeen(() => "Zero.Seen", aExpr: StarfarerExpression.Thinking, aVoice: "BossVoiceNeutral"),
                StarfarerPromptData.CustomDefeat(() => "Zero.Defeat.NotExpert", StarfarerExpression.Surprised, StarfarerExpression.Worried)),

            new("ZeroProtocol",
                StarfarerPromptData.CustomSeen(() => "ZeroProtocol.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "ZeroProtocol.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Worried)),

            new("SupremeRajah",
                StarfarerPromptData.CustomSeen(() => "SupremeRajah.Seen", StarfarerExpression.Worried, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "SupremeRajah.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Thinking)),

            new("Shen",
                StarfarerPromptData.CustomSeen(() => "Shen.Seen", StarfarerExpression.Worried, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => Main.expertMode ? "Shen.Defeat.Expert" : "Shen.Defeat.NotExpert", StarfarerExpression.Surprised, StarfarerExpression.Thinking)),

            new("ShenA",
                StarfarerPromptData.CustomSeen(() => "ShenA.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "ShenA.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Thinking)),

            new("InfinityZero",
                StarfarerPromptData.CustomSeen(() => "InfinityZero.Seen", StarfarerExpression.Surprised, StarfarerExpression.Angry, eVoice: "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "InfinityZero.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Worried)),

            new("SoulOfCthulhu",
                StarfarerPromptData.CustomSeen(() => "SoulOfCthulhu.Seen", StarfarerExpression.Worried, StarfarerExpression.Angry, "BossVoiceAngry", "BossVoiceAngry"),
                StarfarerPromptData.CustomDefeat(() => "SoulOfCthulhu.Defeat", StarfarerExpression.Surprised, StarfarerExpression.Worried, aVoice: "BossVoiceAngry")),
        };

        internal static Dictionary<int, BossData> _lookupCache;

        private void InitializeLookup()
        {
            _lookupCache = [];

            foreach (var data in BossRegistry)
                if (Mod.TryFind<ModNPC>(data.Name, out var modNpc))
                    _lookupCache[modNpc.Type] = data;
        }

        public override void PreUpdate()
        {
            if (_lookupCache == null) InitializeLookup();

            foreach (NPC npc in Main.ActiveNPCs)
                if (npc.ModNPC != null && _lookupCache.TryGetValue(npc.type, out var data))
                    CheckBossEncounter(data);
        }

        internal void CheckBossEncounter(BossData data)
        {
            if (SeenBoss.Contains(data.Name))
                return;

            string dialogue = data.SeenData.Key.Invoke();
            if (dialogue != null)
            {
                int expression = (int)(StarsAbove.SelectedStarfarer(Player) == Starfarer.Asphodene ? data.SeenData.AsphodeneExpression : data.SeenData.EridaniExpression);
                StarsAbove.StarfarerPromptActive(dialogue, Player, expression, true);

                string voice = StarsAbove.SelectedStarfarer(Player) == Starfarer.Asphodene ? data.SeenData.AsphodeneVoiceline : data.SeenData.EridaniVoiceline;

                StarsAbove.PlayVoiceline(voice, Player);
            }

            SeenBoss.Add(data.Name);
        }

        internal void BossDefeat(BossData data)
        {
            string dialogue = data.DefeatData.Key.Invoke();
            if (dialogue != null)
            {
                int expression = (int)(StarsAbove.SelectedStarfarer(Player) == Starfarer.Asphodene ? data.DefeatData.AsphodeneExpression : data.DefeatData.EridaniExpression);
                StarsAbove.StarfarerPromptActive(dialogue, Player, expression, true);

                string voice = StarsAbove.SelectedStarfarer(Player) == Starfarer.Asphodene ? data.DefeatData.AsphodeneVoiceline : data.DefeatData.EridaniVoiceline;

                StarsAbove.PlayVoiceline(voice, Player);
            }

            SeenBoss.Add(data.Name);
        }
    }

    public class TsaGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.ModNPC != null && !NPCExtensions.BeenKilled(npc, true) && TsaPlayer._lookupCache.TryGetValue(npc.type, out var data))
                Main.LocalPlayer.GetModPlayer<TsaPlayer>().BossDefeat(data);
        }
    }
}