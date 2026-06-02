using System.Collections.Generic;
using Terraria.Achievements;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace AAModClassic.Achievements
{
    public class AcropolisDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("LIHZAHRDIAN_IDOL");

        public override Position GetAdvisorPosition() => new After("LIHZAHRDIAN_IDOL");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new Before(ModContent.GetInstance<AthenaKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBiomesDiscovered.TryComplete();
        }
    }

    public class HoardDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("LIHZAHRDIAN_IDOL");

        public override Position GetAdvisorPosition() => new After("LIHZAHRDIAN_IDOL");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new Before(ModContent.GetInstance<GreedKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBiomesDiscovered.TryComplete();
        }
    }

    public class GreedChestOpened : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("LIHZAHRDIAN_IDOL");

        public override Position GetAdvisorPosition() => new After("LIHZAHRDIAN_IDOL");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new After(ModContent.GetInstance<HoardDiscovered>());
        }
    }

    public class EquinoxAltarDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("CHAMPION_OF_TERRARIA");

        public override Position GetAdvisorPosition() => new After("CHAMPION_OF_TERRARIA");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new Before(ModContent.GetInstance<EquinoxWormsKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBiomesDiscovered.TryComplete();
        }
    }

    public class RedMushroomDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new Before("FUNKYTOWN");

        public override void OnCompleted(Achievement achievement)
        {
            AllBiomesDiscovered.TryComplete();
        }
    }

    public class MushmanEncountered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new Before("IT_CAN_TALK");
    }

    public class TerrariumDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("MASTERMIND");

        public override Position GetAdvisorPosition() => new After("SMASHING_POPPET");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new Before(ModContent.GetInstance<BroodmotherKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBiomesDiscovered.TryComplete();
        }
    }

    public class VoidDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("MINER_FOR_FIRE");

        public override Position GetAdvisorPosition() => new After("MINER_FOR_FIRE");

        public override IEnumerable<Position> GetModdedConstraints()
        {
            yield return new Before(ModContent.GetInstance<SagittariusKilled>());
        }

        public override void OnCompleted(Achievement achievement)
        {
            AllBiomesDiscovered.TryComplete();
        }
    }

    public class AllBiomesDiscovered : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Explorer);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("ROCK_BOTTOM");

        public static void TryComplete()
        {
            //Blarg
            if (ModContent.GetInstance<AcropolisDiscovered>().Achievement.IsCompleted &&
                ModContent.GetInstance<HoardDiscovered>().Achievement.IsCompleted &&
                ModContent.GetInstance<EquinoxAltarDiscovered>().Achievement.IsCompleted &&
                ModContent.GetInstance<RedMushroomDiscovered>().Achievement.IsCompleted &&
                ModContent.GetInstance<TerrariumDiscovered>().Achievement.IsCompleted &&
                ModContent.GetInstance<VoidDiscovered>().Achievement.IsCompleted)
            {
                Condition.Complete();
            }
        }
    }
}
