using AAModClassic.Utilities;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;

public class TerraLamp_Tile : ModTile
{
    private static Asset<Texture2D> FlameTexture = null;

	public override void SetStaticDefaults()
	{
		this.SetUpLamp(ModContent.ItemType<TerraLamp>());
        DustType = DustID.Terra;
    }

    public override void HitWire(int i, int j)
    {
        FurnitureCommon.LightHitWire(Type, i, j, 1, 3);
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 0.9f;
			g = 0.9f;
			b = 0.9f;
		}
	}

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        FlameTexture ??= ModContent.Request<Texture2D>(Texture + "_Flame");
        DrawingUtils.DrawFlameEffect(FlameTexture.Value, i, j);
    }
}
