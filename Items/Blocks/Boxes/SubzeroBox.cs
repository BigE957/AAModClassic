using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Tiles.Boxes;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class SubzeroBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Subzero Serpent Music Box");
            // Tooltip.SetDefault(@"Plays 'Burrowing Down' by Charlie Debnam");
        }
        

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SubzeroBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Orange;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<SnowMana>(), 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
