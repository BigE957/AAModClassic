using AAModClassic.___Content.Void._PostMoonlord.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomPlatform : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Doom Platform");
		}

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 10;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<DoomPlatform_Tile>();
		}

		public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<DoomsdayCircuitPlating>());
            recipe.Register();
        }
	}
}
