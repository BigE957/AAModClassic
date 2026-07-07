using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.BossStandard.Relics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard
{
    public class EquinoxWormsRelic_Tile : RelicTile
    {
        public override int PedestalStyle => 1;

        public override int ItemType => ModContent.ItemType<EquinoxWormsRelic>();
    }
}
