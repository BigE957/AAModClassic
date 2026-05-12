using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

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
            Vector2[] positions = (codable is NPC npc ? npc.oldPos : ((Projectile)codable).oldPos);
            if (positions.Length <= 2 || positions[0] == Vector2.Zero)
                positions = [position, codable.velocity];
            Color lightColor = overrideColor != null ? (Color)overrideColor : Lighting.GetColor(position.ToTileCoordinates());

            DrawAfterimage(sb, texture, positions, frame, lightColor, scale, [rotation], frame.Size() * 0.5f, spriteDirection == -1 ? SpriteEffects.FlipHorizontally : 0, distanceScalar, sizeScalar);
        }

        public static void DrawAfterimage(this SpriteBatch sb, Texture2D texture, Vector2[] positions, Rectangle? frame, Color color, float scale, float[] rotations, Vector2 origin, SpriteEffects effects = 0, float distanceScalar = 1.0F, float sizeScalar = 1f)
        {
            Vector2 velAddon = Vector2.Zero;
            Vector2 originalpos = positions[0];
            int imageCount = positions.Length;
            if (positions.Length <= 2)
                imageCount = 10;

            for (int i = 0; i < imageCount; i++)
            {
                scale *= sizeScalar;
                Color newColor = color * ((imageCount + 3 - i) / (float)(imageCount + 9));
                if (positions.Length > 2)
                {
                    Vector2 position = Vector2.Lerp(originalpos, (i >= positions.Length ? positions[positions.Length - 1] : positions[i]), distanceScalar);
                    float rotation = rotations == null ? 0 : i >= rotations.Length ? rotations[^1] : rotations[i];
                    sb.Draw(texture, position - Main.screenPosition, frame, newColor, rotation, frame.HasValue ? frame.Value.Size() * 0.5f : texture.Size() * 0.5f, scale, effects, 0);
                }
                else
                {
                    Vector2 velocity = positions[1];
                    velAddon += velocity * distanceScalar;
                    float rotation = rotations == null ? 0 : i >= rotations.Length ? rotations[^1] : rotations[i];
                    sb.Draw(texture, originalpos - velAddon - Main.screenPosition, frame, newColor, rotation, frame.HasValue ? frame.Value.Size() * 0.5f : texture.Size() * 0.5f, scale, effects, 0);
                }
            }
        }

    }
}
