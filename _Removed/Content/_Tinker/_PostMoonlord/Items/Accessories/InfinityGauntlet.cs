using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.Wings)]
    [AutoloadEquipGlow(EquipType.HandsOn, EquipType.Wings)]
    public class InfinityGauntlet : EquipAbstract, ILocalizedModType
    {
            
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Infinity Gauntlet");
            Tooltip.SetDefault(
@"Pressing the G key allows you to snap your fingers, wiping out half of the enemies on your screen
The snap has a 5 minute cooldown
All effects of the infinity stones
'Perfectly Balanced, as all things should be'");*/
        }

        public bool death;
        public int rodCD;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.defense = 12;
            
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<InfinityGauntletEffect>();
            AddEffect(new WingTimeMaxEffect(500));
            AddEffect(new MaxRunSpeedEffect(10.00f)); // !!!
            AddEffect(new MovementSpeedEffect(1.00f));
            AddEffect(new FrostsparkBootsEffect(0, 0, true));
            bool lol = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? true : false;
            AddEffect(new LavaWadersWaterWalkingEffect(lol));
            AddEffect(new LavaWadersFireImmunityEffect(true, 0));
            AddEffect<FullLavaImmunityEffect>();
            AddEffect<SpelunkerEffect>();
            AddEffect<HunterEffect>();
            AddEffect<DangersenseEffect>();
            AddEffect(new MaxMinionSlotEffect(6));
            AddEffect(new MaxManaEffect(200));
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new ManaCostMultiplierEffect(0.50f));
            else
                AddEffect(new ManaCostEffect(0.50f));
            AddEffect(new AttacksInflictBuffEffect(null, (BuffID.Chilled, 1200))); // real effect of hte item just undocumented
            AddEffect<TimeStoneRespawnEffect>();
            AddEffect<TimeStoneTimeStopEffect>();
            AddEffect<SpaceStoneEffect>();
            AddEffect(new BuffImmunityEffect(BuffID.ChaosState));
            damageMap.GetDamage(DamageClass.Generic) += .40f;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.4f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.3f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 20f;
            acceleration *= 3f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DwarvenGauntlet>();
            recipe.AddIngredient<RealityStone>();
            recipe.AddIngredient<SoulStone>();
            recipe.AddIngredient<MindStone>();
            recipe.AddIngredient<TimeStone>();
            recipe.AddIngredient<SpaceStone>();
            recipe.AddIngredient<PowerStone>();
            recipe.Register();
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return incomingItem.type != ModContent.ItemType<MindStone>() || incomingItem.type != ModContent.ItemType<PowerStone>() || incomingItem.type != ModContent.ItemType<RealityStone>() || incomingItem.type != ModContent.ItemType<SoulStone>() || incomingItem.type != ModContent.ItemType<SpaceStone>() || incomingItem.type != ModContent.ItemType<TimeStone>() || incomingItem.type != ModContent.ItemType<InfinityGauntlet>();
        }
    }
}