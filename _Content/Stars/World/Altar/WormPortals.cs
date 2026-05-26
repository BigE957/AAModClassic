using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Altar
{
    public class DBPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bright Star");
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Texture2D DBPortal = ModContent.Request<Texture2D>("AAModClassic/Tiles/Altar/DBPortal").Value;
            Texture2D DBPortalBack = ModContent.Request<Texture2D>("AAModClassic/Tiles/Altar/DBPortalBack").Value;
            Texture2D DBEyes = ModContent.Request<Texture2D>("AAModClassic/Tiles/Altar/DBPortalEyes").Value;

            BaseDrawing.DrawTexture(spriteBatch, DBPortalBack, 0, NPC.position, NPC.width, NPC.height, NPC.scale * 1.2f, NPC.rotation, 0, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spriteBatch, DBPortal, 0, NPC.position, NPC.width, NPC.height, NPC.scale, -NPC.rotation, 0, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spriteBatch, DBEyes, 0, NPC.position, NPC.width, NPC.height, NPC.scale, 0, 0, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);

            return false;
        }

        public override void AI()
        {
            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            MoveToPoint(player.Center - new Vector2(200, 300f));

            if (Vector2.Distance(NPC.Center, player.Center) > 2000)
            {
                NPC.alpha = 255;
                NPC.Center = player.Center - new Vector2(200, 300f);
            }

            NPC.rotation += .1f;

            if (NPC.ai[0] != 1)
            {
                NPC.Center = player.Center - new Vector2(200, 300f);
                NPC.ai[0] = 1;
            }

            NPC.ai[1]++;
            if (NPC.ai[1] >= 1880)
            {
                NPC.timeLeft--;
                NPC.alpha += 5;
            }
            else
            {
                if (NPC.alpha > 100)
                {
                    NPC.alpha -= 3;
                }
                else
                {
                    NPC.alpha = 100;
                }
                return;
            }


            if (NPC.alpha > 255)
            {
                NPC.active = false;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

    }

    public class NCPortal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Void");
            this.HideFromBestiary();
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Texture2D NCPortal = ModContent.Request<Texture2D>("AAModClassic/Tiles/Altar/NCPortal").Value;
            Texture2D NCPortalBack = ModContent.Request<Texture2D>("AAModClassic/Tiles/Altar/NCPortalBack").Value;
            Texture2D NCEyes = ModContent.Request<Texture2D>("AAModClassic/Tiles/Altar/NCPortalEyes").Value;

            BaseDrawing.DrawTexture(spriteBatch, NCPortalBack, 0, NPC.position, NPC.width, NPC.height, NPC.scale * 1.2f, -NPC.rotation, 0, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spriteBatch, NCPortal, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spriteBatch, NCEyes, 0, NPC.position, NPC.width, NPC.height, NPC.scale, 0, 0, 1, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);

            return false;
        }

        public override void AI()
        {
            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            MoveToPoint(player.Center - new Vector2(-200, 300f));

            if (Vector2.Distance(NPC.Center, player.Center) > 2000)
            {
                NPC.alpha = 255;
                NPC.Center = player.Center - new Vector2(-200, 300f);
            }

            NPC.rotation += .1f;

            if (NPC.ai[0] != 1)
            {
                NPC.Center = player.Center - new Vector2(-200, 300f);
                NPC.ai[0] = 1;
            }

            NPC.ai[1]++;
            if (NPC.ai[1] >= 1880)
            {
                NPC.alpha += 5;
            }
            else
            {
                if (NPC.alpha > 100)
                {
                    NPC.alpha -= 3;
                }
                else
                {
                    NPC.alpha = 100;
                }
                return;
            }

            if (NPC.alpha > 255)
            {
                NPC.active = false;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

    }
}