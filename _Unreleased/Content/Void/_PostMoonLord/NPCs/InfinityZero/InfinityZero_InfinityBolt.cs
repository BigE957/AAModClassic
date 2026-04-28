using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
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
            if (Projectile.frameCounter == 0 || Projectile.oldPos[0] == Vector2.Zero)
            {
                for (int num31 = Projectile.oldPos.Length - 1; num31 > 0; num31--)
                {
                    Projectile.oldPos[num31] = Projectile.oldPos[num31 - 1];
                }
                Projectile.oldPos[0] = Projectile.position;
                float theta = Projectile.rotation + 1.57079637f + ((Main.rand.NextBool(2)) ? -1f : 1f) * 1.57079637f;
                float magnitude = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 velocity = new Vector2((float)Math.Cos(theta) * magnitude, (float)Math.Sin(theta) * magnitude);
                int d = Dust.NewDust(Projectile.oldPos[Projectile.oldPos.Length - 1], 0, 0, ModContent.DustType<Dusts.VoidDust_Unreleased>(), velocity.X, velocity.Y, 0);
                Main.dust[d].noGravity = true;
                Main.dust[d].scale = 1.7f;
            }

            Projectile.frameCounter++;
            Vector2 ahead = Projectile.Center + Projectile.velocity * 3f;
            Lighting.AddLight(ahead, AAColor.Oblivion.ToVector3() * .3f);
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
                Vector2 spinningpoint2 = -Vector2.UnitY;
                Vector2 vector85;
                for(int i = 0; i < 100; i++)
                {
                    int rand = Main.rand.Next();
                    Projectile.ai[1] = rand;
                    rand %= 100;
                    float f = rand / 100f * 6.28318548f;
                    vector85 = f.ToRotationVector2();
                    if (vector85.Y > 0f)
                    {
                        vector85.Y *= -1f;
                    }
                    bool remain = false;
                    if (vector85.Y > -0.02f)
                    {
                        remain = true;
                    }
                    if (vector85.X * (Projectile.extraUpdates + 1) * 2f * num855 + Projectile.localAI[0] > 40f)
                    {
                        remain = true;
                    }
                    if (vector85.X * (Projectile.extraUpdates + 1) * 2f * num855 + Projectile.localAI[0] < -40f)
                    {
                        remain = true;
                    }
                    if (!remain)
                    {
                        goto IL_230B7;
                    }
                }
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

        public override bool PreDraw(ref Color lightColor)
        {
            Color color25 = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
            Vector2 end = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D tex3 = TextureAssets.Extra[ExtrasID.CultistLightingArc].Value;
            Projectile.GetAlpha(color25);
            Vector2 scale16 = new Vector2(Projectile.scale) / 2f;
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = Color.DarkRed * 0.5f;
                }
                else if (i == 1)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = Color.Red * 0.5f;
                }
                else
                {
                    scale16 = new Vector2(Projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = Color.White * 0.5f;
                }
                DelegateMethods.f_1 = 1f;

                for (int j = Projectile.oldPos.Length - 1; j > 0; j--)
                {
                    if (!(Projectile.oldPos[j] == Vector2.Zero))
                    {
                        Vector2 start = Projectile.oldPos[j] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = Projectile.oldPos[j - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (Projectile.oldPos[0] != Vector2.Zero)
                {
                    DelegateMethods.f_1 = 1f;
                    Vector2 start2 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }

                Utils.DrawLaser(Main.spriteBatch, tex3, Main.LocalPlayer.Center, Projectile.Center, Vector2.One, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
            }
            return false;
        }
    }
}
