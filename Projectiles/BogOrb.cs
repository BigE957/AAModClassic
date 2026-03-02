using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{

    public class BogOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.25f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.damage = 10;
            Projectile.scale = 1f;
        }
        public override void AI()
        {
            Projectile.rotation = ((float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f);
            Lighting.AddLight(Projectile.Center, 0.1f, 0.1f, 1f);
                if (Main.rand.Next(2) == 0)
                {
                    Dust.NewDust(Projectile.Center, Projectile.width/2, Projectile.height/2, Mod.Find<ModDust>("AbyssDust").Type, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default, 0.7f);
                }
                float magnitude = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
            if (magnitude > 0.5f)
            {
                    Projectile.velocity.X /= 1.005f;
                    Projectile.velocity.Y /= 1.005f;
            }
                Projectile.velocity.Y += 0.05f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < Main.rand.Next(3) + 5; k++)
                {
                    Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(Main.rand.Next(171) - 85) / 100, (float)(Main.rand.Next(176) - 900) / 100, Mod.Find<ModProjectile>("Drop").Type, Projectile.damage, 2f, Projectile.owner,0f,0f);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) // Want some Venom?
        {
            //target.AddBuff(BuffID.Venom, 180);
        }

    }
}
