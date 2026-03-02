using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Toad
{
    public class GlowshroomGrow : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Mushroom");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.damage = 30;
            NPC.defense = 40;
            NPC.lifeMax = 200;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = false;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0]++;
            }
            if (NPC.ai[0] < 600)
            {
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 4;
                }
                else
                {
                    NPC.alpha = 0;
                }
            }
            else
            {
                if (NPC.alpha < 255)
                {
                    NPC.alpha += 5;
                }
                else
                {
                    NPC.active = false;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 5)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y > frameHeight * 4)
            {
                NPC.frame.Y = frameHeight * 4;
            }
        }

        public override bool PreKill()
        {
            return false;
        }
    }
}