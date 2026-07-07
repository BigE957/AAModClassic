using AAModClassic.Base.BaseMod.Base;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

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
            Vector2 screenPos = Main.screenPosition;
            if (codable is NPC n)
            {
                frameCount = Main.npcFrameCount[n.type];
                frame = n.frame;
                scale = n.scale;
                rotation = n.rotation;
                spriteDirection = n.spriteDirection;
                offsetY2 = n.gfxOffY;
                if (n.IsABestiaryIconDummy)
                    screenPos = Vector2.Zero;
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
            Vector2 position = codable.Center + new Vector2(0f, offsetY2) - screenPos;
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
                sb.Draw(texture, position - velAddon, frame, newColor, rotation, frame.HasValue ? frame.Value.Size() * 0.5f : texture.Size() * 0.5f, scale, effects, 0);
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

        public static void DrawWithVanillaShader(SpriteBatch spriteBatch, int shader, Action<SpriteBatch> action)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);

            GameShaders.Armor.Apply(shader, null, null);
            action.Invoke(spriteBatch);
            
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }

        //Thanks YuH
        public static bool DrawAnimatedBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor, Texture2D headTexture, Texture2D bodyTexture, int segmentCount, int segmentSpacing, float rotationStrength, Vector2 baseOffset, int animationSpeed, float range, float headOffset = 0, float headSpeedOffset = 0, bool flip = false)
        {
            DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, headTexture, [bodyTexture], segmentCount, segmentSpacing, rotationStrength, baseOffset, animationSpeed, range, headOffset, headSpeedOffset, flip);
            return false;
        }

        public static bool DrawAnimatedBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor, Texture2D headTexture, Texture2D bodyTexture, Texture2D bodyTextureAlt, int segmentCount, int segmentSpacing, float rotationStrength, Vector2 baseOffset, int animationSpeed, float range, float headOffset = 0, float headSpeedOffset = 0, bool flip = false)
        {
            DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, headTexture, [bodyTexture, bodyTextureAlt], segmentCount, segmentSpacing, rotationStrength, baseOffset, animationSpeed, range, headOffset, headSpeedOffset, flip);
            return false;
        }

        /// <summary>
        /// Draws animated wiggly worms for the Bestiary
        /// </summary>
        /// <param name="spriteBatch">The PreDraw's SpriteBatch</param>
        /// <param name="npc">The NPC to draw</param>
        /// <param name="drawColor">The NPC's drawColor</param>
        /// <param name="headTexture">The worm's head texture</param>
        /// <param name="bodyTextures">The worm's body textures</param>
        /// <param name="segmentCount">The amount of segments that should be added</param>
        /// <param name="segmentSpacing">The spacing between segments</param>
        /// <param name="rotationStrength">How strongly the worm rotates. Higher values cause it to make sharper turns</param>
        /// <param name="baseOffset">Moves around the position of the worm</param>
        /// <param name="animationSpeed">How fast the worm moves</param>
        /// <param name="range">How far up and down the worm moves</param>
        /// <param name="headOffset">How far to bash (move) the head horizontally in case the automated math is too off or the worm's head extends past its neck joint</param>
        /// <param name="headSpeedOffset">Offsets the animation progression for the head. Meant to pair with headOffset</param>
        /// <param name="flip">If the sprites should be flipped. Used for worms viewed from the side like Wyverns</param>
        /// <returns></returns>
        public static bool DrawAnimatedBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor, Texture2D headTexture, Texture2D[] bodyTextures, int segmentCount, int segmentSpacing, float rotationStrength, Vector2 baseOffset, int animationSpeed, float range, float headOffset = 0, float headSpeedOffset = 0, bool flip = false)
        {
            npc.frame = headTexture.Frame(1, Main.npcFrameCount[npc.type]);
            // Buffers the segment position and rotations
            float offset = -0.2f;
            float startX = baseOffset.X;
            float startY = baseOffset.Y;
            SpriteEffects fx = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            float wormTimer = npc.GetGlobalNPC<BestiaryDrawingNPC>().bestiaryWormTimer;
            // Draw the body segments
            for (int i = segmentCount; i > 0; i--)
            {
                // The first segment is slightly closer to keep up with the head
                float bodyOffset = i == 1 ? i * segmentSpacing * 0.4f : i * segmentSpacing - segmentSpacing * 0.5f;

                // If there's only one texture passed in, use it for all segments
                // If two are passed in, alternate between them
                // If more are passed in, each iteration must correspond to a texture
                Texture2D toUse = bodyTextures.Length == 1 ? bodyTextures[0] : bodyTextures.Length == 2 ? (i % 2 == 0 ? bodyTextures[0] : bodyTextures[1]) : bodyTextures[i - 1];
                spriteBatch.Draw(toUse, npc.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * i) * animationSpeed) * range + startY), toUse.Frame(1, 1, 0, 0), npc.GetAlpha(drawColor), npc.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer + offset * i) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, toUse.Size() / 2, npc.scale, fx, 0f);
            }
            // Draw the head
            spriteBatch.Draw(headTexture, npc.position + new Vector2(startX + headOffset, MathF.Sin((wormTimer - headSpeedOffset) * animationSpeed) * range + startY), npc.frame, npc.GetAlpha(drawColor), npc.rotation - MathHelper.PiOver2 - MathF.Cos((wormTimer - headSpeedOffset) * animationSpeed) * MathHelper.PiOver4 * rotationStrength, npc.frame.Size() * 0.5f, npc.scale, fx, 0f);

            return false;
        }
    }

    public class BestiaryDrawingNPC : GlobalNPC
    {
        public float bestiaryWormTimer = 0;

        public override bool InstancePerEntity => true;

        public override void FindFrame(NPC npc, int frameHeight)
        {
            // Increment the bestiary worm timer when hovering over the NPC or having their entry open. Pauses otherwise
            if (npc.IsABestiaryIconDummy)
            {
                bestiaryWormTimer += 0.02f;
                // Resets after an hour. No sane human being is looking at a bestiary entry for an hour straight
                if (bestiaryWormTimer > 4320)
                    bestiaryWormTimer = 0;
            }
        }
    }
}
