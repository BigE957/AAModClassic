using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepLantern_Tile : ModTile
{
    private static Asset<Texture2D> FlameTexture = null;

    public override void SetStaticDefaults()
    {
        this.SetUpLantern(ModContent.ItemType<TerraLantern>());
        DustType = DustID.Terra;
    }

    public override void HitWire(int i, int j)
    {
        FurnitureUtils.LightHitWire(Type, i, j, 1, 2);
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        if (Main.tile[i, j].TileFrameX < 18)
        {
            r = 0.5f;
            g = 0.5f;
            b = 0.5f;
        }
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => DrawingUtils.DrawSwayingMultiTile(i, j);

    public override void GetTileFlameData(int i, int j, ref TileDrawing.TileFlameData tileFlameData)
    {
        ulong flameSeed = Main.TileFrameSeed ^ (ulong)(((long)i << 32) | (uint)j);
        tileFlameData.flameSeed = flameSeed;
        tileFlameData.flameTexture = (FlameTexture ??= ModContent.Request<Texture2D>(Texture + "_Flame")).Value;
        tileFlameData.flameColor = new Color(100, 100, 100, 0);
        tileFlameData.flameCount = 2;
        tileFlameData.flameRangeXMin = -10;
        tileFlameData.flameRangeXMax = 11;
        tileFlameData.flameRangeYMin = -10;
        tileFlameData.flameRangeYMax = 11;
        tileFlameData.flameRangeMultX = 0f;
        tileFlameData.flameRangeMultY = 0f;
    }

    public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameY == 18 && tile.TileFrameX < 18)
        {
            DrawingUtils.DrawFlameSparks(DustID.Torch, 5, i, j);
        }
    }
}
