using AAModClassic.Base.BaseMod.Base;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.FeudalFungus
{
    public class FungusSpore : ModNPC
    {
        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fungal Spore");
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 14;
            NPC.value = BaseUtility.CalcValue(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1;
            NPC.defense = 0;
            NPC.damage = 15;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = null;
            NPC.knockBackResist = 0f;
            NPCID.Sets.NeedsExpertScaling[NPC.type] = false;
        }

        public override void AI()
        {
            if (NPC.ai[0] == 0 && NPC.ai[1] == 0)
            {
                NPC.velocity.X = 5;
            }
            else if (NPC.ai[0] == 1 && NPC.ai[1] == 0)
            {
                NPC.velocity.X = -5;
            }
            else if (NPC.ai[0] == 2 && NPC.ai[1] == 0)
            {
                NPC.velocity.X = 4;
                NPC.velocity.Y = 2.5F;

            }
            else if (NPC.ai[0] == 3 && NPC.ai[1] == 0)
            {
                NPC.velocity.X = -4;
                NPC.velocity.Y = 2.5f;
            }
            NPC.ai[1] = 1;
            
            BaseAI.AISpore(NPC, ref internalAI, 0.1f, 0.02f, 5f, 1f);
            
            if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.velocity *= .96f;
                NPC.scale -= .5f;
                if (NPC.scale <= 0)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }
    }
}


