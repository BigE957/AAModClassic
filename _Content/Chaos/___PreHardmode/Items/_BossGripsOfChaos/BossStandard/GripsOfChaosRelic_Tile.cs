using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard
{
    public class GripsOfChaosRelic_Tile : RelicTile
    {
        public override int PedestalStyle => 1;

        public override int ItemType => ModContent.ItemType<GripsOfChaosRelic>();
    }
}
