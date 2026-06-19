using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard
{
    public class InfernoGripTrophy_Tile : ModTile
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

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Vector2 TileDrawOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            spriteBatch.Draw(glowTex.Value, new Point(x, y).ToWorldCoordinates(1, 0) - Main.screenPosition + TileDrawOffset, new Rectangle(Main.tile[x, y].TileFrameX, Main.tile[x, y].TileFrameY, 16, 16), AAColor.Glow);
        }
    }
}