using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushBed_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            this.SetUpBed(ModContent.ItemType<RedmushBed>());
            DustType = ModContent.DustType<MushDust>();
            VanillaFallbackOnModDeletion = TileID.Beds;
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool RightClick(int i, int j) => FurnitureUtils.BedRightClick(i, j);

        public override void MouseOver(int i, int j) => FurnitureUtils.MouseOver(i, j, ModContent.ItemType<RedmushBed>());

        public override void ModifySmartInteractCoords(ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
		{
			width = 2;
			height = 2;
		}

		public override void ModifySleepingTargetInfo(int i, int j, ref TileRestingInfo info)
		{
			info.VisualOffset.Y += 4f;
		}
	}
}