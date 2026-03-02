using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Critters
{
    public class VoidFlowey : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("???");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.knockBackResist = 1f;
            NPC.width = 28;
            NPC.height = 46;
            NPC.lifeMax = 1;
            NPC.immortal = true;
            NPC.friendly = true;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 2)
            {
                NPC.frameCounter = 0;
                Frame += 1;
            }
            else
            {
                if (Frame == 8)
                {
                    //Frame = 0;
                    NPC.active = false;
                }
            }

            NPC.frame.Y = frameHeight * Frame;
        }
    }
}
