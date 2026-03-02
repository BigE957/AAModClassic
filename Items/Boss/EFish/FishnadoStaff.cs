using AAModClassic;
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
			Item.shoot = Mod.Find<ModProjectile>("Fishnado").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TempestStaff);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}