using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.BossStandard.Relics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard
{
    public class YamataRelic_Tile : RelicTile
    {
        public override int PedestalStyle => 1;

        public override int ItemType => ModContent.ItemType<YamataRelic>();
    }
}
