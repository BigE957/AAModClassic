using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons
{
    public class SandLamp_Spray : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spray");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 6;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, (255 - Projectile.alpha) * 0.3f / 255f, (255 - Projectile.alpha) * 0.3f / 255f, (255 - Projectile.alpha) * 0f / 255f);
			Projectile.scale -= 0.002f;
			if (Projectile.scale <= 0f)
			{
				Projectile.Kill();
			}
			if (Projectile.ai[0] <= 3f)
			{
				Projectile.ai[0] += 1f;
				return;
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.075f;
			for (int i = 0; i < 3; i++)
			{
				float xPos = Projectile.velocity.X / 3f * i;
				float yPos = Projectile.velocity.Y / 3f * i;
				int eggroll = 14;
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X + eggroll, Projectile.position.Y + eggroll), Projectile.width - eggroll * 2, Projectile.height - eggroll * 2, DustID.Sandnado, 0f, 0f, 100);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 0.1f;
				Main.dust[dustIndex].velocity += Projectile.velocity * 0.5f;
				Dust sand = Main.dust[dustIndex];
				sand.position.X -= xPos;
				Dust sand2 = Main.dust[dustIndex];
				sand2.position.Y -= yPos;
			}
			if (Main.rand.NextBool(8))
			{
				int eggplant = 16;
				int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X + eggplant, Projectile.position.Y + eggplant), Projectile.width - eggplant * 2, Projectile.height - eggplant * 2, DustID.Sandnado, 0f, 0f, 100, default, 0.5f);
				Main.dust[dustIndex2].velocity *= 0.25f;
				Main.dust[dustIndex2].velocity += Projectile.velocity * 0.5f;
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.immune[Projectile.owner] = 8;
        }
    }
}