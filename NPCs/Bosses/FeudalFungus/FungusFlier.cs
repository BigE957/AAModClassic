using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.FeudalFungus
{
    public class FungusFlier : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fungus Flier");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 14;
            NPC.value = Item.sellPrice(0, 0, 0, 0);
            NPC.npcSlots = 0;
            NPC.aiStyle = -1;
            NPC.lifeMax = 5;
            NPC.defense = 0;
            NPC.damage = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = null;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting

            BaseAI.AIFloater(NPC, player, ref NPC.ai, false, 0.2f, 2f, 1.5f, 0.04f, 1.5f, 3);

            if (NPC.wet)
            {
                NPC.life = 0;
            }

            NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 20;
                if (NPC.frame.Y > 40)
                {
                    NPC.frame.Y = 0;
                }
            }
        }
    }
}


