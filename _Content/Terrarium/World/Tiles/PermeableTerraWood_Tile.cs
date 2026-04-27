using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class PermeableTerraWood_Tile : ModTile
    {

        public bool glow = true; 
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][ModContent.TileType<TerraLeaves_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<TerraCrystal_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<TerraWood_Tile>()] = true;
            TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            DustType = DustID.Terra;
            AddMapEntry(new Color(52, 200, 0));
            RegisterItemDrop(ItemID.Wood);
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            BaseDrawing.DrawTileTexture(spriteBatch, TextureAssets.Tile[Type].Value, i, j, true, false, false, null, AAGlobalTile.GetTerraColorDim);
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(Color.LimeGreen, 1.4f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }
    }
}