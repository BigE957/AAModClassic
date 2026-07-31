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
    public class TerraLeaves_Tile : ModTile
    {

        public override void SetStaticDefaults()
        {
            MineResist = 2f;
            MinPick = 200;

            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMerge[Type][ModContent.TileType<PermeableTerraWood_Tile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<TerraWood_Tile>()] = true;
            TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;
            HitSound = SoundID.Tink;
            Main.tileLighted[Type] = true;
            DustType = DustID.Terra;
            AddMapEntry(new Color(100, 100, 100));
        }
        
        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            bool glow = true;
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                BaseDrawing.DrawTileTexture(sb, TextureAssets.Tile[Type].Value, x, y, true, false, false, null, AAGlobalTile.GetTerra2ColorDim);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = Color.YellowGreen.R / 255f;
            g = Color.YellowGreen.G / 255f;
            b = Color.YellowGreen.B / 255f;
        }
    }
}