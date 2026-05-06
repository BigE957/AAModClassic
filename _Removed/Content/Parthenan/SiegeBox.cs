using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;

namespace AAModClassic._Removed.Content.Parthenan
{
    public class SiegeBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("S.I.E.G.E. Bosses Music Box");
            // Tooltip.SetDefault(@"Plays 'Storming S.I.E.G.E.' by Karamitasu");
        }

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SiegeBox_Tile>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = 4;
			Item.value = 10000;
			Item.accessory = true;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
