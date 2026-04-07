using AAModClassic;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.EFish
{
    public class FancyTruffle : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fancy Truffle");
			// Tooltip.SetDefault("Attracts a royal creature which flourishes in water & combat");
        }    
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ScalyTruffle);
			Item.width = 32;
			Item.height = 30;
			Item.value = 500000;
			Item.rare = ItemRarityID.Purple;
			Item.mountType = Mod.Find<ModMount>("PrinceFishron").Type;
		}



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShrimpyTruffle);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}
