using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Yamata
{
    public class YamataStorm : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Storm");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 120;
            CooldownSlot = 1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(250, 250, 250, 0);
        }

        public override void AI()
        {
        	Projectile.alpha -= 1;
        	if (Projectile.alpha <= 0)
        	{
        		Projectile.Kill();
        	}
        	Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.9f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0.4f / 255f);
        	Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        	if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			}
        	int num103 = Player.FindClosest(Projectile.Center, 1, 1);
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] < 220f && Projectile.ai[1] > 20f)
			{
				float scaleFactor2 = Projectile.velocity.Length();
				Vector2 vector11 = Main.player[num103].Center - Projectile.Center;
				vector11.Normalize();
				vector11 *= scaleFactor2;
				Projectile.velocity = (Projectile.velocity * 24f + vector11) / 25f;
				Projectile.velocity.Normalize();
				Projectile.velocity *= scaleFactor2;
			}
			if (Projectile.ai[0] < 0f)
			{
				if (Projectile.velocity.Length() < 18f)
				{
					Projectile.velocity *= 1.02f;
				}
			}
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(ModContent.BuffType<HydraToxin>(), 300);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item89);
            float spread = 12f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
	    	double Angle = spread/30f;
	    	double offsetAngle;
	    	int i;
	    	if (Projectile.owner == Main.myPlayer)
	    	{
		    	for (i = 0; i < 10; i++ )
		    	{
		   			offsetAngle = startAngle + Angle * ( i + i * i ) / 2f  + 32f * i;
		        	Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 6f ), (float)( Math.Cos(offsetAngle) * 6f ), ModContent.ProjectileType<YamataRain>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
		        	Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 6f ), (float)( -Math.Cos(offsetAngle) * 6f ), ModContent.ProjectileType<YamataRain>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
		    	}
	    	}
        	for (int dust = 0; dust <= 5; dust++)
        	{
        		Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        	}
        }
    }
}