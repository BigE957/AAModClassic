using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Zero
{
    public class Rocket : ModProjectile
    {
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Rocket");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 30;
            Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
			Projectile.scale = 1f;
			AIType = ProjectileID.Bullet;
        }

		public override void AI()
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), 6, new Color(0, 127, 0, 255), Projectile.scale * 1.5f);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 1.5f;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.damage = (int)(Projectile.damage * 1.25);
			Projectile.width = 70;
			Projectile.height = 70;
			Projectile.timeLeft = 2;
			for (var i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.LifeDrain, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-1f, 1f), 6, new Color(0, 127, 0, 255), Projectile.scale * 1.5f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 1.5f;
			}
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		
		}
    }
}
