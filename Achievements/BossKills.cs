using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic._Content.Void.___PreHardmode.NPCs.__BossSagittarius;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever;
using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using System.Collections.Generic;
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

        public override Position GetAdvisorPosition() => new Before("HEART_BREAKER");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class FeudalFungusKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<FeudalFungus>());
        }

        public override Position GetDefaultPosition() => new Before("HEART_BREAKER");

        public override Position GetAdvisorPosition() => new Before("HEART_BREAKER");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<MushroomMonarchKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class GripsOfChaosKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("EYE_ON_YOU");

        public override Position GetAdvisorPosition() => new After("EYE_ON_YOU");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class TruffleToadKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<TruffleToad>());
        }

        public override Position GetDefaultPosition() => new After("SMASHING_POPPET");

        public override Position GetAdvisorPosition() => new After("SMASHING_POPPET");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class HydraKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<HydraBody>());
        }

        public override Position GetDefaultPosition() => new After("MASTERMIND");

        public override Position GetAdvisorPosition() => new Before("WHERES_MY_HONEY");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class BroodmotherKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Broodmother>());
        }

        public override Position GetDefaultPosition() => new After("MASTERMIND");

        public override Position GetAdvisorPosition() => new Before("WHERES_MY_HONEY");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class DesertDjinnKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<DesertDjinn>());
        }

        public override Position GetDefaultPosition() => new After("DUNGEON_HEIST");

        public override Position GetAdvisorPosition() => new After("DUNGEON_HEIST");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class SubzeroSerpentKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<SubzeroSerpent_Head>());
        }

        public override Position GetDefaultPosition() => new After("DUNGEON_HEIST");

        public override Position GetAdvisorPosition() => new After("DUNGEON_HEIST");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class SagittariusKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Sagittarius>());
        }

        public override Position GetDefaultPosition() => new After("MINER_FOR_FIRE");

        public override Position GetAdvisorPosition() => new After("MINER_FOR_FIRE");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class TechnoTruffleKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<TechnoTruffle>());
        }

        public override Position GetDefaultPosition() => new Before("HEAD_IN_THE_CLOUDS");

        public override Position GetAdvisorPosition() => new Before("HEAD_IN_THE_CLOUDS");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class SiegeMechsKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddManyNPCKilledCondition([ModContent.NPCType<Retriever>(), ModContent.NPCType<OrthrusXBody>(), ModContent.NPCType<RaiderUltima>()]);
        }

        public override Position GetDefaultPosition() => new After("BUCKETS_OF_BOLTS");

        public override Position GetAdvisorPosition() => new After("BUCKETS_OF_BOLTS");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class StormingSiege : ModAchievement
    {
        public static CustomFlagCondition StormingSiegeCondition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            StormingSiegeCondition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("MECHA_MAYHEM");
    }

    public class AnubisKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Anubis>());
            AddNPCKilledCondition(ModContent.NPCType<AnubisUnreleased>());
        }

        public override Position GetDefaultPosition() => new After("GET_A_LIFE");

        public override Position GetAdvisorPosition() => new After("GET_A_LIFE");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class BiomiteCoreKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<BiomiteCore>());
        }

        public override Position GetDefaultPosition() => new After("THE_GREAT_SOUTHERN_PLANTKILL");

        public override Position GetAdvisorPosition() => new After("THE_GREAT_SOUTHERN_PLANTKILL");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class AthenaKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<Athena>());
        }

        public override Position GetDefaultPosition() => new After("LIHZAHRDIAN_IDOL");

        public override Position GetAdvisorPosition() => new After("LIHZAHRDIAN_IDOL");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class GreedKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<GreedHead>());
        }

        public override Position GetDefaultPosition() => new After("LIHZAHRDIAN_IDOL");

        public override Position GetAdvisorPosition() => new After("LIHZAHRDIAN_IDOL");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class RajahKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<RajahRabbit>());
        }

        public override Position GetDefaultPosition() => new Before("FISH_OUT_OF_WATER");

        public override Position GetAdvisorPosition() => new Before("OBSESSIVE_DEVOTION");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class PreEquinoxAncientsKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddManyNPCKilledCondition([ModContent.NPCType<AnubisA>(), ModContent.NPCType<AthenaA>(), ModContent.NPCType<GreedAHead>()]);
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class EquinoxWormsKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<PreEquinoxAncientsKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class SistersOfDiscordKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<CelestialOreMined>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class AkumaKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<TierThreeChaosOreMined>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class YamataKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<AkumaKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class ZeroKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<YamataKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class ChampionRajahKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<RajahRabbitA>());
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<ApocalyptiteOreMined>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class ShenDoragonKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<ChampionRajahKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class InfinityZeroKilled : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            AddNPCKilledCondition(ModContent.NPCType<InfinityZero>());
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<ChampionRajahKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class SoulOfCthulhuKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<ChampionRajahKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBossesKilled.TryComplete();
        }
    }

    public class AllBossesKilled : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Slayer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<SoulOfCthulhuKilled>());
        }

        public static void TryComplete()
        {
            //Blarg
            if (ModContent.GetInstance<MushroomMonarchKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<FeudalFungusKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<GripsOfChaosKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<BroodmotherKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<HydraKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<DesertDjinnKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<SubzeroSerpentKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<SagittariusKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<TechnoTruffleKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<SiegeMechsKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<AnubisKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<BiomiteCoreKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<AthenaKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<GreedKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<RajahKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<PreEquinoxAncientsKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<EquinoxWormsKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<SistersOfDiscordKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<AkumaKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<YamataKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<ZeroKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<ShenDoragonKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<InfinityZeroKilled>().Achievement.IsCompleted &&
                ModContent.GetInstance<SoulOfCthulhuKilled>().Achievement.IsCompleted)
            {
                Condition.Complete();
            }
        }
    }
}
