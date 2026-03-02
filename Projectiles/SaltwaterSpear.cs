using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.Projectiles
{
    public class SaltwaterSpear : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
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
            // DisplayName.SetDefault("Saltwater Spear");
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
                    MovementFactor = 2f; // Make sure the spear moves forward when initially thrown out
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
    }
}
