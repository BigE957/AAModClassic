using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles;

public class TerraBrickS_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileBlendAll[Type] = true;
		HitSound = SoundID.Tink;
		Main.tileBlockLight[Type] = true;
		base.DustType = DustID.Terra;
		AddMapEntry(new Color(40, 80, 40), (LocalizedText)null);
		RegisterItemDrop(ModContent.ItemType<TerraBrick>());
	}
}
