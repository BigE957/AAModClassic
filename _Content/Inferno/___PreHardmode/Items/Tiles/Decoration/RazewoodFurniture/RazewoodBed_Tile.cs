using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture
{
    public class RazewoodBed_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            this.SetUpBed(ModContent.ItemType<RazewoodBed>());
            DustType = ModContent.DustType<RazewoodDust>();
            VanillaFallbackOnModDeletion = TileID.Beds;
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool RightClick(int i, int j) => FurnitureCommon.BedRightClick(i, j);

        public override void MouseOver(int i, int j) => FurnitureCommon.MouseOver(i, j, ModContent.ItemType<RazewoodBed>());
    }
}