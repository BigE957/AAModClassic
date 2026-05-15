using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unreleased
{
    public class AAPlayer_Unreleased : ModPlayer
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
            return ZoneStorm == modOther.ZoneStorm && ZoneShip == modOther.ZoneShip;
        }

        public void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            modOther.ZoneStorm = ZoneStorm;
            modOther.ZoneShip = ZoneShip;
        }

        public void SendCustomBiomes(BinaryWriter bb)
        {
            bb.WriteFlags(ZoneStorm, ZoneShip);
        }

        public void ReceiveCustomBiomes(BinaryReader bb)
        {
            bb.ReadFlags(out ZoneStorm, out ZoneShip);
        }
    }
}
