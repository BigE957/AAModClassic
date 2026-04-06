using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Yamata.Awakened
{
    public class Shockwave2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shockwave");     
            Main.projFrames[Projectile.type] = 6;     
        }

        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 202;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return Projectile.frame == 1 || Projectile.frame == 2;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();

                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y *= 0.00f;
            if (++Projectile.localAI[0] == 6)
                if (Main.netMode != NetmodeID.MultiplayerClient && Projectile.ai[0] != 0)
                {
                    Projectile.ai[0] -= Projectile.ai[0] > 0 ? 1 : -1; //approach 0
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + Vector2.UnitX * Math.Sign(Projectile.ai[0]) * Projectile.width, Vector2.Zero, ModContent.ProjectileType<Shockwave2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0]);
                }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

    }
}
