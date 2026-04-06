using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks;

public class KeepBrick_Wall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[((ModWall)this).Type] = true;
		base.DustType = DustID.Stone;
		((ModWall)this).AddMapEntry(new Color(25, 30, 25), (LocalizedText)null);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillWall(int i, int j, ref bool fail)
	{
		fail = true;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
