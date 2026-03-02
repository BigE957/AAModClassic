using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class TrueTerraBallista : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Unity Ballista");
            /* Tooltip.SetDefault(@"Replaces Arrows with Terra Arrows
Shoots 3 waves of 3 arrows on single use
Terra Ballista EX"); */
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 215;
	        Item.crit += 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 50;
	        Item.height = 34;
	        Item.useTime = 3;
	        Item.reuseDelay = 15;
	        Item.useAnimation = 9;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
	        Item.value = 500000;
	        Item.rare = 11;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			Vector2 perturbedSpeed3 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			float speedX2 = perturbedSpeed2.X;
			float speedY2 = perturbedSpeed2.Y;
			float speedX3 = perturbedSpeed3.X;
			float speedY3 = perturbedSpeed3.Y;
			Projectile.NewProjectile(vector.X, vector.Y, speedX2, speedY2, Mod.Find<ModProjectile>("TerraArrow").Type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, Mod.Find<ModProjectile>("TerraArrow").Type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(vector.X, vector.Y, speedX3, speedY3, Mod.Find<ModProjectile>("TerraArrow").Type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(Mod.Find<ModItem>("TerraBallista").Type);
			recipe.AddIngredient(Mod.Find<ModItem>("EXSoul").Type);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
	}
}