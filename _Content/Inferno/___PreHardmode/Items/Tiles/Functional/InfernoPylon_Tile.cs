using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Functional
{
    public class InfernoPylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<InfernoPylon>();
        public override Condition ShopCondition => AAConditions.InAnyInferno;
        public override bool TeleportBiomeRequirements => AAWorld.infernoTiles > 100;
        public override (float, float, float) LightColor => (0.9f, 0.6f, 0.2f);
        public override Color DustColor => new Color(0.7f, 0.5f, 0f, 1f);
    }
}
