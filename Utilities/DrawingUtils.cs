using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void DrawAfterimage(this SpriteBatch sb, Texture2D texture, int shader, Entity codable, float distanceScalar = 1.0F, float sizeScalar = 1.0f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? overrideFrame = null, int overrideFrameCount = 0)
        {
            int frameCount = (overrideFrameCount > 0 ? overrideFrameCount : codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1);
            Rectangle frame = (overrideFrame != null ? (Rectangle)overrideFrame : codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
            float scale = (codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale);
            float rotation = (codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
            int spriteDirection = (codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
            Vector2[] velocities = new Vector2[] { codable.velocity };
            if (useOldPos)
            {
                velocities = (codable is NPC ? ((NPC)codable).oldPos : ((Projectile)codable).oldPos);
            }
            float offsetY2 = (codable is NPC ? ((NPC)codable).gfxOffY : 0f);
            DrawAfterimage(sb, texture, shader, codable.position + new Vector2(0f, offsetY2), codable.width, codable.height, velocities, scale, rotation, spriteDirection, frameCount, frame, distanceScalar, sizeScalar, imageCount, useOldPos, offsetX, offsetY, overrideColor);
        }

        /*
         * Draws the given texture multiple times with each one being farther away and more faded depending on velocity.
         * 
         * oldPoints : an array of points used to draw the afterimage.
         * distanceScalar : How far away from each other each image is.
         * sizeScalar : the amount to scale by for each image. (NOTE: this is ADDITIVE!)
         * fullbright : If the images are fullbright or not.
         * alphaAmt : The amount of alpha to subtract with each image. (0-255)
         * imageCount : How many images to draw.
         * useOldPos : If true, considers the given array as old positions instead of old oldPoints.
         */
        public static void DrawAfterimage(this SpriteBatch sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float distanceScalar = 1.0F, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
        {
            Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
            Color lightColor = overrideColor != null ? (Color)overrideColor : Lighting.GetColor((position + new Vector2(width * 0.5f, height * 0.5f)).ToTileCoordinates());
            Vector2 velAddon = default(Vector2);
            Vector2 originalpos = position;
            Vector2 offset = new Vector2(offsetX, offsetY);
            for (int m = 1; m <= imageCount; m++)
            {
                scale *= sizeScalar;
                Color newLightColor = lightColor;
                newLightColor.R = (byte)(newLightColor.R * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.G = (byte)(newLightColor.G * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.B = (byte)(newLightColor.B * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.A = (byte)(newLightColor.A * (imageCount + 3 - m) / (imageCount + 9));
                if (useOldPos)
                {
                    position = Vector2.Lerp(originalpos, (m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1]), distanceScalar);
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, newLightColor);
                }
                else
                {
                    Vector2 velocity = (m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1]);
                    velAddon += velocity * distanceScalar;
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, newLightColor);
                }
            }
        }

    }
}
