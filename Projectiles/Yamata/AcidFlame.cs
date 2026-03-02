using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
    public class AcidFlame : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Acid Flame");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 45;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0.2f / 255f, (255 - Projectile.alpha) * 0.45f / 255f);
			if (Projectile.timeLeft > 45)
			{
				Projectile.timeLeft = 45;
			}
			if (Projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (Projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (Projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (Projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				Projectile.ai[0] += 1f;
				int num297 = ModContent.DustType<Dusts.YamataDust>();
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 2; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 0.75f);
						if (num297 == 66 && Main.rand.Next(3) == 0)
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 2f;
							Dust expr_DBEF_cp_0 = Main.dust[num299];
							expr_DBEF_cp_0.velocity.X *= 2f;
							Dust expr_DC0F_cp_0 = Main.dust[num299];
							expr_DC0F_cp_0.velocity.Y *= 2f;
						}
						else
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 0.8f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X *= 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y *= 1.2f;
						Main.dust[num299].scale *= num296;
						if (num297 == 66)
						{
							Main.dust[num299].velocity += Projectile.velocity;
						}
					}
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
			}
			Projectile.rotation += 0.3f * Projectile.direction;
			return;	
        }

           public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(target.life<=0)
           {
              Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("AEBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);             
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 15;
            double offsetAngle;
            int i;
            for (i = 0; i < 7; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), Mod.Find<ModProjectile>("AcidBall").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 7f), (float)(-Math.Cos(offsetAngle) * 7f), Mod.Find<ModProjectile>("AcidBall").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
           }
        }
    }
}
