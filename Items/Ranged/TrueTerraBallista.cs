using AAModClassic.Items.Boss;
using AAModClassic.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
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
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.Purple;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = velocity.RotatedByRandom(MathHelper.ToRadians(3));
			Vector2 perturbedSpeed3 = velocity.RotatedByRandom(MathHelper.ToRadians(3));
			float speedX2 = perturbedSpeed2.X;
			float speedY2 = perturbedSpeed2.Y;
			float speedX3 = perturbedSpeed3.X;
			float speedY3 = perturbedSpeed3.Y;
			Projectile.NewProjectile(source, vector.X, vector.Y, speedX2, speedY2, ModContent.ProjectileType<TerraArrow>(), damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, vector.X, vector.Y, velocity.X, velocity.X, ModContent.ProjectileType<TerraArrow>(), damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, vector.X, vector.Y, speedX3, speedY3, ModContent.ProjectileType<TerraArrow>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ModContent.ItemType<TerraBallista>());
			recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
	}
}