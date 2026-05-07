using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.NPCs._BossSagittarius
{
    public class SagRing : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.scale = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 500;
        }
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(in SoundID.Item94, Projectile.position);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, .7f, 0f, 0f);
            SoundEngine.TryGetActiveSound(SlotId.FromFloat(Projectile.localAI[0]), out ActiveSound activeSound);
            if (activeSound != null)
            {
                if (activeSound.Volume == 0f)
                {
                    activeSound.Stop();
                    Projectile.localAI[0] = SlotId.Invalid.ToFloat();
                }
                activeSound.Volume = Math.Max(0f, activeSound.Volume - 0.05f);
            }
            else
            {
                Projectile.localAI[0] = SlotId.Invalid.ToFloat();
            }
            if (Projectile.ai[1] == 1f)
            {
                if (Projectile.alpha < 255)
                {
                    Projectile.alpha += 51;
                }
                if (Projectile.alpha >= 255)
                {
                    Projectile.alpha = 255;
                    Projectile.Kill();
                    return;
                }
            }
            else
            {
                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 50;
                }
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            float num726 = 30f;
            float num727 = num726 * 4f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > num727)
            {
                Projectile.ai[0] = 0f;
            }
            Vector2 vector62 = -Vector2.UnitY.RotatedBy(6.28318548f * Projectile.ai[0] / num726, default);
            float val = 0.75f + vector62.Y * 0.25f;
            float val2 = 0.8f - vector62.Y * 0.2f;
            float num728 = Math.Max(val, val2);
            Projectile.position += new Vector2(Projectile.width, Projectile.height) / 2f;
            Projectile.width = (Projectile.height = (int)(80f * num728));
            Projectile.position -= new Vector2(Projectile.width, Projectile.height) / 2f;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            for (int num729 = 0; num729 < 1; num729++)
            {
                float num730 = 55f * num728;
                float num731 = 11f * num728;
                float num732 = 0.5f;
                int num733 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 0.5f);
                Main.dust[num733].noGravity = true;
                Main.dust[num733].velocity *= 2f;
                Main.dust[num733].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (num731 + num732 * (float)Main.rand.NextDouble() * num730) + Projectile.Center;
                Main.dust[num733].velocity = Main.dust[num733].velocity / 2f + Vector2.Normalize(Main.dust[num733].position - Projectile.Center);
                if (Main.rand.NextBool(2))
                {
                    num733 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 0.9f);
                    Main.dust[num733].noGravity = true;
                    Main.dust[num733].velocity *= 1.2f;
                    Main.dust[num733].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (num731 + num732 * (float)Main.rand.NextDouble() * num730) + Projectile.Center;
                    Main.dust[num733].velocity = Main.dust[num733].velocity / 2f + Vector2.Normalize(Main.dust[num733].position - Projectile.Center);
                }
                if (Main.rand.NextBool(4))
                {
                    num733 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default, 0.7f);
                    Main.dust[num733].noGravity = true;
                    Main.dust[num733].velocity *= 1.2f;
                    Main.dust[num733].position = ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (num731 + num732 * (float)Main.rand.NextDouble() * num730) + Projectile.Center;
                    Main.dust[num733].velocity = Main.dust[num733].velocity / 2f + Vector2.Normalize(Main.dust[num733].position - Projectile.Center);
                }
            }
            return;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color25 = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
            Texture2D texture2D27 = TextureAssets.Projectile[Projectile.type].Value;
            float num242 = 30f;
            float num243 = num242 * 4f;
            float num244 = 6.28318548f * Projectile.ai[0] / num242;
            float num245 = 6.28318548f * Projectile.ai[0] / num243;
            Vector2 vector33 = -Vector2.UnitY.RotatedBy(num244, default);
            float scale6 = 0.75f + vector33.Y * 0.25f;
            float scale7 = 0.8f - vector33.Y * 0.2f;
            int num246 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y10 = num246 * Projectile.frame;
            Vector2 position15 = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture2D27, position15, new Rectangle?(new Rectangle(0, y10, texture2D27.Width, num246)), Projectile.GetAlpha(color25), Projectile.rotation + num245, new Vector2(texture2D27.Width / 2f, num246 / 2f), scale6, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture2D27, position15, new Rectangle?(new Rectangle(0, y10, texture2D27.Width, num246)), Projectile.GetAlpha(color25), Projectile.rotation + (6.28318548f - num245), new Vector2(texture2D27.Width / 2f, num246 / 2f), scale7, spriteEffects, 0f);
            return false;
        }
    }
}