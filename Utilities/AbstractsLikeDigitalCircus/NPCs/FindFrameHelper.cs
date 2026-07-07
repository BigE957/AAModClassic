using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs
{
    public static class FindFrameHelper
    {
        /// <summary>
        /// Handles the framing for "Hostile Frog" enemies, like Tiny Toads and Fungus Frogs.
        /// </summary>
        /// <param name="npc"></param>
        public static void FrameHandler_HostileFrog(this NPC npc, int frameHeight)
        {
            if (npc.velocity.Y < 0)
                npc.frame.Y = frameHeight * 4;
            else if (npc.velocity.Y > 0)
                npc.frame.Y = frameHeight * 5;
            else if (npc.ai[0] < -15f)
                npc.frame.Y = 0;
            else if (npc.ai[0] > -15f)
                npc.frame.Y = frameHeight;
            else if (npc.ai[0] > -10f)
                npc.frame.Y = frameHeight * 2;
            else if (npc.ai[0] > -5f)
                npc.frame.Y = frameHeight * 3;
        }
    }
}
