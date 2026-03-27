using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic.Removed
{
    public class AAModRemoved : ModSystem
    {
        #region mod loaded bools
        public static bool fargoLoaded = false;
        public static bool calamityLoaded = false;
        public static bool grealmLoaded = false;
        public static bool sacredToolsLoaded = false;
        public static bool spiritLoaded = false;
        public static bool thoriumLoaded = false;
        public static bool tremorLoaded = false;
        public static bool redemptionLoaded = false;
        public static bool cheatsheetLoaded = false;
        public static bool herosLoaded = false;
        #endregion
        public override void PostSetupContent()
        {
            Mod DradonIsDum = ModLoader.HasMod("AchievementLibs") ? ModLoader.GetMod("AchievementLibs") : null;
            Mod bossChecklist = ModLoader.HasMod("BossChecklist") ? ModLoader.GetMod("BossChecklist") : null;
            Mod yabhb = ModLoader.HasMod("FKBossHealthBar") ? ModLoader.GetMod("FKBossHealthBar") : null;
            Mod Calamity = ModLoader.HasMod("CalamityMod") ? ModLoader.GetMod("CalamityMod") : null;
            Mod Thorium = ModLoader.HasMod("ThoriumMod") ? ModLoader.GetMod("ThoriumMod") : null;
            Mod Spirit = ModLoader.HasMod("SpiritMod") ? ModLoader.GetMod("SpiritMod") : null;
            Mod Fargos = ModLoader.HasMod("Fargowiltas") ? ModLoader.GetMod("Fargowiltas") : null;
            Mod GRealm = ModLoader.HasMod("GRealm") ? ModLoader.GetMod("GRealm") : null;
            Mod SacredTools = ModLoader.HasMod("SacredTools") ? ModLoader.GetMod("SacredTools") : null;
            Mod Tremor = ModLoader.HasMod("Tremor") ? ModLoader.GetMod("Tremor") : null;
            Mod Redemption = ModLoader.HasMod("Redemption") ? ModLoader.GetMod("Redemption") : null;
            Mod CheatSheet = ModLoader.HasMod("CheatSheet") ? ModLoader.GetMod("CheatSheet") : null;
            Mod HEROsMod = ModLoader.HasMod("HEROsMod") ? ModLoader.GetMod("HEROsMod") : null;
            if (Calamity != null) calamityLoaded = true;
            if (Thorium != null) thoriumLoaded = true;
            if (Spirit != null) spiritLoaded = true;
            if (Fargos != null) fargoLoaded = true;
            if (GRealm != null) grealmLoaded = true;
            if (SacredTools != null) sacredToolsLoaded = true;
            if (Tremor != null) tremorLoaded = true;
            if (Redemption != null) redemptionLoaded = true;
            if (CheatSheet != null) cheatsheetLoaded = true;
            if (HEROsMod != null) herosLoaded = true;
        }
    }
}
