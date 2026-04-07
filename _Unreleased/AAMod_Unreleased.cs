using AAModClassic.___Content.Mire.World.Biomes;
using AAModClassic._Unreleased.Content.Parthenan.Biomes;
using AAModClassic._Unreleased.Content.SunkenShip.Biomes;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero.Biomes;
using AAModClassic.Backgrounds;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased
{
    public class AAMod_Unreleased : ModSystem
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
            Mod SacredTools = ModLoader.HasMod("SacredTools") ? ModLoader.GetMod("SacredTools") : null; //TODO update whenever soa ports
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

        public override void Load()
        {
            if (!Main.dedServ)
            {
                LoadClient();
            }
        }
        public void LoadClient()
        {
            Filters.Scene["AAModClassic:CthulhuSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:CthulhuSky"] = new CthulhuSky();

            Filters.Scene["AAModClassic:StormSky"] = new Filter(new StormSkyData("FilterMiniTower").UseColor(0.4f, 0f, 0.6f).UseOpacity(0.3f), EffectPriority.High);
            //SkyManager.Instance["AAModClassic:StormSky"] = new StormBiome(); //TODO: Fake biome

            Filters.Scene["AAModClassic:InfinityZeroSky"] = new Filter(new InfinityZeroSkyData("FilterMiniTower").UseColor(0.4f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:InfinityZeroSky"] = new InfinityZeroSky();
        }
    }
}
