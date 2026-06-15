using AAModClassic._Content.Terrarium.World.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles;

public class TerraCrystalBack_Tile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[Type] = false;
		Main.tileBlockLight[Type] = true;
		Main.tileMerge[Type][ModContent.TileType<PermeableTerraWood_Tile>()] = true;
		HitSound = SoundID.Tink;
		Main.tileLighted[Type] = false;
		base.DustType = DustID.Terra;
		AddMapEntry(new Color(39, 125, 37), (LocalizedText)null);
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
