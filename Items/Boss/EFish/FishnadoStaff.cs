using AAModClassic.Projectiles.EFish;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.EFish
{
    public class FishnadoStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fishnado Staff");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.TempestStaff);
			Item.damage = 150;
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<Fishnado>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TempestStaff);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}