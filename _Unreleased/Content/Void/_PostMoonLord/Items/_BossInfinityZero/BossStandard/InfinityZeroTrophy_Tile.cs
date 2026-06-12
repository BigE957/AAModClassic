using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.BossStandard
{
    public class InfinityZeroTrophy_Tile : ModTile
    {
        private static Asset<Texture2D> glowTex;

        public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);
            DustType = DustID.WoodFurniture;
            TileID.Sets.DisableSmartCursor[Type] = true;
			AddMapEntry(new Color(120, 85, 60));

            glowTex = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Vector2 TileDrawOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            spriteBatch.Draw(glowTex.Value, new Point(x, y).ToWorldCoordinates(1, 0) - Main.screenPosition + TileDrawOffset, new Rectangle(Main.tile[x, y].TileFrameX, Main.tile[x, y].TileFrameY, 16, 16), AAColor.FlashGlow);
        }
	}
}