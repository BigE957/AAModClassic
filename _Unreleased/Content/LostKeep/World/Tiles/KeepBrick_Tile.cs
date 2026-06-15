using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles;

public class KeepBrick_Tile : ModTile
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
		AddMapEntry(new Color(40, 50, 40), (LocalizedText)null);
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged)
	{
		return false;
    }

    public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => false;

    public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
