using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.BossStandard
{
    public class SistersOfDiscordRelic_Tile : RelicTile
    {
        public override int PedestalStyle => 1;

        public override int ItemType => ModContent.ItemType<SistersOfDiscordRelic>();
    }
}
