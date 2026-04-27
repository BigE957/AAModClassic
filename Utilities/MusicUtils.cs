using AAModClassic.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic.Utilities
{
    public static class MusicUtils
    {
        public static void InstantSwitchMusic(int musicSlot)
        {
            int previousMusic = Main.curMusic;
            Main.musicFade[previousMusic] = 0f;
            Main.newMusic = Main.curMusic = musicSlot;
            Main.musicFade[Main.curMusic] = 1f;
        }
    }
}
