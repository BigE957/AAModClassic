using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever;
using AAModClassic.Achievements;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs
{
    public class StormingSiegeSystem : ModSystem
    {
        private static bool StormingSiegeActive = false;

        private static readonly int[] SiegeMechs = new int[3];

        public static void KillSiegeMech(int index) => SiegeMechs[index] = -1;

        public override void PreUpdateEntities()
        {
            if (!StormingSiegeActive)
            {
                bool retriever = false;
                bool orthrus = false;
                bool raider = false;
                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.type == ModContent.NPCType<Retriever>())
                    {
                        retriever = true;
                        SiegeMechs[0] = npc.whoAmI;
                    }
                    else if (npc.type == ModContent.NPCType<OrthrusXBody>())
                    {
                        orthrus = true;
                        SiegeMechs[1] = npc.whoAmI;
                    }
                    else if (npc.type == ModContent.NPCType<RaiderUltima>())
                    {
                        raider = true;
                        SiegeMechs[2] = npc.whoAmI;
                    }

                    if (retriever && orthrus && raider)
                    {
                        StormingSiegeActive = true;
                        break;
                    }
                }
            }
        }

        public override void PostUpdateNPCs()
        {
            if(StormingSiegeActive)
            {
                if (SiegeMechs[0] == -1 && SiegeMechs[1] == -1 && SiegeMechs[2] == -1)
                {
                    StormingSiegeActive = false;
                    StormingSiege.StormingSiegeCondition.Complete();
                    return;
                }

                if (SiegeMechs[0] != -1 && (Main.npc[SiegeMechs[0]] == null || !Main.npc[SiegeMechs[0]].active || Main.npc[SiegeMechs[0]].type != ModContent.NPCType<Retriever>()))
                {
                    StormingSiegeActive = false;
                    return;
                }

                if (SiegeMechs[1] != -1 && (Main.npc[SiegeMechs[1]] == null || !Main.npc[SiegeMechs[1]].active || Main.npc[SiegeMechs[1]].type != ModContent.NPCType<OrthrusXBody>()))
                {
                    StormingSiegeActive = false;
                    return;
                }

                if (SiegeMechs[2] != -1 && (Main.npc[SiegeMechs[2]] == null || !Main.npc[SiegeMechs[2]].active || Main.npc[SiegeMechs[2]].type != ModContent.NPCType<RaiderUltima>()))
                {
                    StormingSiegeActive = false;
                    return;
                }
            }
        }
    }
}
