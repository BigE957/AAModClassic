using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;

public class KeepChandelier_Tile : ModTile
{
    private static Asset<Texture2D> GlowTexture = null;

    public override void SetStaticDefaults()
    {
        this.SetUpChandelier(ModContent.ItemType<KeepChandelier>());
        DustType = DustID.Stone;
    }

    public override void HitWire(int i, int j) => FurnitureCommon.LightHitWire(Type, i, j, 3, 3);

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 36)
		{
			r = 0.6f;
			g = 0.6f;
			b = 0.6f;
		}
	}

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];

        if (tile.IsTileInvisible && !Main.ShouldShowInvisibleWalls())
            return;

        int xFrameOffset = tile.TileFrameX;
        int yFrameOffset = tile.TileFrameY;

        GlowTexture ??= ModContent.Request<Texture2D>(Texture + "_Flame");
        Texture2D glowmask = GlowTexture.Value;

        Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y - 2) + drawOffest;
        Color drawColour = Color.White;
        if (!tile.IsHalfBlock && tile.Slope == 0)
            spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 18, 18), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
        else if (tile.IsHalfBlock)
            spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
    }

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => DrawingUtils.DrawSwayingMultiTile(i, j);
}
