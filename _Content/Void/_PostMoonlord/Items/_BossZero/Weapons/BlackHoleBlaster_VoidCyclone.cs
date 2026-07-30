using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class BlackHoleBlaster_VoidCyclone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Cyclone");
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 99999;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 255;
            Projectile.scale = .05f;
        }

        public override void AI()
        {
            Projectile.rotation += 0.03f;
            Projectile.velocity *= 0;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.ai[1]++;
            }
            if (Projectile.ai[0] == 0)
            {
                if (Projectile.scale < 1)
                {
                    Projectile.scale += .05f;
                }
                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 5;
                }
                if (Projectile.ai[1] > 240)
                {
                    Projectile.ai[0] = 1;
                    Projectile.ai[1] = 0;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                if (Projectile.scale > 0)
                {
                    Projectile.scale -= .05f;
                }
                if (Projectile.alpha < 255)
                {
                    Projectile.alpha += 5;
                }

                if (Projectile.ai[1] > 30f)
                {
                    Projectile.active = false;
                    Projectile.netUpdate = true;
                }
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];

                if (target.active && Vector2.Distance(Projectile.Center, target.Center) < 6000 && !target.friendly && !target.boss && !NPCID.Sets.ShouldBeCountedAsBoss[target.type])
                {
                    float num3 = 10f;
                    Vector2 vector = new Vector2(target.position.X + target.width / 2, target.position.Y + target.height / 2);
                    float num4 = Projectile.Center.X - vector.X;
                    float num5 = Projectile.Center.Y - vector.Y;
                    float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                    num6 = num3 / num6;
                    num4 *= num6;
                    num5 *= num6;
                    int num7 = 12;
                    target.velocity.X = (target.velocity.X * (num7 - 1) + num4) / num7;
                    target.velocity.Y = (target.velocity.Y * (num7 - 1) + num5) / num7;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, Tex.Width, Tex.Height, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}
