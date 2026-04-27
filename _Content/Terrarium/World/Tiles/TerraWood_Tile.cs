using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Tiles;

public class TerraWood_Tile : ModTile
{
	public bool glow = true;

	public override void SetStaticDefaults()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileMerge[Type][ModContent.TileType<PermeableTerraWood_Tile>()] = true;
		Main.tileMerge[Type][ModContent.TileType<TerraLeaves_Tile>()] = true;
		Main.tileMerge[Type][ModContent.TileType<TerraCrystal_Tile>()] = true;
		HitSound = SoundID.Tink;
		Main.tileLighted[Type] = true;
		DustType = DustID.Terra;
		AddMapEntry(new Color(52, 200, 0), null);
        RegisterItemDrop(ItemID.Wood);
    }

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		Tile tile = Main.tile[i, j];
		new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			_ = Vector2.Zero;
		}
		_ = tile.TileFrameY;
		_ = 36;
		BaseDrawing.DrawTileTexture(spriteBatch, TextureAssets.Tile[Type].Value, i, j, slopeDraw: true, flipTex: false, ignoreHalfBricks: false, null, AAGlobalTile.GetTerraColorBright);
	}

	public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
	{
		if (glow)
		{
			Color val = BaseUtility.ColorMult(Color.LimeGreen, 1f);
			r = val.R / 255f;
			g = val.G / 255f;
			b = val.B / 255f;
		}
	}
}
