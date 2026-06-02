using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using System.Collections.Generic;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace AAModClassic.Achievements
{
    public class ChaosOreMined : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            AddTileDestroyedCondition([ModContent.TileType<IncineriteOre_Tile>(), ModContent.TileType<AbyssiumOre_Tile>()]);
        }

        public override Position GetDefaultPosition() => new Before("WHERES_MY_HONEY");
        public override Position GetAdvisorPosition() => new Before("WHERES_MY_HONEY");
    }

    public class CelestialOreMined : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            AddTileDestroyedCondition([ModContent.TileType<RadiumOre_Tile>()]);
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<EquinoxWormsKilled>());
        }
    }

    public class TierThreeChaosOreMined : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            AddTileDestroyedCondition([ModContent.TileType<DaybreakIncineriteOre_Tile>(), ModContent.TileType<EventideAbyssiumOre_Tile>()]);
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<SistersOfDiscordKilled>());
        }
    }

    public class ApocalyptiteOreMined : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            AddTileDestroyedCondition([ModContent.TileType<ApocalyptiteOre_Tile>()]);
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<ZeroKilled>());
        }
    }
}
