using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    public class InfinityZero_InfinityBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinity Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 120 * (Projectile.extraUpdates + 1);
        }

        

        public override void AI()
        {

            Projectile.frameCounter++;
            Vector2 vector14 = Projectile.Center + Projectile.velocity * 3f;
            Lighting.AddLight(vector14, AAColor.Oblivion.ToVector3() * .3f);
            if (Projectile.velocity == Vector2.Zero)
            {
                if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
                {
                    Projectile.frameCounter = 0;
                    bool flag35 = true;
                    for (int num849 = 1; num849 < Projectile.oldPos.Length; num849++)
                    {
                        if (Projectile.oldPos[num849] != Projectile.oldPos[0])
                        {
                            flag35 = false;
                        }
                    }
                    if (flag35)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.Next(Projectile.extraUpdates) == 0)
                {
                    for (int num850 = 0; num850 < 2; num850++)
                    {
                        float num851 = Projectile.rotation + (Main.rand.NextBool(2) ? -1f : 1f) * 1.57079637f;
                        float num852 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector84 = new Vector2((float)Math.Cos((double)num851) * num852, (float)Math.Sin((double)num851) * num852);
                        int num853 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.VoidDust_Unreleased>(), vector84.X, vector84.Y, 0, default, 1f);
                        Main.dust[num853].noGravity = true;
                        Main.dust[num853].scale = 1.2f;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        Vector2 value49 = Projectile.velocity.RotatedBy(1.5707963705062866, default) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                        int num854 = Dust.NewDust(Projectile.Center + value49 - Vector2.One * 4f, 8, 8, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, default, 1.5f);
                        Main.dust[num854].velocity *= 0.5f;
                        Main.dust[num854].velocity.Y = -Math.Abs(Main.dust[num854].velocity.Y);
                        return;
                    }
                }
            }
            else if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
            {
                Projectile.frameCounter = 0;
                float num855 = Projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)Projectile.ai[1]);
                int num856 = 0;
                Vector2 spinningpoint2 = -Vector2.UnitY;
                Vector2 vector85;
                do
                {
                    int num857 = unifiedRandom.Next();
                    Projectile.ai[1] = num857;
                    num857 %= 100;
                    float f = num857 / 100f * 6.28318548f;
                    vector85 = f.ToRotationVector2();
                    if (vector85.Y > 0f)
                    {
                        vector85.Y *= -1f;
                    }
                    bool flag36 = false;
                    if (vector85.Y > -0.02f)
                    {
                        flag36 = true;
                    }
                    if (vector85.X * (Projectile.extraUpdates + 1) * 2f * num855 + Projectile.localAI[0] > 40f)
                    {
                        flag36 = true;
                    }
                    if (vector85.X * (Projectile.extraUpdates + 1) * 2f * num855 + Projectile.localAI[0] < -40f)
                    {
                        flag36 = true;
                    }
                    if (!flag36)
                    {
                        goto IL_230B7;
                    }
                }
                while (num856++ < 100);
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[1] = 1f;
                goto IL_230BF;
                IL_230B7:
                spinningpoint2 = vector85;
                IL_230BF:
                if (Projectile.velocity != Vector2.Zero)
                {
                    Projectile.localAI[0] += spinningpoint2.X * (Projectile.extraUpdates + 1) * 2f * num855;
                    Projectile.velocity = spinningpoint2.RotatedBy((double)(Projectile.ai[0] + 1.57079637f), default) * num855;
                    Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                    return;
                }
            }
        }
        

    }
}
