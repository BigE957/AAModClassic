using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SThunderBullet : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Supercharged ThunderBullet");
		}
		public override void SetDefaults()
		{
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.light = 0.5f;
            Projectile.alpha = 30;
            Projectile.extraUpdates = 5;
            Projectile.scale = 1.3f;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            DropMeteor();
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //projectile.tileCollide = false;
            //projectile.timeLeft = 20;
            Projectile.ai[0] = 1f;
            return false;
        }

        public void DropMeteor()
        {
            Player player = Main.player[Projectile.owner];
            float num72 = 12f;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            int num112 = 3;
            for (int num113 = 0; num113 < num112; num113++)
            {
                vector2 = new Vector2(player.position.X + (player.width * 0.5f) + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-200, 201);
                vector2.Y -= 100 * num113;
                num78 = Main.mouseX + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
                num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num79 < 0f)
                {
                    num79 *= -1f;
                }
                if (num79 < 20f)
                {
                    num79 = 20f;
                }
                num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
                num80 = num72 / num80;
                num78 *= num80;
                num79 *= num80;
                float num114 = num78;
                float num115 = num79 + (Main.rand.Next(-40, 41) * 0.02f);
                Projectile.NewProjectile(vector2.X, vector2.Y, num114 * 0.75f, num115 * 0.75f, ModContent.ProjectileType<TLThunder>(), Projectile.damage, Projectile.damage, player.whoAmI, 0f, 0.5f + ((float)Main.rand.NextDouble() * 2f));
            }
        }
    }
}