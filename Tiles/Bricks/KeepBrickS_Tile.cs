using AAModClassic.Items.Blocks.Bricks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Bricks;

public class KeepBrickS_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileBlendAll[Type] = true;
		HitSound = SoundID.Tink;
		Main.tileBlockLight[Type] = true;
		DustType = DustID.Stone;
		AddMapEntry(new Color(40, 50, 40));
		RegisterItemDrop(ModContent.ItemType<AAModClassic.Items.Blocks.Bricks.KeepBrick>());
	}
}
