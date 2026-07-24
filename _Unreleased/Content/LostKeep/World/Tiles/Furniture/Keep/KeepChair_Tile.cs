using AAModClassic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepChair_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpChair(ModContent.ItemType<KeepChair>());
        DustType = DustID.Terra;

        VanillaFallbackOnModDeletion = TileID.Chairs;
    }

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
    }

    public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) => FurnitureUtils.ChairSitInfo(i, j, ref info);

    public override bool RightClick(int i, int j) => FurnitureUtils.ChairRightClick(i, j);

    public override void MouseOver(int i, int j) => FurnitureUtils.ChairMouseOver(i, j, ModContent.ItemType<KeepChair>());

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);
}
