using AAModClassic.Buffs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Removed
{
    public class AAPlayerRemoved : ModPlayer
    {
        public bool ZoneStorm = false;
        public bool ZoneShip = false;

        public override void Initialize()
        {
            ZoneStorm = false;
            ZoneShip = false;
        }

        public bool CustomBiomesMatch(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            return ZoneStorm == modOther.ZoneStorm &&
                ZoneShip == modOther.ZoneShip;
        }

        public void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            modOther.ZoneStorm = ZoneStorm;
            modOther.ZoneShip = ZoneShip;
        }

        public void SendCustomBiomes(BinaryWriter bb)
        {
            BitsByte zoneByte = 0;
            zoneByte[0] = ZoneStorm;
            zoneByte[1] = ZoneShip;
            //zoneByte[2] = ZoneVoid;
            //zoneByte[3] = ZoneMush;
            //zoneByte[4] = Terrarium;
            //zoneByte[5] = ZoneStorm;
            //zoneByte[6] = ZoneRisingSunPagoda;
            //zoneByte[7] = ZoneRisingMoonLake;
            bb.Write(zoneByte);
        }

        public void ReceiveCustomBiomes(BinaryReader bb)
        {
            BitsByte zoneByte = bb.ReadByte();
            ZoneStorm = zoneByte[0];
            ZoneShip = zoneByte[1];
            //ZoneVoid = zoneByte[2];
            //ZoneMush = zoneByte[3];
            //Terrarium = zoneByte[4];
            //ZoneStorm = zoneByte[5];
            //ZoneRisingSunPagoda = zoneByte[6];
            //ZoneRisingMoonLake = zoneByte[7];
        }
    }
}
