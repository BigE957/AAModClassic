using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Void : BaseAAItem
    {

        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.Terrarian);
            Item.damage = 190;                            
            Item.value = 1000000;
            Item.rare = 9;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = 5;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.rare = 11;
            Item.shoot = Mod.Find<ModProjectile>("Void").Type;  
		}

        public override void SetStaticDefaults()
        {
             // DisplayName.SetDefault("Void");
            // Tooltip.SetDefault("Made out of pure Dark Matter");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarkEnergy", 5);
            recipe.AddIngredient(null, "DarkMatter", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }

    }
}
