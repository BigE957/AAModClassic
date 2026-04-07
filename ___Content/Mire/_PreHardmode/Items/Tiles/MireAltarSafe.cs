using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Crafters;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Tiles
{
    public class MireAltarSafe : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyss Altar");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
            Item.createTile = ModContent.TileType<ChaosAltar_Tile>();
            Item.placeStyle = 0;
            Item.width = 28;
			Item.height = 24;
			Item.rare = ItemRarityID.Orange;
			Item.value = 1000;
			Item.accessory = false;
			Item.maxStack = Item.CommonMaxStack;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
