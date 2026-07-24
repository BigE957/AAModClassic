using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class DragonstrideBoots : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragonstride Boots");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new FrostsparkBootsEffect(3, 9f, true));
            AddEffect(new MovementSpeedEffect(0.12f));
            AddEffect(new MaxRunSpeedEffect(0.12f));
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new LavaWadersWaterWalkingEffect(true));
            else
                AddEffect(new LavaWadersWaterWalkingEffect(false));
            AddEffect(new LavaWadersFireImmunityEffect(true, 600));
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect<ObsidianRoseEffect>();
            AddEffect<FlipperEffect>();
            AddEffect(new MasterNinjaMobilityEffect(false, true));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TerrasparkBoots, 1);
            recipe.AddIngredient(ItemID.TigerClimbingGear, 1);
            recipe.AddIngredient(ItemID.Flipper, 1);
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}