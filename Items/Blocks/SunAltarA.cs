using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Tiles.Decoration;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class SunAltarA : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Awakened Sun Altar");
        }

        public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.createTile = ModContent.TileType<SunAltarA_Tile>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 15);
			recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}