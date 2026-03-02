
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Spark2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
			Projectile.extraUpdates = 1;
        }


        public override void AI()
        {
			int dustType = DustID.Torch;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }
            if (Projectile.ai[0] >= 2f)
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
			if(Main.rand.Next(3) == 0)
			{
				for(int m = 0; m < 3; m++)
				{
					int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = -Projectile.velocity * 0.5f;
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustID2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID2].velocity = -Projectile.velocity * 0.5f;
				Main.dust[dustID2].noLight = false;
				Main.dust[dustID2].noGravity = true;
			}
        }
        public override void OnKill(int timeLeft)
        {
            int dustType = DustID.Torch;
            int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < 15; m++)
			{
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("SparkBoom2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0f);
        }


        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.White.R, Color.White.G, Color.White.B, 120);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 300);
        }
    }
}