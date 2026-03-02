using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class L85A2 : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("L85A2");
            // Tooltip.SetDefault("Turns bullets into high-velocity bullets!");
        }

		public override void SetDefaults()
		{
			Item.damage = 44;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 24;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 9, 0, 0);
			Item.rare = 7;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
			recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
        
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(01));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, -2);
		}
	}
}
