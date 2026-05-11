using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic._Content.Void.___PreHardmode.NPCs.__BossSagittarius;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace AAModClassic.Achievements
{
    public class MushroomMonarchKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<MushroomMonarch>());
        }

        public override Position GetDefaultPosition() => new Before("HEART_BREAKER");
    }

    public class FeudalFungusKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<FeudalFungus>());
        }

        public override Position GetDefaultPosition() => new Before("HEART_BREAKER");
    }

    public class GripsOfChaosKilled : ModAchievement
    {
        public static CustomFlagCondition KilledGripsCondition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            KilledGripsCondition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("EYE_ON_YOU");
    }

    public class TruffleToadKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<TruffleToad>());
        }

        public override Position GetDefaultPosition() => new After("SMASHING_POPPET");
    }

    public class HydraKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<HydraBody>());
        }

        public override Position GetDefaultPosition() => new After("MASTERMIND");
    }

    public class BroodmotherKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Broodmother>());
        }

        public override Position GetDefaultPosition() => new After("MASTERMIND");
    }

    public class DesertDjinnKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<DesertDjinn>());
        }

        public override Position GetDefaultPosition() => new After("DUNGEON_HEIST");
    }

    public class SubzeroSerpentKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<SubzeroSerpent_Head>());
        }

        public override Position GetDefaultPosition() => new After("DUNGEON_HEIST");
    }

    public class SagittariusKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Sagittarius>());
        }

        public override Position GetDefaultPosition() => new After("MINER_FOR_FIRE");
    }
}
