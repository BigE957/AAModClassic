using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static AAModClassic.Assets.AssetDirectory;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraDresser_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpDresser(ModContent.ItemType<TerraDresser>());
		DustType = DustID.Terra;
	}

    public override LocalizedText DefaultContainerName(int i, int j) => ModContent.GetModItem(ModContent.ItemType<TerraDresser>()).DisplayName;
    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    public override void MouseOver(int i, int j) => FurnitureUtils.DresserMouseOver<TerraDresser>();
    public override void MouseOverFar(int i, int j) => FurnitureUtils.DresserMouseFar<TerraDresser>();
    public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
    public override bool RightClick(int i, int j) => FurnitureUtils.DresserRightClick();
}
