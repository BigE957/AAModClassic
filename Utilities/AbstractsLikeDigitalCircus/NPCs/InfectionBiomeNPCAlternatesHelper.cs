using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs
{
    public static class InfectionBiomeNPCAlternatesHelper
    {
        public enum InfectionType
        {
            None = 0,
            Corruption = 1,
            Crimson = 2,
            Inferno = 3,
            Mire = 4,
            Hallow = 5
        }

        public static void SetProperFramingForBiome_Horizontal(this NPC npc, int biomeType)
        {
            npc.frame.Width = TextureAssets.Npc[npc.type].Value.Width / 6;
            npc.frame.X = npc.frame.Width * biomeType;
        }
    }
}
