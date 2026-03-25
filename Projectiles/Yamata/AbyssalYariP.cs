using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Yamata
{
    public class AbyssalYariP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 75;
            Projectile.height = 75;
            Projectile.scale = 1.1f;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 90;
            Projectile.hide = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AbyssalYariP");
        }

        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }

        public override void AI()
        {
            // ai[0] = Speed value of the spear. Changes as time goes by.
            // localAI[0] = Special effect 0-1 flag value. Actived right before the spear goes backward.

            Player player = Main.player[Projectile.owner];
            float itemAnimationMax = Math.Max(1f, player.itemAnimationMax);
            float syncedItemAnimation = AAGlobalProjectile.GetSyncedItemAnimation(Projectile, player);

            // Adjust owner stats based on this projectile
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;

            // Stick to the player
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter);

            // And move outward/inward based on the speed variable.
            Projectile.position += Projectile.velocity * Projectile.ai[0];

            // If we're not movement, start.
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 3f;
                Projectile.netUpdate = true;
            }

            if (syncedItemAnimation < itemAnimationMax / 3f) // Reel back
                Projectile.ai[0] -= 2.4f;
            else // Move forward
                Projectile.ai[0] += 2.1f;

            // If at the end of the animation, kill the projectile.
            //Checking if == 0 is too late, lets the projectile linger into chained item uses.
            if (syncedItemAnimation <= 1f)
                Projectile.Kill();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
            if (Projectile.spriteDirection == -1)
                Projectile.rotation -= MathHelper.PiOver2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < 3; i++)
            {
                float screenX;
                float screenY;
                if (Main.rand.Next(2) == 0)
                {
                    screenX = Main.screenPosition.X;
                    if (Main.rand.Next(2) == 0)
                    {
                        screenX += Main.screenWidth;
                    }
                    screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                }
                else
                {
                    screenY = Main.screenPosition.Y;
                    if (Main.rand.Next(2) == 0)
                    {
                        screenY += Main.screenHeight;
                    }
                    screenX = Main.screenPosition.X;
                    screenX += Main.rand.Next(Main.screenWidth);
                }
                Vector2 vector = new Vector2(screenX, screenY);
                float velocityX = target.Center.X - vector.X;
                float velocityY = target.Center.Y - vector.Y;
                velocityX += Main.rand.Next(-50, 51) * 0.1f;
                velocityY += Main.rand.Next(-50, 51) * 0.1f;
                int num5 = 24;
                float num6 = (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
                num6 = num5 / num6;
                velocityX *= num6;
                velocityY *= num6;
                Projectile p = Projectile.NewProjectileDirect(Projectile.GetSource_OnHit(target), new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<AbyssalYariP2>(), damageDone, 0f, player.whoAmI);
                p.tileCollide = false;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Vector2 origin = Vector2.Zero;
            Main.EntitySpriteDraw(texture, drawPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, 0, 0);
            return false;
        }
    }
}
