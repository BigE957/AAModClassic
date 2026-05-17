using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.AwakenedShenAH
{
    public class FuryAsheVanish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashe Akuma");     
            Main.npcFrameCount[NPC.type] = 17;     
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
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 17, NPC.frame, NPC.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 17, NPC.frame, Color.White, true);
            return false;
        }
    }
}
