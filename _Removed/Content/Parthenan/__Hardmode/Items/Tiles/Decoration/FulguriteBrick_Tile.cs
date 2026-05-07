using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class FulguriteBrick_Tile : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            HitSound = SoundID.Tink;
            DustType = ModContent.DustType<Dusts.FulguriteDust>();
            AddMapEntry(new Color(70, 20, 90
                ));
			MinPick = 200;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(Color.Violet, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = ModContent.Request<Texture2D>("AAModClassic/_Removed/Content/Parthenan/Tiles/Ancient/AncientFulguritePlatingS_Tile_Glow").Value;
                BaseDrawing.DrawTileTexture(spriteBatch, glowTex, x, y, true, false, false, null, AAGlobalTile.GetStormColorDim);
            }
        }
    }
}