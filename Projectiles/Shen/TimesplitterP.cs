using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles.Shen
{
    public class TimesplitterP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 75;
            Projectile.height = 75;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = ProjAIStyleID.Spear;
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
            // DisplayName.SetDefault("Timesplitter");
        }

        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }

        public override void AI()
        {
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (MovementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    MovementFactor = 3f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
                {
                    MovementFactor -= 2.4f;
                }
                else // Otherwise, increase the movement factor
                {
                    MovementFactor += 2.1f;
                }
            }
            // Change the spear position based off of the velocity and the movementFactor
            Projectile.position += Projectile.velocity * MovementFactor;
            // When we reach the end of the animation, we can kill the spear projectile
            if (projOwner.itemAnimation == 0)
            {
                Projectile.Kill();
            }
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            // Offset by 90 degrees here
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Daybreak, 600);
			target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 600);
            Player player = Main.player[Projectile.owner];
            float screenX = Main.screenPosition.X;
            if (player.direction < 0)
            {
                screenX += Main.screenWidth;
            }

            //change to make more/less projectiles
            float screenY = Main.screenPosition.Y;
            screenY += Main.rand.Next(Main.screenHeight);
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
            Projectile p = Projectile.NewProjectileDirect(new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<CosmicBlow>(), damage*4, 0f, player.whoAmI);
            p.tileCollide = false;
        }
    }
}
