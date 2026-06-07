using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Crafters
{
    public class ChaosCrucible_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("ChaosCrucible");
            AddMapEntry(new Color(40, 0, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[]
            {
                ModContent.TileType<ACS_Tile>(),
            };
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AlchemyTable];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0;
            b = 0.50f;
        }

        public static Color White(Color color)
        {
            return Color.White;
        }

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D Sphere = ModContent.Request<Texture2D>(Texture + "_Sphere").Value;

            Vector2 TileDrawOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            spriteBatch.Draw(glowTex, new Point(x, y).ToWorldCoordinates(0, 0) - Main.screenPosition + TileDrawOffset, new Rectangle(Main.tile[x, y].TileFrameX, Main.tile[x, y].TileFrameY + (Main.tileFrame[Type] * AnimationFrameHeight), 16, 16), Color.White);
            spriteBatch.Draw(Sphere, new Point(x, y).ToWorldCoordinates(0, 0) - Main.screenPosition + TileDrawOffset, new Rectangle(Main.tile[x, y].TileFrameX, Main.tile[x, y].TileFrameY + (Main.tileFrame[Type] * AnimationFrameHeight), 16, 16), AAGlobalTile.GetShenColorBright(Lighting.GetColor(x, y)));
        }
    }
}