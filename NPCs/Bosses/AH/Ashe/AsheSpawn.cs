using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH.Ashe
{
    public class AsheSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashe Akuma");     
            Main.npcFrameCount[NPC.type] = 16;
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

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 10)
            {
                NPC.frameCounter = 0;
                Frame++;
            }
            if (Frame > 15)
            {
                Frame = 12;
            }
            NPC.frame.Y = frameHeight * Frame;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/AsheSpawn_Glow");
            
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 16, NPC.frame, drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 16, NPC.frame, Color.White, true);
            return false;
        }
    }
}
