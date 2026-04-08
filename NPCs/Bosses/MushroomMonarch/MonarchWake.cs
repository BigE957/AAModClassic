using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using AAModClassic.Music;

namespace AAModClassic.NPCs.Bosses.MushroomMonarch
{
    public class MonarchWake : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Very Large Mushroom...?");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.defense = 0;
            NPC.damage = 0;
            NPC.width = 74;
            NPC.height = 70;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.dontTakeDamage = true;
            NPC.value = 0;
            Music = MusicManagementSystem.MusicSlots["Monarch"];
        }

        int frame = 0;

        public override void AI()
        {
            NPC.velocity.Y += .1f;
            NPC.ai[0]++;

            if (NPC.ai[0] == 30)
            {
                frame += 1;
            }
            if (NPC.ai[0] == 60)
            {
                frame += 1;
            }
            if (NPC.ai[0] == 120)
            {
                frame += 1;
            }
            if (NPC.ai[0] == 160)
            {
                frame += 1;
            }

            NPC.frame.Y = 70 * frame;

            if (NPC.ai[0] == 160 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<MushroomMonarch>());
                NPC.active = false;
                NPC.netUpdate = true;
            }
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }
    }
}
