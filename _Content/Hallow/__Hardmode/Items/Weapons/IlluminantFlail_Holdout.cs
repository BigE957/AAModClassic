using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Weapons
{

    public class IlluminantFlail_Holdout : FlailHoldout
    {
        public float rot = 0;

        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => base.DrawRotationOffset;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.knockBack = 3;

            base.SetDefaults();
        }

        public override void AI()
        {
            //BaseAI.AIFlail(Projectile, ref Projectile.ai, false, 230);
            //Projectile.direction = Projectile.spriteDirection = Main.player[Projectile.owner].direction;

            base.AI();
            /*
            if (CurrentAIState != AIState.Ricochet && CurrentAIState != AIState.Dropping)
            {
                if ((Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) / 2f > 0.52f)
                {
                    rot += (float)Math.PI / 16f;
                }
                else
                {
                    rot *= 0.9f;
                    if (rot < (float)Math.PI / 20f)
                        rot = 0f;
                }
                Projectile.rotation += rot;
            }
            */
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);
            Rectangle? chainSourceRectangle = null;
            // Drippler Crippler customizes sourceRectangle to cycle through sprite frames: sourceRectangle = asset.Frame(1, 6);
            float chainHeightAdjustment = 0f; // Use this to adjust the chain overlap.

            Asset<Texture2D> chainTexture = chainTextures[Type];
            Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
            Vector2 chainDrawPosition = Projectile.Center;
            Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
            Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
            float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
            if (chainSegmentLength == 0)
            {
                chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
            }
            float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
            int chainCount = 0;
            float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (chainLengthRemainingToDraw > 0f)
            {
                // This code gets the lighting at the current tile coordinates
                Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

                // Flaming Mace and Drippler Crippler use code here to draw custom sprite frames with custom lighting.
                // Cycling through frames: sourceRectangle = asset.Frame(1, 6, 0, chainCount % 6);
                // This example shows how Flaming Mace works. It checks chainCount and changes chainTexture and draw color at different values

                var chainTextureToDraw = chainTexture;

                // Here, we draw the chain texture at the coordinates
                Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

                // chainDrawPosition is advanced along the vector back to the player by the chainSegmentLength
                chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
                chainCount++;
                chainLengthRemainingToDraw -= chainSegmentLength;
            }

            // Add a motion trail when moving forward, like most flails do (don't add trail if already hit a tile)
            Texture2D projectileTexture = TextureAssets.Projectile[Type].Value;
            Vector2 drawOrigin = new Vector2(projectileTexture.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (CurrentAIState == AIState.LaunchingForward)
            {
                int afterimageCount = Math.Min(Projectile.oldPos.Length - 1, (int)StateTimer);
                for (int k = afterimageCount; k > 0; k--)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.spriteBatch.Draw(projectileTexture, drawPos, null, color, Projectile.rotation + DrawRotationOffset, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length / 3, spriteEffects, 0f);
                }
            }
            Main.spriteBatch.Draw(projectileTexture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation + DrawRotationOffset, drawOrigin, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
