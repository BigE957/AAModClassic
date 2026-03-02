using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class FreedomStar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Freedom Star");
            /* Tooltip.SetDefault(@"Tails' trusty blaster.
Hold the use button to charge, and then release a powerful Charged Shot!
Kept you waiting, huh?
Tails
Mobian Buster EX"); */
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 250;  
            Item.shoot = Mod.Find<ModProjectile>("FreedomStar").Type;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.channel = true;
            Item.sellPrice(3, 0, 0, 0);
            Item.noMelee = true;
			Item.rare = 11;
			Item.shootSpeed = 12f;
			Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "MobianBuster");
                recipe.AddIngredient(null, "EXSoul");
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.Register();
            }
        }
    }
}

// pls nerf
