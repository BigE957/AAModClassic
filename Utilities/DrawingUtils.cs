using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace AAModClassic.Utilities
{
    public static class DrawingUtils
    {
        public static void DrawAura(this SpriteBatch sb, Texture2D texture, Entity codable, float auraPercent, float distanceScalar = 1f, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, bool centered = false)
        {
            int frameCount;
            Rectangle frame;
            float scale;
            float rotation;
            int spriteDirection;
            float offsetY2;
            if (codable is NPC n)
            {
                frameCount = Main.npcFrameCount[n.type];
                frame = n.frame;
                scale = n.scale;
                rotation = n.rotation;
                spriteDirection = n.spriteDirection;
                offsetY2 = n.gfxOffY;
            }
            else
            {
                Projectile p = codable as Projectile;
                frameCount = Main.projFrames[p.type];
                frame = new Rectangle(0, p.frame * texture.Width / frameCount, texture.Height, texture.Width / frameCount);
                scale = p.scale;
                rotation = p.rotation;
                spriteDirection = p.spriteDirection;
                offsetY2 = p.gfxOffY;
            }
            Vector2 position = codable.Center + new Vector2(0f, offsetY2) - Main.screenPosition;
            Color lightColor = overrideColor != null ? (Color)overrideColor : Lighting.GetColor(position.ToTileCoordinates());
            DrawAura(sb, texture, position, frame, lightColor, rotation, frame.Size() * 0.5f, scale, spriteDirection == -1 ? SpriteEffects.FlipHorizontally : 0, auraPercent, distanceScalar);
        }

        public static void DrawAura(this SpriteBatch sb, Texture2D texture, Vector2 position, Rectangle? frame, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float auraPercent, float distanceScalar = 1f)
        {
            float percentHalf = auraPercent * 5f * distanceScalar;
            float percentLight = MathHelper.Lerp(0.8f, 0.2f, auraPercent);
            color *= percentLight;
            for (int m = 0; m < 4; m++)
            {
                float offX = 0;
                float offY = 0;
                switch (m)
                {
                    case 0: offX += percentHalf; break;
                    case 1: offX -= percentHalf; break;
                    case 2: offY += percentHalf; break;
                    case 3: offY -= percentHalf; break;
                }
                Vector2 offsetPos = new Vector2(position.X + offX, position.Y + offY);
                sb.Draw(texture, offsetPos, frame, color, rotation, origin, scale, effects, 0);
            }
        }

        /*
         * Draws the given texture multiple times with each one being farther away and more faded depending on velocity.
         * Uses a Entity(NPC/Projectile) for width, height, position, rotation, sprite direction, and velocity. If an npc, also uses framecount and frame.
         */
        public static void DrawAfterimage(this SpriteBatch sb, Texture2D texture, Entity codable, float distanceScalar = 1.0F, float sizeScalar = 1.0f, Color? overrideColor = null)
        {
            int frameCount;
            Rectangle frame;
            float scale;
            float rotation;
            int spriteDirection;
            float offsetY2;
            if (codable is NPC n)
            {
                frameCount = Main.npcFrameCount[n.type];
                frame = n.frame;
                scale = n.scale;
                rotation = n.rotation;
                spriteDirection = n.spriteDirection;
                offsetY2 = n.gfxOffY;
            }
            else
            {
                Projectile p = codable as Projectile;
                frameCount = Main.projFrames[p.type];
                frame = new Rectangle(0, p.frame * texture.Width / frameCount, texture.Height, texture.Width / frameCount);
                scale = p.scale;
                rotation = p.rotation;
                spriteDirection = p.spriteDirection;
                offsetY2 = p.gfxOffY;
            }
            Vector2 position = codable.Center + new Vector2(0f, offsetY2);
            Color lightColor = overrideColor != null ? (Color)overrideColor : Lighting.GetColor(position.ToTileCoordinates());
            Vector2[] positions = (codable is NPC npc ? npc.oldPos : ((Projectile)codable).oldPos);
            if (positions.Length <= 2 || positions[0] == Vector2.Zero)
                DrawAfterimageWithVelocity(sb, texture, position, codable.velocity, 10, frame, lightColor, scale, [rotation], frame.Size() * 0.5f, spriteDirection == -1 ? SpriteEffects.FlipHorizontally : 0, distanceScalar, sizeScalar);
            else
                DrawAfterimage(sb, texture, positions, frame, lightColor, scale, [rotation], frame.Size() * 0.5f, spriteDirection == -1 ? SpriteEffects.FlipHorizontally : 0, distanceScalar, sizeScalar);
        }

        public static void DrawAfterimage(this SpriteBatch sb, Texture2D texture, Vector2[] positions, Rectangle? frame, Color color, float scale, float[] rotations, Vector2 origin, SpriteEffects effects = 0, float distanceScalar = 1.0F, float sizeScalar = 1f)
        {
            Vector2 velAddon = Vector2.Zero;
            Vector2 originalpos = positions[0];
            int imageCount = positions.Length;

            for (int i = 0; i < imageCount; i++)
            {
                scale *= sizeScalar;
                Color newColor = color * ((imageCount + 3 - i) / (float)(imageCount + 9));
                Vector2 position = Vector2.Lerp(originalpos, (i >= positions.Length ? positions[positions.Length - 1] : positions[i]), distanceScalar);
                float rotation = rotations == null ? 0 : i >= rotations.Length ? rotations[^1] : rotations[i];
                sb.Draw(texture, position - Main.screenPosition, frame, newColor, rotation, frame.HasValue ? frame.Value.Size() * 0.5f : texture.Size() * 0.5f, scale, effects, 0);
            }
        }

        public static void DrawAfterimageWithVelocity(this SpriteBatch sb, Texture2D texture, Vector2 position, Vector2 velocity, int imageCount, Rectangle? frame, Color color, float scale, float[] rotations, Vector2 origin, SpriteEffects effects = 0, float distanceScalar = 1.0F, float sizeScalar = 1f)
        {
            Vector2 velAddon = Vector2.Zero;

            for (int i = 0; i < imageCount; i++)
            {
                scale *= sizeScalar;
                Color newColor = color * ((imageCount + 3 - i) / (float)(imageCount + 9));
                velAddon += velocity * distanceScalar;
                float rotation = rotations == null ? 0 : i >= rotations.Length ? rotations[^1] : rotations[i];
                sb.Draw(texture, position - velAddon - Main.screenPosition, frame, newColor, rotation, frame.HasValue ? frame.Value.Size() * 0.5f : texture.Size() * 0.5f, scale, effects, 0);
            }
        }

        public static void DrawCenteredAfterimages(Projectile proj, int mode, Color lightColor, int typeOneIncrement = 1, Texture2D texture = null, bool drawCentered = true)
        {
            texture ??= TextureAssets.Projectile[proj.type].Value;

            int num = texture.Height / Main.projFrames[proj.type];
            int y = num * proj.frame;
            float scale = proj.scale;
            float rotation = proj.rotation;
            Rectangle rectangle = new(0, y, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            SpriteEffects effects = SpriteEffects.None;
            if (proj.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }

            bool flag = false;
            Vector2 vector = (drawCentered ? (proj.Size / 2f) : Vector2.Zero);
            Color alpha = proj.GetAlpha(lightColor);
            switch (mode)
            {
                case 0:
                    {
                        for (int j = 0; j < proj.oldPos.Length; j++)
                        {
                            Vector2 position2 = proj.oldPos[j] + vector - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
                            Color color2 = alpha * ((float)(proj.oldPos.Length - j) / (float)proj.oldPos.Length);
                            Main.spriteBatch.Draw(texture, position2, rectangle, color2, rotation, origin, scale, effects, 0f);
                        }

                        break;
                    }
                case 1:
                    {
                        int num2 = Math.Max(1, typeOneIncrement);
                        Color color3 = alpha;
                        int num3 = ProjectileID.Sets.TrailCacheLength[proj.type];
                        float num4 = (float)num3 * 1.5f;
                        for (int k = 0; k < num3; k += num2)
                        {
                            Vector2 position3 = proj.oldPos[k] + vector - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
                            if (k > 0)
                            {
                                float num5 = num3 - k;
                                color3 *= num5 / num4;
                            }

                            Main.spriteBatch.Draw(texture, position3, rectangle, color3, rotation, origin, scale, effects, 0f);
                        }

                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < proj.oldPos.Length; i++)
                        {
                            float rotation2 = proj.oldRot[i];
                            SpriteEffects effects2 = ((proj.oldSpriteDirection[i] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                            Vector2 position = proj.oldPos[i] + vector - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
                            Color color = alpha * ((float)(proj.oldPos.Length - i) / (float)proj.oldPos.Length);
                            Main.spriteBatch.Draw(texture, position, rectangle, color, rotation2, origin, scale, effects2, 0f);
                        }

                        break;
                    }
                default:
                    flag = true;
                    break;
            }

            if (ProjectileID.Sets.TrailCacheLength[proj.type] <= 0 || flag)
            {
                Vector2 vector2 = (drawCentered ? proj.Center : proj.position);
                Main.spriteBatch.Draw(texture, vector2 - Main.screenPosition + new Vector2(0f, proj.gfxOffY), rectangle, proj.GetAlpha(lightColor), rotation, origin, scale, effects, 0f);
            }
        }

        /// <summary>
        /// Draws a projectile as a series of afterimages. The first of these afterimages is centered on the center of the projectile's hitbox.<br />
        /// This function is guaranteed to draw the projectile itself, even if it has no afterimages and/or the Afterimages config option is turned off.
        /// </summary>
        /// <param name="proj">The projectile to be drawn.</param>
        /// <param name="mode">The type of afterimage drawing code to use. Vanilla Terraria has three options: 0, 1, and 2.</param>
        /// <param name="lightColor">The light color to use for the afterimages.</param>
        /// <param name="typeOneIncrement">If mode 1 is used, this controls the loop increment. Set it to more than 1 to skip afterimages.</param>
        /// <param name="texture">The texture to draw. Set to <b>null</b> to draw the projectile's own loaded texture.</param>
        /// <param name="drawCentered">If <b>false</b>, the afterimages will be centered on the projectile's position instead of its own center.</param>
        public static void DrawCenteredAfterimages(NPC npc, int mode, Color lightColor, int typeOneIncrement = 1, Texture2D texture = null, bool drawCentered = true)
        {
            texture ??= TextureAssets.Npc[npc.type].Value;
            float scale = npc.scale;
            float rotation = npc.rotation;
            Rectangle frame = npc.frame;
            Vector2 origin = frame.Size() / 2f;
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }

            bool flag = false;
            Vector2 vector = (drawCentered ? (npc.Size / 2f) : Vector2.Zero);
            Color alpha = npc.GetAlpha(lightColor);
            switch (mode)
            {
                case 0:
                    {
                        for (int j = 0; j < npc.oldPos.Length; j++)
                        {
                            Vector2 position2 = npc.oldPos[j] + vector - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
                            Color color2 = alpha * ((float)(npc.oldPos.Length - j) / (float)npc.oldPos.Length);
                            Main.spriteBatch.Draw(texture, position2, frame, color2, rotation, origin, scale, effects, 0f);
                        }

                        break;
                    }
                case 1:
                    {
                        int num2 = Math.Max(1, typeOneIncrement);
                        Color color3 = alpha;
                        int num3 = NPCID.Sets.TrailCacheLength[npc.type];
                        float num4 = (float)num3 * 1.5f;
                        for (int k = 0; k < num3; k += num2)
                        {
                            Vector2 position3 = npc.oldPos[k] + vector - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
                            if (k > 0)
                            {
                                float num5 = num3 - k;
                                color3 *= num5 / num4;
                            }

                            Main.spriteBatch.Draw(texture, position3, frame, color3, rotation, origin, scale, effects, 0f);
                        }

                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < npc.oldPos.Length; i++)
                        {
                            float rotation2 = npc.oldRot[i];
                            SpriteEffects effects2 = ((npc.spriteDirection == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                            Vector2 position = npc.oldPos[i] + vector - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
                            Color color = alpha * ((float)(npc.oldPos.Length - i) / (float)npc.oldPos.Length);
                            Main.spriteBatch.Draw(texture, position, frame, color, rotation2, origin, scale, effects2, 0f);
                        }

                        break;
                    }
                default:
                    flag = true;
                    break;
            }

            if (NPCID.Sets.TrailCacheLength[npc.type] <= 0 || flag)
            {
                Vector2 vector2 = (drawCentered ? npc.Center : npc.position);
                Main.spriteBatch.Draw(texture, vector2 - Main.screenPosition + new Vector2(0f, npc.gfxOffY), frame, npc.GetAlpha(lightColor), rotation, origin, scale, effects, 0f);
            }
        }
    }
}
