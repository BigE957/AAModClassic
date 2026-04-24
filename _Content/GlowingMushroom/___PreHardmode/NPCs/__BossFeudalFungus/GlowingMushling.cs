using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus
{
    public class GlowingMushling : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Mushling");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 50;
            NPC.damage = 6;
            NPC.defense = 5; 
            NPC.knockBackResist = 1f;
            NPC.value = Item.sellPrice(0, 0, 0, 0);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.width = 30;
            NPC.height = 44;
            NPC.npcSlots = 0f;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.buffImmune[46] = true;
            NPC.buffImmune[47] = true;
            NPC.netAlways = true;
            NPC.scale = 0.5f;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting

            BaseAI.AIZombie(NPC, ref NPC.ai, true, true, -1, .09f, 2, 3, 5, 120, true, 10, 10, true);

            if (NPC.velocity.Y == 0)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 8)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 88;
                }
                if (NPC.frame.Y > 88 * 6)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                NPC.frame.Y = 88 * 6;
            }
        }
    }
}


