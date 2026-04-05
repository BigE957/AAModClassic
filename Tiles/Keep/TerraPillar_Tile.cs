using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Keep;

public class TerraPillar_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = false;
		Main.tileSolid[Type] = false;
		Main.tileBlendAll[Type] = false;
		Main.tileMergeDirt[Type] = false;
		HitSound = SoundID.Tink;
		Main.tileLighted[Type] = true;
		base.DustType = DustID.Terra;
		AddMapEntry(Color.DarkGreen, (LocalizedText)null);
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged)
	{
		return false;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
