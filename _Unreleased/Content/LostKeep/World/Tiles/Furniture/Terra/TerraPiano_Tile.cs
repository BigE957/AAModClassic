using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static AAModClassic.Assets.AssetDirectory;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraPiano_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
        this.SetUpPiano(ModContent.ItemType<TerraPiano>(), true);
        DustType = DustID.Terra;
	}

	public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}
