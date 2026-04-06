using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Hooks
{
	internal class GripsOfChaos : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("The Grips of Chaos");
			/* Tooltip.SetDefault(@"Fires 2 different hooks depending on which one is already out
Red has a longer range
Blue pulls in/retracts quicker"); */
		}

		public override void SetDefaults() 
		{
			Item.CloneDefaults(ItemID.SkeletronHand);
			Item.shoot = ModContent.ProjectileType<Projectiles.GripRed>();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<GripRed>())
				{
					Item.shoot = ModContent.ProjectileType<Projectiles.GripBlue>();
				}
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<GripBlue>())
				{
					Item.shoot = ModContent.ProjectileType<Projectiles.GripRed>();
				}
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<Materials.DragonClaw>(), 5);
			recipe.AddIngredient(ModContent.ItemType<HydraClaw>(), 5);
			recipe.AddIngredient(ModContent.ItemType<Materials.IncineriteBar>(), 5);
			recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	internal class GripRed : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Grip");
		}

		public override void SetDefaults() 
		{
			Projectile.CloneDefaults(ProjectileID.SkeletronHand);
		}

		// Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook
		public override bool? CanUseGrapple(Player player) 
		{
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && (Main.projectile[l].type == ModContent.ProjectileType<GripRed>() || Main.projectile[l].type == ModContent.ProjectileType<GripBlue>()))
				{
					hooksOut++;
				}
			}
			if (hooksOut > 1)
			{
				return false;
			}
			return true;
		}

		public override void UseGrapple(Player player, ref int type)
		{
			int hooksOut = 0;
			int oldestHookIndex = -1;
			int oldestHookTimeLeft = 100000;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.whoAmI && Main.projectile[i].type == Projectile.type)
				{
					hooksOut++;
					if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
					{
						oldestHookIndex = i;
						oldestHookTimeLeft = Main.projectile[i].timeLeft;
					}
				}
			}
			if (hooksOut > 1)
			{
				Main.projectile[oldestHookIndex].Kill();
			}
		}

		// Amethyst Hook is 300, Static Hook is 600
		public override float GrappleRange() 
		{
			return 350f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks) 
		{
			numHooks = 2;
		}

		public override void GrappleRetreatSpeed(Player player, ref float speed) 
		{
			speed = 14f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed) 
		{
			speed = 4;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;
			Vector2 distToProj = playerCenter - Projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance)) 
			{
				distToProj.Normalize();                 //get unit vector
				distToProj *= 24f;                      //speed = 24
				center += distToProj;                   //update draw position
				distToProj = playerCenter - center;    //update distance
				distance = distToProj.Length();
				Color drawColor = lightColor;

                //Draw chain
                Main.spriteBatch.Draw(Mod.GetTexture("Items/Hooks/GripRed_Chain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, TextureAssets.Chain30.Value.Width, TextureAssets.Chain30.Value.Height), drawColor, projRotation,
					new Vector2(TextureAssets.Chain30.Value.Width * 0.5f, TextureAssets.Chain30.Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
	}

	internal class GripBlue : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grip");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.SkeletronHand);
		}

		// Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook
		public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && (Main.projectile[l].type == ModContent.ProjectileType<GripRed>() || Main.projectile[l].type == ModContent.ProjectileType<GripBlue>()))
				{
					hooksOut++;
				}
			}
			if (hooksOut > 1)
			{
				return false;
			}
			return true;
		}

		public override void UseGrapple(Player player, ref int type)
		{
			int hooksOut = 0;
			int oldestHookIndex = -1;
			int oldestHookTimeLeft = 100000;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.whoAmI && Main.projectile[i].type == Projectile.type)
				{
					hooksOut++;
					if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
					{
						oldestHookIndex = i;
						oldestHookTimeLeft = Main.projectile[i].timeLeft;
					}
				}
			}
			if (hooksOut > 1)
			{
				Main.projectile[oldestHookIndex].Kill();
			}
		}

		// Amethyst Hook is 300, Static Hook is 600
		public override float GrappleRange()
		{
			return 300f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 2;
		}

		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 16f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 6;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;
			Vector2 distToProj = playerCenter - Projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance))
			{
				distToProj.Normalize();                 //get unit vector
				distToProj *= 24f;                      //speed = 24
				center += distToProj;                   //update draw position
				distToProj = playerCenter - center;    //update distance
				distance = distToProj.Length();
				Color drawColor = lightColor;

                //Draw chain
                Main.spriteBatch.Draw(Mod.GetTexture("Items/Hooks/GripRed_Chain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, TextureAssets.Chain30.Value.Width, TextureAssets.Chain30.Value.Height), drawColor, projRotation,
					new Vector2(TextureAssets.Chain30.Value.Width * 0.5f, TextureAssets.Chain30.Value.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
