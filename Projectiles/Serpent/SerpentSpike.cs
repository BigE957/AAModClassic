using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Serpent
{
    public class SerpentSpike : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Serpent Spike");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = ProjAIStyleID.Spear;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 60;
            Projectile.hide = true;
        }

        public float MovementFactor
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 100);
        }


        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;

            if (!projOwner.frozen)
            {
                if (MovementFactor == 0f)
                {
                    MovementFactor = 3f;
                    Projectile.netUpdate = true;
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
                {
                    MovementFactor -= 2.4f;
                }
                else
                {
                    MovementFactor += 2.1f;
                }
            }
            
            Projectile.position += Projectile.velocity * MovementFactor;
            
            if (projOwner.itemAnimation == 0)
            {
                Projectile.Kill();
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }
        }
    }
}
