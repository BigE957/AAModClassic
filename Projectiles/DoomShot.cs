using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DoomShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("DoomShot");
            Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			Projectile.friendly = true; 
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 20;
			Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
			AIType = ProjectileID.WoodenArrowFriendly;           
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.ZeroShield;
        }

        public override void PostAI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 7)
            {
                Projectile.frame += 1;
                if (Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeleft)
        {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}
