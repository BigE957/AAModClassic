using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Djinn
{
    class Sandstorm : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 210;
            Projectile.aiStyle = 145;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 600;
            Projectile.localNPCHitCooldown = -1;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            float num = 300f;
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = -1;
                Projectile.localAI[1] = SoundEngine.PlaySound(SoundID.DD2_BookStaffTwisterLoop, Projectile.Center).ToFloat();
            }
            SoundEngine.TryGetActiveSound(SlotId.FromFloat(Projectile.localAI[1]), out ActiveSound activeSound);
            if (activeSound != null)
            {
                activeSound.Position = Projectile.Center;
                activeSound.Volume = 1f - Math.Max(Projectile.ai[0] - (num - 15f), 0f) / 15f;
            }
            else
            {
                Projectile.localAI[1] = SlotId.Invalid.ToFloat();
            }
            if (Projectile.localAI[0] >= 16f && Projectile.ai[0] < num - 15f)
            {
                Projectile.ai[0] = num - 15f;
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= num)
            {
                Projectile.Kill();
            }
            Vector2 top = Projectile.Top;
            Vector2 bottom = Projectile.Bottom;
            Vector2 value = Vector2.Lerp(top, bottom, 0.5f);
            Vector2 value2 = new Vector2(0f, bottom.Y - top.Y);
            value2.X = value2.Y * 0.2f;
            int num2 = 16;
            int num3 = 160;
            for (int i = 0; i < 1; i++)
            {
                Vector2 position = new Vector2(Projectile.Center.X - num2 / 2, Projectile.position.Y + Projectile.height - num3);
                if (Collision.SolidCollision(position, num2, num3) || Collision.WetCollision(position, num2, num3))
                {
                    if (Projectile.velocity.Y > 0f)
                    {
                        Projectile.velocity.Y = 0f;
                    }
                    if (Projectile.velocity.Y > -4f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 2f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 4f;
                        Projectile.localAI[0] += 2f;
                    }
                    if (Projectile.velocity.Y < -16f)
                    {
                        Projectile.velocity.Y = -16f;
                    }
                }
                else
                {
                    Projectile.localAI[0] -= 1f;
                    if (Projectile.localAI[0] < 0f)
                    {
                        Projectile.localAI[0] = 0f;
                    }
                    if (Projectile.velocity.Y < 0f)
                    {
                        Projectile.velocity.Y = 0f;
                    }
                    if (Projectile.velocity.Y < 4f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 2f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 4f;
                    }
                    if (Projectile.velocity.Y > 16f)
                    {
                        Projectile.velocity.Y = 16f;
                    }
                }
            }
            if (Projectile.ai[0] < num - 30f)
            {
                for (int j = 0; j < 1; j++)
                {
                    float value3 = -1f;
                    float value4 = 0.9f;
                    float amount = Main.rand.NextFloat();
                    Vector2 value5 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value3, value4, amount));
                    value5.X *= MathHelper.Lerp(2.2f, 0.6f, amount);
                    value5.X *= -1f;
                    Vector2 value6 = new Vector2(6f, 10f);
                    Vector2 position2 = value + value2 * value5 * 0.5f + value6;
                    Dust dust = Main.dust[Dust.NewDust(position2, 0, 0, 269, 0f, 0f, 0, default, 1f)];
                    dust.position = position2;
                    dust.fadeIn = 1.3f;
                    dust.scale = 0.87f;
                    dust.alpha = 211;
                    if (value5.X > -1.2f)
                    {
                        dust.velocity.X = 1f + Main.rand.NextFloat();
                    }
                    dust.noGravity = true;
                    dust.velocity.Y = Main.rand.NextFloat() * -0.5f - 1.3f;
                    Dust expr_49A_cp_0 = dust;
                    expr_49A_cp_0.velocity.X += Projectile.velocity.X * 2.1f;
                    dust.noLight = true;
                }
            }
            Vector2 position3 = Projectile.Bottom + new Vector2(-25f, -25f);
            for (int k = 0; k < 4; k++)
            {
                Dust dust2 = Dust.NewDustDirect(position3, 50, 25, 269, Projectile.velocity.X, -2f, 100);
                dust2.fadeIn = 1.1f;
                dust2.noGravity = true;
            }
            for (int l = 0; l < 1; l++)
            {
                if (Main.rand.Next(5) == 0)
                {
                    Gore gore = Gore.NewGoreDirect(Projectile.TopLeft + Main.rand.NextVector2Square(0f, 1f) * Projectile.Size, new Vector2(Projectile.velocity.X * 1.5f, -Main.rand.NextFloat() * 16f), Utils.SelectRandom(Main.rand, new int[]
                    {
                        1007,
                        1008,
                        1008
                    }), 1f);
                    gore.timeLeft = 60;
                    gore.alpha = 50;
                    Gore expr_5FA_cp_0 = gore;
                    expr_5FA_cp_0.velocity.X += Projectile.velocity.X;
                }
            }
            for (int m = 0; m < 1; m++)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Gore gore2 = Gore.NewGoreDirect(Projectile.TopLeft + Main.rand.NextVector2Square(0f, 1f) * Projectile.Size, new Vector2(Projectile.velocity.X * 1.5f, -Main.rand.NextFloat() * 16f), Utils.SelectRandom(Main.rand, new int[]
                    {
                        1007,
                        1008,
                        1008
                    }), 1f);
                    gore2.timeLeft = 0;
                    gore2.alpha = 80;
                }
            }
            for (int n = 0; n < 1; n++)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Gore gore3 = Gore.NewGoreDirect(Projectile.TopLeft + Main.rand.NextVector2Square(0f, 1f) * Projectile.Size, new Vector2(Projectile.velocity.X * 1.5f, -Main.rand.NextFloat() * 16f), Utils.SelectRandom(Main.rand, new int[]
                    {
                        1007,
                        1008,
                        1008
                    }), 1f);
                    gore3.timeLeft = 0;
                    gore3.alpha = 80;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.TryGetActiveSound(SlotId.FromFloat(Projectile.localAI[1]), out ActiveSound activeSound);
            if (activeSound != null)
            {
                activeSound.Volume = 0f;
                activeSound.Stop();
            }
        }
        
    }
}