using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.Projectiles.Zero
{
    public class ZeroTaze : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tasershock");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 120 * (Projectile.extraUpdates + 1);
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                projHitbox.X = (int)Projectile.oldPos[i].X;
                projHitbox.Y = (int)Projectile.oldPos[i].Y;
				if (projHitbox.Intersects(targetHitbox))
                {
                    return true;
                }
            }
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[1] < 1f)
            {
                Projectile.localAI[1] += 2f;
                Projectile.position += Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
            }
            return false;
        }

        public override void AI()
        {
            Projectile.numUpdates = Projectile.extraUpdates;
            while (Projectile.numUpdates >= 0)
            {
                Projectile.numUpdates--;
                if (Projectile.frameCounter == 0 || Projectile.oldPos[0] == Vector2.Zero)
                {
                    for (int num31 = Projectile.oldPos.Length - 1; num31 > 0; num31--)
                    {
                        Projectile.oldPos[num31] = Projectile.oldPos[num31 - 1];
                    }
                    Projectile.oldPos[0] = Projectile.position;
                    float num32 = Projectile.rotation + 1.57079637f + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                    float num33 = (float)Main.rand.NextDouble() * 2f + 2f;
                    Vector2 vector2 = new Vector2((float)Math.Cos(num32) * num33, (float)Math.Sin(num32) * num33);
                    int num34 = Dust.NewDust(Projectile.oldPos[Projectile.oldPos.Length - 1], 0, 0, ModContent.DustType<Dusts.VoidDust>(), vector2.X, vector2.Y, 0);
                    Main.dust[num34].noGravity = true;
                    Main.dust[num34].scale = 1.7f;
                }
            }
            Projectile.frameCounter++;
            Lighting.AddLight(Projectile.Center, Color.Magenta.R / 255, Color.Magenta.G / 255, Color.Magenta.B / 255);
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
                        float num851 = Projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num852 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector84 = new Vector2((float)Math.Cos(num851) * num852, (float)Math.Sin(num851) * num852);
                        int num853 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.Discord>(), vector84.X, vector84.Y, 0);
                        Main.dust[num853].noGravity = true;
                        Main.dust[num853].scale = 1.2f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Vector2 value49 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                        int num854 = Dust.NewDust(Projectile.Center + value49 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default, 1.5f);
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
                    Projectile.velocity = spinningpoint2.RotatedBy(Projectile.ai[0] + 1.57079637f) * num855;
                    Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                    return;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color25 = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
            Vector2 end = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D tex3 = Main.extraTexture[33];
            Projectile.GetAlpha(color25);
            Vector2 scale16 = new Vector2(Projectile.scale) / 2f;
            for (int num291 = 0; num291 < 3; num291++)
            {
                if (num291 == 0)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = Color.Red * 0.5f;
                }
                else if (num291 == 1)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = AAColor.Oblivion * 0.5f;
                }
                else
                {
                    scale16 = new Vector2(Projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = Color.Black * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                for (int num292 = Projectile.oldPos.Length - 1; num292 > 0; num292--)
                {
                    if (!(Projectile.oldPos[num292] == Vector2.Zero))
                    {
                        Vector2 start = Projectile.oldPos[num292] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = Projectile.oldPos[num292 - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (Projectile.oldPos[0] != Vector2.Zero)
                {
                    DelegateMethods.f_1 = 1f;
                    Vector2 start2 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
            return false;
        }
    }
}
 