using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class Vulcan : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flare of Evil");
		}

		public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.light = 0.8f;
            Projectile.alpha = 100;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 2;
        }

        public override bool OnTileCollide(Vector2 velocity)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            int num102 = Dust.NewDust(new Vector2(Projectile.position.X + Projectile.velocity.X, Projectile.position.Y + Projectile.velocity.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 3f * Projectile.scale);
            Main.dust[num102].noGravity = true;
            Projectile.ai[1] += 1f;

			if (Projectile.ai[1] >= 20f)
			{
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			}

            Projectile.rotation += 0.3f * Projectile.direction;

            if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
				return;
			}
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 420, false);
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("VulcanExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            int DustType = DustID.Torch;
            for (int num583 = 0; num583 < 20; num583++)
            {
                int num584 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustType, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default, 2f * Projectile.scale);
                Main.dust[num584].noGravity = true;
                Main.dust[num584].velocity *= 2f;
                num584 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustType, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default, 1f * Projectile.scale);
                Main.dust[num584].velocity *= 2f;
            }
        }
    }
}
