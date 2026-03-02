using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Volley : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Volley");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.light = 2f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.WoodenArrowFriendly;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(Projectile.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            if (Projectile.wet)
            {
                Projectile.Kill();
            }
            if (Projectile.frameCounter++ >= 9)
            {
                Projectile.frameCounter = 0;
                Projectile.frame += 1;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            return true;
        }


        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.Center);
            if (Main.rand.Next(3) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }
    }
}
