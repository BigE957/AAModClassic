using AAModClassic._Content._Dev._PostMoonlord.Items.Tools;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._Misc.__Hardmode.Items.Materials.Mushrooms;
using AAModClassic._Content.Chaos.__Hardmode.Items.Accessories;
using AAModClassic._Content.Chaos.__Hardmode.Items.Consumables;
using AAModClassic._Content.Chaos.__Hardmode.Items.Tools;
using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Tools;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Tools;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Tools;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Quest;
using AAModClassic._Content.Terra.__Hardmode.Items.Tools;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Tools;
using AAModClassic.Items.Blocks;
using System.Linq;
using Terraria;
using Terraria.Achievements;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace AAModClassic.Achievements
{
    public class ChaosCrafted : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            AddItemCraftCondition(ModContent.ItemType<Chaos_Item>());
        }

        public override Position GetDefaultPosition() => new Before("SWORD_OF_THE_HERO");
    }

    public class DragonstrideBootsCrafted : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            AddItemCraftCondition(ModContent.ItemType<DragonstrideBoots>());
        }

        public override Position GetDefaultPosition() => new After("GET_TERRASPARK_BOOTS");
    }

    public class EXSoulCrafted : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new Before("GET_ZENITH");

        private class EXSoulCraftItem : GlobalItem
        {
            public override void OnCreated(Item item, ItemCreationContext context)
            {
                if (context is RecipeItemCreationContext recipeContext && recipeContext.ConsumedItems.Any(i => i.type == ModContent.ItemType<EXSoul>()))
                    Condition.Complete();
            }
        }
    }

    public class InfinityCoreCrafted : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            AddItemCraftCondition(ModContent.ItemType<TerraPrismStation>());
        }

        public override Position GetDefaultPosition() => new Before("GET_ZENITH");
    }

    public class MadnessPotionCrafted : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new Before("GET_ZENITH");

        private class MadnessPotionCraftItem : GlobalItem
        {
            public override void OnCreated(Item item, ItemCreationContext context)
            {
                if (context is RecipeItemCreationContext recipeContext && recipeContext.ConsumedItems.Any(i => i.type == ModContent.ItemType<RainbowMushroom>()))
                    Condition.Complete();
            }
        }
    }

    public class TerratoolCrafted : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            AddItemPickupCondition([
                ModContent.ItemType<Terratool>(),
                ModContent.ItemType<ChaosTerratool>(),
                ModContent.ItemType<DiscordianTerratool>(),
                ModContent.ItemType<DoomsdayTerratool>(),
                ModContent.ItemType<DraconianTerratool>(),
                ModContent.ItemType<DreadTerratool>(),
                ModContent.ItemType<ExtravagantTerratool>(),
                ModContent.ItemType<GroviteTerratool>()
            ]);
        }

        public override Position GetDefaultPosition() => new Before("SWORD_OF_THE_HERO");
    }

    public class TimecallerCrafted : ModAchievement
    {
        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            AddItemCraftCondition(ModContent.ItemType<Timecaller>());
        }

        public override Position GetDefaultPosition() => new After("PHOTOSYNTHESIS");
    }

    public class LuckyArmorEquipped : ModAchievement
    {
        public static CustomFlagCondition Condition { get; private set; }

        public override void SetStaticDefaults()
        {
            Achievement.SetCategory(AchievementCategory.Collector);
            Condition = AddCondition();
        }

        public override Position GetDefaultPosition() => new After("SUPREME_HELPER_MINION");
    }
}
