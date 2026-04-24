using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    public class IceSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Spike");
            Main.projFrames[Projectile.type] = 30;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.tileCollide = true;
            Projectile.coldDamage = true;
            Projectile.hostile = true;
            Projectile.friendly = false;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 50) { Projectile.velocity.Y += 1; }
            if (Projectile.velocity.Y > 16) { Projectile.velocity.Y = 16; }

            if (Projectile.frameCounter != 1)
            {
                Projectile.frameCounter = 1;
                Projectile.frame = Main.rand.Next(5) * (int)Projectile.ai[1];
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                int dustID = Dust.NewDust(Projectile.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 100, Color.White, 0.8f);
            }
        }
    }
}
