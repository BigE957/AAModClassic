using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class CuringBand : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shadow Band");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<CuringBandEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MendingBand>());
            recipe.AddIngredient(ItemID.PhilosophersStone);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CharmofMyths);
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddCondition(ConditionUtils.Unofficial);
            recipe.Register();
        }
    }
}