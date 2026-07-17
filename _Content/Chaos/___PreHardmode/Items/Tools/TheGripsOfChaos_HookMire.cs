using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items.Tools
{
	public class TheGripsOfChaos_HookMire : ModProjectile
	{
        private static Asset<Texture2D> chainTexture;

        public override void Load()
        {
            chainTexture = ModContent.Request<Texture2D>(Texture + "_Chain");
        }

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grip");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.SkeletronHand);
		}

		public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			foreach (var projectile in Main.ActiveProjectiles)
			{
                if (projectile.owner == Main.myPlayer && (projectile.type == Projectile.type || projectile.type == ModContent.ProjectileType<TheGripsOfChaos_HookInferno>()))
                    hooksOut++;
            }

			return hooksOut <= 1;
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

        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.LocalPlayer.MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - Projectile.Center;
            float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer /= distanceToPlayer; // get unit vector
                directionToPlayer *= chainTexture.Height(); // multiply by chain link length

                center += directionToPlayer; // update draw position
                directionToPlayer = playerCenter - center; // update distance
                distanceToPlayer = directionToPlayer.Length();

                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                // Draw chain
                Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                    chainTexture.Value.Bounds, drawColor, chainRotation,
                    chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }
            // Stop vanilla from drawing the default chain.
            return false;
        }
    }
}
