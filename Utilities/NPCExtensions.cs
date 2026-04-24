using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public static class NPCExtensions
    {
        public static bool BeenKilled(int type, bool justKilled = false) => Main.BestiaryTracker.Kills.GetKillCount(ContentSamples.NpcsByNetId[type]) > (justKilled ? 1 : 0);

        public static bool BeenKilled(this NPC npc, bool justKilled = false) => BeenKilled(npc.type, justKilled);
        
        public static bool BeenKilled(this ModNPC modNPC, bool justKilled = false) => BeenKilled(modNPC.Type, justKilled);

        public static bool BeenKilled<T>(bool justKilled = false) where T : ModNPC => BeenKilled(ModContent.GetInstance<T>().Type, justKilled);

        /// <summary>
        /// Hides an NPC from the bestiary. This should be called in SetStaticDefaults.
        /// </summary>
        /// <param name="n"></param>
        public static void HideFromBestiary(this ModNPC n)
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(n.Type, value);
        }
    }
}
