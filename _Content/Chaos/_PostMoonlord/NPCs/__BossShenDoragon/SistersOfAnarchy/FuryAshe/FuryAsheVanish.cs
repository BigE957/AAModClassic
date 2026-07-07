using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.FuryAshe
{
    public class FuryAsheVanish : ModNPC
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashe Akuma");     
            Main.npcFrameCount[NPC.type] = 17;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.width = 82;
            NPC.height = 82;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            NPC.velocity.X *= 0.97f;
            NPC.velocity.Y *= 0.97f;

            if (++NPC.frameCounter >= 5)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 88;
                if (NPC.frame.Y > 88 * 13)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask.Value;

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(true), 0);
            return false;
        }
    }
}
