using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Evil.___PreHardmode.Items.Tiles.Functional;

namespace AAModClassic._Content.Corruption.___PreHardmode.Items.Tiles.Functional
{
    public class CorruptAltar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Altar");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EvilAltarSafe_Tile>();
            Item.placeStyle = 0;
            Item.width = 28;
            Item.height = 26;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.accessory = false;
            Item.maxStack = Item.CommonMaxStack;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 15);
            recipe.AddIngredient(ItemID.ShadowScale, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}

