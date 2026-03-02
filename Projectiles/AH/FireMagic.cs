using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.AH
{
    internal class FireMagic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fire Magic");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {

            Projectile.width = 45;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1.1f;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 0;
            Projectile.timeLeft = 255;
            Projectile.tileCollide = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Projectile.velocity *= .98f;
            if (Projectile.timeLeft > 0 && Projectile.velocity == new Vector2(0, 0))
            {
                Projectile.Kill();
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0);

                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }
		
		public override void OnKill (int timeLeft)
		{
			SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
			float spread = 12f * 0.0174f;
			double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
			double deltaAngle = spread / 4;
			for (int i = 0; i < 4; i++)
			{
				double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
				Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f), (float)(Math.Cos(offsetAngle) * 3f), Mod.Find<ModProjectile>("Ash").Type, Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f), (float)(-Math.Cos(offsetAngle) * 3f), Mod.Find<ModProjectile>("Ash").Type, Projectile.damage / 6, Projectile.knockBack, Projectile.owner, 0f, 0f);
			}
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MagicBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            target.AddBuff(Mod.Find<ModBuff>("DragonFire").Type, 600);
            Projectile.active = false;
		}
    }
}