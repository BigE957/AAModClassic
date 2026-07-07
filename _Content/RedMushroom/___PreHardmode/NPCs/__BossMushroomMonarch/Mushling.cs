using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch
{
    public class Mushling : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushling");
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 50;
            NPC.damage = 6;
            NPC.defense = 5; 
            NPC.knockBackResist = 1f;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.aiStyle = -1;
            NPC.width = 30;
            NPC.height = 44;
            NPC.npcSlots = 0f;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.buffImmune[46] = true;
            NPC.buffImmune[47] = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting

            BaseAI.AIZombie(NPC, ref NPC.ai, false, false, -1, .09f, 2, 3, 5, 120, true, 10, 10, true);
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 8)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
                if (NPC.frame.Y > frameHeight * 6)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                NPC.frame.Y = frameHeight * 6;
            }
        }
    }
}


