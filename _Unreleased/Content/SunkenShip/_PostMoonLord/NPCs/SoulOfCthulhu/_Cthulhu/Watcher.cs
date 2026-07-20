using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu

{
    public class Watcher : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Watcher");
		}

		public override void SetDefaults()
		{
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.alpha -= 40;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] >= 45f)
                {
                    Projectile.localAI[0] = 0f;
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = -Projectile.ai[1];
                    Projectile.netUpdate = true;
                }
                Projectile.velocity.X = Projectile.velocity.RotatedBy((double)Projectile.ai[1], default(Vector2)).X;
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -6f, 6f);
                Projectile.velocity.Y = Projectile.velocity.Y - 0.08f;
                if (Projectile.velocity.Y > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - 0.2f;
                }
                if (Projectile.velocity.Y < -7f)
                {
                    Projectile.velocity.Y = -7f;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] >= 90f)
                {
                    Projectile.localAI[0] = 0f;
                    Projectile.ai[0] = 2f;
                    Projectile.ai[1] = (float)Player.FindClosest(Projectile.position, Projectile.width, Projectile.height);
                    Projectile.netUpdate = true;
                }
                Projectile.velocity.X = Projectile.velocity.RotatedBy((double)Projectile.ai[1], default(Vector2)).X;
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -6f, 6f);
                Projectile.velocity.Y = Projectile.velocity.Y - 0.08f;
                if (Projectile.velocity.Y > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - 0.2f;
                }
                if (Projectile.velocity.Y < -7f)
                {
                    Projectile.velocity.Y = -7f;
                }
            }
            else if (Projectile.ai[0] == 2f)
            {
                Vector2 vector68 = Main.player[(int)Projectile.ai[1]].Center - Projectile.Center;
                if (vector68.Length() < 30f)
                {
                    Projectile.Kill();
                    return;
                }
                vector68.Normalize();
                vector68 *= 14f;
                vector68 = Vector2.Lerp(Projectile.velocity, vector68, 0.6f);
                if (vector68.Y < 6f)
                {
                    vector68.Y = 6f;
                }
                float num784 = 0.4f;
                if (Projectile.velocity.X < vector68.X)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num784;
                    if (Projectile.velocity.X < 0f && vector68.X > 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num784;
                    }
                }
                else if (Projectile.velocity.X > vector68.X)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num784;
                    if (Projectile.velocity.X > 0f && vector68.X < 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num784;
                    }
                }
                if (Projectile.velocity.Y < vector68.Y)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num784;
                    if (Projectile.velocity.Y < 0f && vector68.Y > 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num784;
                    }
                }
                else if (Projectile.velocity.Y > vector68.Y)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num784;
                    if (Projectile.velocity.Y > 0f && vector68.Y < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num784;
                    }
                }
            }
            if (Projectile.alpha < 40)
            {
                int num785 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, ModContent.DustType<CthulhuDust>(), -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 1.2f);
                Main.dust[num785].noGravity = true;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.position);
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 144);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num193 = 0; num193 < 4; num193++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default(Color), 1.5f);
            }
            for (int num194 = 0; num194 < 40; num194++)
            {
                int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num195].noGravity = true;
                Main.dust[num195].velocity *= 3f;
                num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num195].velocity *= 2f;
                Main.dust[num195].noGravity = true;
            }
            if(!Main.dedServ)
                for (int num196 = 0; num196 < 1; num196++)
                {
                    int num197 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position + new Vector2((float)(Projectile.width * Main.rand.Next(100)) / 100f, (float)(Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[num197].velocity *= 0.3f;
                    Gore expr_6EC5_cp_0 = Main.gore[num197];
                    expr_6EC5_cp_0.velocity.X = expr_6EC5_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
                    Gore expr_6EF5_cp_0 = Main.gore[num197];
                    expr_6EF5_cp_0.velocity.Y = expr_6EF5_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
                }
            Projectile.Damage();
        }
    }
}
