using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushCouch_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpSofa(ModContent.ItemType<RedmushCouch>());
            DustType = ModContent.DustType<MushDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureUtils.BenchMouseOver(i, j, ModContent.ItemType<RedmushCouch>());

        public override void NumDust(int i, int j, bool fail, ref int num) => num = (fail ? 1 : 3);

        public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) => FurnitureUtils.BenchSitInfo(i, j, ref info);

        public override bool RightClick(int i, int j) => FurnitureUtils.ChairRightClick(i, j);

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);
    }
}