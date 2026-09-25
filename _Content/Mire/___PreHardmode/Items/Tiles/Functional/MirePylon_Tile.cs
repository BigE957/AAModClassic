using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Functional
{
    public class MirePylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<MirePylon>();
        public override Condition ShopCondition => AAConditions.InAnyMire;
        public override bool TeleportBiomeRequirements => AAWorld.mireTiles > 100;
        public override (float, float, float) LightColor => (0.9f, 0.3f, 0.9f);
        public override Color DustColor => new Color(0.7f, 0.1f, 0.7f, 1f);
    }
}
