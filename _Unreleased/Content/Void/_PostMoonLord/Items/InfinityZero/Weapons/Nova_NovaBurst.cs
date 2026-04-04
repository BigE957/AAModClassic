using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Nova_NovaBurst : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.penetrate = 1;                       //this is the projectile penetration           //this is projectile frames
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;                        //this make the projectile do magic damage
            Projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 180;
            Projectile.aiStyle = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnKill(int timeleft)
        {

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Supernova>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            float num689 = 500f;
            int num690 = -1;
            for (int num691 = 0; num691 < 200; num691++)
            {
                NPC nPC5 = Main.npc[num691];
                if (nPC5.CanBeChasedBy(this, false) && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC5.position, nPC5.width, nPC5.height))
                {
                    float num692 = (nPC5.Center - Projectile.Center).Length();
                    if (num692 < num689)
                    {
                        num690 = num691;
                        num689 = num692;
                    }
                }
            }
        }
    }
}
