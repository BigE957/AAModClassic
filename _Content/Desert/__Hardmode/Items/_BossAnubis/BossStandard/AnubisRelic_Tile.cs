using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.BossStandard.Relics;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.BossStandard
{
    public class AnubisRelic_Tile : RelicTile
    {
        public override int PedestalStyle => 1;

        public override int ItemType => ModContent.ItemType<AnubisRelic>();
    }
}
