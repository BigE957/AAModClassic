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
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes, EquipType.HandsOn, EquipType.HandsOff)]
    public class KappaGear : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<ShadowBandUnofficialEffect>();
            AddEffect<FlipperEffect>();
            AddEffect(new MasterNinjaMobilityEffect(false, true));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KappaFins>());
            recipe.AddIngredient(ItemID.TigerClimbingGear);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KappaMits>());
            recipe.AddIngredient(ItemID.Flipper);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}