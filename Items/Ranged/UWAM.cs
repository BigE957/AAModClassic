using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class UWAM : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("U.W.A.M.");
			/* Tooltip.SetDefault("Shoots hundreds of bullets with a very low spread"
			+"\nHave a chance to shoot sharks, dealing 2x damage"
			+"\n88% chance not to consume ammo"
			+"\nS.D.M.G. EX"); */
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.SDMG);
			Item.damage = 85;

			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 4;
			Item.width = 86;
			Item.height = 40;
			Item.useTime = 3;
			Item.useAnimation = 3;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Purple;
			Item.autoReuse = true;
		}
		
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .88;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-16, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextBool(10))
			{
				type = 408;
				speedX *= 4;
				speedY *= 4;
				damage *= 2;
			}
			else
			{
			}
			
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
			Vector2 perturbedSpeed3 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
			float speedX2 = perturbedSpeed2.X;
			float speedY2 = perturbedSpeed2.Y;
			float speedX3 = perturbedSpeed3.X;
			float speedY3 = perturbedSpeed3.Y;
			int p1 = Projectile.NewProjectile(vector.X, vector.Y, speedX2, speedY2, type, damage, knockBack, player.whoAmI);
			int p2 = Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			int p3 = Projectile.NewProjectile(vector.X, vector.Y, speedX3, speedY3, type, damage, knockBack, player.whoAmI);
			if (type == 408)
			{
				Main.projectile[p1].minion = false;
				Main.projectile[p1].DamageType = DamageClass.Ranged;
				Main.projectile[p2].minion = false;
				Main.projectile[p2].DamageType = DamageClass.Ranged;
				Main.projectile[p3].minion = false;
				Main.projectile[p3].DamageType = DamageClass.Ranged;
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SDMG);
			recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}
