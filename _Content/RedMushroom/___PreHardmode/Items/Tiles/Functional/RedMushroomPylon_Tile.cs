using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Functional
{
    public class RedMushroomPylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<RedMushroomPylon>();
        public override Condition ShopCondition => AAConditions.InAnyRedMushroom;
        public override bool TeleportBiomeRequirements => AAWorld.mushTiles > 100;
        public override (float, float, float) LightColor => (0.8f, 0.7f, 0.3f);
        public override Color DustColor => new Color(0.6f, 0.5f, 0.1f, 1f);
    }
}
