using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;
using AAModClassic.___Content.Desert.__Hardmode.Items.Materials;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class AnubisBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Music Box");
            // Tooltip.SetDefault(@"Plays 'Strings of Judgement' by Universe");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AnubisBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Pink;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<ForsakenFragment>(), 3);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
