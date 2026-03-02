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
    public class BinaryReassembler : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            DustType = Mod.Find<ModDust>("DoomDust").Type;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Binary Reassembler");
            AddMapEntry(new Color(40, 0, 0), name);
            disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
            AdjTiles = new int[]
            {
                Mod.Find<ModTile>("ACS").Type,
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
            b = 0f;
        }

        public Texture2D glowTex = null;

        public Color GetColor(Color color)
        {
            Color glowColor = AAColor.ZeroShield;
            return glowColor;
        }


        public Color White(Color color)
        {
            return Color.White;
        }


        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = Mod.GetTexture("Glowmasks/BinaryReassemblerTile_Glow");
            int frameY = tile != null && tile.HasTile ? tile.TileFrameY + (Main.tileFrame[Type] * 54) : 0;

            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.TileFrameX, frameY, false, false, false, null, White);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, Mod.Find<ModItem>("BinaryReassembler").Type);
        }
    }
}