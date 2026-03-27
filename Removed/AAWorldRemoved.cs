using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic.Removed
{
    public class AAWorldRemoved : ModSystem
    {
        public static bool doRemovedContent; // has no function but u can see where removed content is placed elsewhere

        public static bool downedSoC;
        public static bool downedIZ;

        public static bool Anticheat = true;

        #region stupid bullshit
        public override void PreWorldGen()
        {
            downedSoC = false;
            downedIZ = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downedRemoved = new List<string>();
            if (downedSoC) downedRemoved.Add("SoC");
            if (downedIZ) downedRemoved.Add("IZ");

            tag.Add("downedRemoved", downedRemoved);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downedRemoved = tag.GetList<string>("downedRemoved");
            downedSoC = downedRemoved.Contains("SoC");
            downedIZ = downedRemoved.Contains("IZ");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedSoC;
            flags[1] = downedIZ;
            //flags[2] = downedIZ;
            //flags[3] = downedIZ;
            //flags[4] = downedIZ;
            //flags[5] = downedIZ;
            //flags[6] = downedIZ;
            //flags[7] = downedIZ;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedSoC = flags[0];
            downedIZ = flags[1];
            //downedIZ = flags[2];
            //downedIZ = flags[3];
            //downedIZ = flags[4];
            //downedIZ = flags[5];
            //downedIZ = flags[6];
            //downedIZ = flags[7];
        }
        #endregion
    }
}
