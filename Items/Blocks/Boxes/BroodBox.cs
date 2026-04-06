using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class BroodBox : BaseAAItem
	{
            
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broodmother Music Box");
            // Tooltip.SetDefault(@"Plays 'Blazing Fury' by Spectral Aves");
        }

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<BroodBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.LightRed;
			Item.value = 10000;
			Item.accessory = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "IncineriteBar", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
