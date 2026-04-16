using AAModClassic.NPCs.Bosses.MushroomMonarch;
using System.Collections.Generic;
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

        public enum Starfarer
        {
            Error = -1,
            None = 0,
            Asphodene = 1,
            Eridani = 2
        }

        public override void PostSetupContent()
        {
            if(ModLoader.TryGetMod("StarsAbove", out var tsa) && tsa.TryFind<ModPlayer>("StarsAbovePlayer", out ModPlayer tsaPlayer))
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
            foreach(ModPlayer mp in p.ModPlayers)
            {
                if (mp.Name == tsaPlayer.Name)
                    return mp;
            }
            return null;
        }

        public static Starfarer SelectedStarfarer(Player player) => (Starfarer)(!Initialized ? -1 : (int)tsaPlayerFieldInfo["chosenStarfarer"].GetValue(GetTsaPlayer(player)));

        public static void StarfarerPromptActive(string key, Player player, bool force = false)
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

                tsaPlayerFieldInfo["promptExpression"].SetValue(tsaPlayer, 1);
                tsaPlayerFieldInfo["promptDialogue"].SetValue(tsaPlayer, Language.GetTextValue($"Mods.AAModClassic.StarsAbove.PromptDialogue." + key + "." + starfarerName, player.name));
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
        public bool SeenMushMon = false;

        public override void PreUpdate()
        {
            foreach(NPC npc in Main.ActiveNPCs)
            {
                if(npc.type == ModContent.NPCType<MushroomMonarch>() && !SeenMushMon)
                {
                    StarsAbove.StarfarerPromptActive("Seen.MushroomMonarch", Main.LocalPlayer, true);
                    if (StarsAbove.SelectedStarfarer(Main.LocalPlayer) == StarsAbove.Starfarer.Asphodene)
                        StarsAbove.PlayVoiceline("BossVoiceNeutral", Main.LocalPlayer);
                    else
                        StarsAbove.PlayVoiceline("BossVoiceSuprise", Main.LocalPlayer);
                }
            }
        }
    }
}
