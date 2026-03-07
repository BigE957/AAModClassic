using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;


namespace AAModClassic.Tiles.Crafters
{
    public class Transmuter : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            DustType = Mod.Find<ModDust>("MireBubbleDust").Type;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Material Transmuter");
            AddMapEntry(new Color(20, 20, 20), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 4) frame = 0;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = Color.Violet.R;
            g = Color.Violet.G;
            b = Color.Violet.B;
        }


        public static Color lightColor = Color.Violet;

        public Texture2D glowTex = null;

        public static Color GetColor(Color color)
        {
            Color glowColor = Color.White;
            return glowColor;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glowTex == null) glowTex = Mod.GetTexture("Glowmasks/Transmuter_Glow");
            if (glowTex != null && tile != null && tile.HasTile && tile.TileType == Type)
            {
                int width = 16, height = 16;
                int frameX = tile != null && tile.HasTile ? tile.TileFrameX : 0;
                int frameY = tile != null && tile.HasTile ? tile.TileFrameY + (Main.tileFrame[Type] * 54) : 0;
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor);
                for (int m = 0; m < 3; m++)
                {
                    BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor, new Vector2(Main.rand.Next(-3, 4) * 0.5f, Main.rand.Next(-3, 4) * 0.5f));
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 16, Mod.Find<ModItem>("Transmuter").Type);
        }
    }
}