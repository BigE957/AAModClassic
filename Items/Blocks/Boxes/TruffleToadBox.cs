using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Tiles.Boxes;
using AAModClassic.___Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic.___Content.GlowingMushroom.___PreHardmode.Items.Materials;

namespace AAModClassic.Items.Blocks.Boxes
{
    public class TruffleToadBox : BaseAAItem
	{
            
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Truffle Toad Music Box");
            // Tooltip.SetDefault("Plays 'TODESTOOL' by Spectral Aves");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<TruffleToadBox_Tile>();
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
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<GlowingMushiumBar>(), 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
