using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Functional
{
    public class VoidPylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<VoidPylon>();
        public override Condition ShopCondition => AAConditions.InAnyVoid;
        public override bool TeleportBiomeRequirements => AAWorld.voidTiles > 20 && Main.LocalPlayer.ZoneSkyHeight || AAWorld.voidTiles > 100 && !Main.LocalPlayer.ZoneSkyHeight;
        public override (float, float, float) LightColor => (1.2f, 0.3f, 0.3f);
        public override Color DustColor => new Color(1.0f, 0.1f, 0.1f, 1f);
    }
}
