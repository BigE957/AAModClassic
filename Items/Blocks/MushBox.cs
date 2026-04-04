using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;

namespace AAModClassic.Items.Blocks
{
    public class MushBox : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Red Mushroom Music Box");
            // Tooltip.SetDefault("Plays 'Overgrowth' by Spectral Aves");
        }

        public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<MushBox>();
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
            recipe.AddIngredient(ItemID.Mushroom, 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
