using AAModClassic.Assets;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus
{
    public abstract class FireProj : ModProjectile
    {
        public readonly struct ColorInterval(Color color, float hold, float transition)
        {
            public readonly Color Color = color;
            public readonly float HoldDuration = hold;
            public readonly float TransitionDuration = transition;
        }

        public readonly struct MulticolorShift(ColorInterval[] colors, Color? final = null)
        {
            private readonly ColorInterval[] Colors = colors;
            private readonly Color? FinalColor = final;

            public readonly Color Evaluate(float interpolant)
            {
                float count = 0;
                for (int i = 0; i < Colors.Length; i++)
                {
                    if (interpolant < count + Colors[i].HoldDuration)
                        return Colors[i].Color;
                    count += Colors[i].HoldDuration;

                    if (interpolant < count + Colors[i].TransitionDuration)
                    {
                        Color current = Colors[i].Color;
                        Color next = (i == Colors.Length - 1) ? FinalColor ?? Color.Transparent : Colors[i + 1].Color;
                        float lerp = Utils.GetLerpValue(0, Colors[i].TransitionDuration, interpolant - count);
                        return Color.Lerp(current, next, lerp);
                    }

                    count += Colors[i].TransitionDuration;
                }
                return FinalColor ?? Color.Transparent;
            }
        }

        public abstract MulticolorShift ColorShift { get; }

        public virtual int DustType => DustID.Torch;

        //TODO: make thise use ExtraTextureDirectory
        public override string Texture => AssetDirectory.Projectiles.FireProj;

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.alpha = 255;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Projectile.extraUpdates = 2;
                Projectile.Center += Projectile.velocity.SafeNormalize(Vector2.Zero) * 80;
            }
        }

        public override void AI()
        {
            //Adapted from Vanilla
            Projectile.localAI[0] += 1f;
            int lifeTime = 60;
            int fadeTime = 12;
            int upTime = lifeTime + fadeTime;
            if (Projectile.localAI[0] >= upTime)
                Projectile.Kill();

            if (Projectile.localAI[0] >= upTime)
                Projectile.velocity *= 0.95f;

            int stickTime = 50;
            int dustTime = stickTime;
            if (Projectile.localAI[0] < dustTime && Main.rand.NextFloat() < 0.25f)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Projectile.localAI[0], 0f, 72f, 0.5f, 1f), 4, 4, DustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                if (Main.rand.NextBool(4))
                {
                    dust.noGravity = true;
                    dust.scale *= 3f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= 2f;
                }
                else
                {
                    dust.scale *= 1.5f;
                }
                dust.scale *= 1.5f;
                dust.velocity *= 1.2f;
                dust.velocity += Projectile.velocity * 1f * Utils.Remap(Projectile.localAI[0], 0f, (float)lifeTime * 0.75f, 1f, 0.1f) * Utils.Remap(Projectile.localAI[0], 0f, (float)lifeTime * 0.1f, 0.1f, 1f);
                dust.customData = 1;
            }
            if (stickTime > 0 && Projectile.localAI[0] >= stickTime && Main.rand.NextFloat() < 0.5f)
            {
                Vector2 center = Main.npc[(int)Projectile.ai[1]].Center;
                Vector2 vector = (Projectile.Center - center).SafeNormalize(Vector2.Zero).RotatedByRandom(0.19634954631328583) * 7f;
                short num7 = 31;
                Dust dust2 = Dust.NewDustDirect(Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) - vector * 2f, 4, 4, num7, 0f, 0f, 150, new Color(80, 80, 80));
                dust2.noGravity = true;
                dust2.velocity = vector;
                dust2.scale *= 1.1f + Main.rand.NextFloat() * 0.2f;
                dust2.customData = -0.3f - 0.15f * Main.rand.NextFloat();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return true;

            Projectile.velocity = oldVelocity * 0.95f;
            Projectile.position -= Projectile.velocity;
            Projectile.timeLeft--;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return false;

            //Adapted from vanilla
            Main.instance.LoadProjectile(ProjectileID.Flames);
            Texture2D texture = TextureAssets.Projectile[ProjectileID.Flames].Value;

            float maxLifetime = 60f;
            float fadeDuration = 12f;
            float totalTime = maxLifetime + fadeDuration;
            float lifetime = Projectile.localAI[0];

            float lifetimeProgress = Utils.Remap(lifetime, 0f, totalTime, 0f, 1f);
            if (lifetimeProgress >= 1f)
                return false;

            float fadeOut = Utils.Remap(lifetime, maxLifetime, totalTime, 1f, 0f);
            float backwardOffset = Math.Min(lifetime, 20f);
            float scale = Utils.Remap(lifetimeProgress, 0.2f, 0.5f, 0.25f, 1f);
            float step = (lifetime > maxLifetime - 10f) ? 0.175f : 0.2f;

            int verticalFrames = 7;
            Rectangle frame = texture.Frame(1, verticalFrames, 0, 3);

            for (int layer = 0; layer < 2; layer++)
            {
                for (float distanceFraction = 1f; distanceFraction >= 0f; distanceFraction -= step)
                {
                    float alphaFactor = (1f - distanceFraction) * Utils.Remap(lifetimeProgress, 0f, 0.2f, 0f, 1f);

                    Color baseColor = ColorShift.Evaluate(lifetimeProgress);
                    Color drawColor = baseColor * alphaFactor;

                    Color altColor = drawColor;
                    altColor.G /= 2;
                    altColor.B /= 2;
                    altColor.A = (byte)Math.Min((int)drawColor.A + (int)(80f * alphaFactor), 255);

                    float rotSpeed = (1f / step) * (distanceFraction + 1f);
                    float time = Main.GlobalTimeWrappedHourly;

                    float rotA = Projectile.rotation + distanceFraction * MathHelper.PiOver2 + time * rotSpeed * 2f;
                    float rotB = Projectile.rotation - distanceFraction * MathHelper.PiOver2 - time * rotSpeed * 2f;

                    Vector2 drawPos = Projectile.Center - Main.screenPosition + Projectile.velocity * (-backwardOffset) * distanceFraction;

                    switch (layer)
                    {
                        case 0:
                            Vector2 offsetPos = drawPos + Projectile.velocity * (-backwardOffset) * step * 0.5f;
                            Main.EntitySpriteDraw(texture, offsetPos, frame, altColor * fadeOut * 0.25f, rotA + MathHelper.PiOver4, frame.Size() / 2f, scale, SpriteEffects.None);
                            Main.EntitySpriteDraw(texture, drawPos, frame, altColor * fadeOut, rotB, frame.Size() / 2f, scale, SpriteEffects.None);
                            break;

                        case 1:
                            offsetPos = drawPos + Projectile.velocity * (-backwardOffset) * step * 0.2f;
                            Main.EntitySpriteDraw(texture, offsetPos, frame, drawColor * fadeOut * 0.25f, rotA + MathHelper.PiOver2, frame.Size() / 2f, scale * 0.75f, SpriteEffects.None);
                            Main.EntitySpriteDraw(texture, drawPos, frame, drawColor * fadeOut, rotB + MathHelper.PiOver2, frame.Size() / 2f, scale * 0.75f, SpriteEffects.None);
                            break;
                    }
                }
            }
            return false;
        }
    }
}
