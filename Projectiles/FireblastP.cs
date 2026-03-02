using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class FireblastP : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireblast");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
			Projectile.DamageType = DamageClass.Magic;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 120;
        }

        public override void AI()
        {
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
                int num297 = ModContent.DustType<Dusts.DragonflameDust>();
                if (Main.rand.Next(2) == 0)
                {
                    for (int num298 = 0; num298 < 3; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                        if (Main.rand.Next(3) == 0)
                        {
                            Main.dust[num299].noGravity = true;
                            Main.dust[num299].scale *= 3f;
                            Dust expr_DD5D_cp_0 = Main.dust[num299];
                            expr_DD5D_cp_0.velocity.X *= 2f;
                            Dust expr_DD7D_cp_0 = Main.dust[num299];
                            expr_DD7D_cp_0.velocity.Y *= 2f;
                        }
                        Main.dust[num299].scale *= 1f;
                        Dust expr_DDE2_cp_0 = Main.dust[num299];
                        expr_DDE2_cp_0.velocity.X *= 1.2f;
                        Dust expr_DE02_cp_0 = Main.dust[num299];
                        expr_DE02_cp_0.velocity.Y *= 1.2f;
                        Main.dust[num299].scale *= num296;
                        Main.dust[num299].velocity += Projectile.velocity;
                        if (!Main.dust[num299].noGravity)
                        {
                            Main.dust[num299].velocity *= 0.5f;
                        }
                    }
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Mod.Find<ModBuff>("DragonFire").Type, 300);
			Projectile.Kill();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return true;
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int index1 = 0; index1 < 30; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index2].velocity *= 1.4f;
			}
			for (int index1 = 0; index1 < 20; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0.0f, 0.0f, 100, new Color(), 3.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 7f;
				int index3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index3].velocity *= 3f;
			}
			for (int index1 = 0; index1 < 2; ++index1)
			{
				float num2 = 0.4f;
				if (index1 == 1)
				num2 = 0.8f;
				int index2 = Gore.NewGore(new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index2].velocity *= num2;
				++Main.gore[index2].velocity.X;
				++Main.gore[index2].velocity.Y;
				int index3 = Gore.NewGore(new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index3].velocity *= num2;
				--Main.gore[index3].velocity.X;
				++Main.gore[index3].velocity.Y;
				int index4 = Gore.NewGore(new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index4].velocity *= num2;
				++Main.gore[index4].velocity.X;
				--Main.gore[index4].velocity.Y;
				int index5 = Gore.NewGore(new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
				Main.gore[index5].velocity *= num2;
				--Main.gore[index5].velocity.X;
				--Main.gore[index5].velocity.Y;
			}
		}
    }
}