using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Blocks.Bricks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Tiles.Bricks
{
    class EquinoxBrick_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<EquinoxBrick>());   
            AddMapEntry(Color.DarkGoldenrod);
            DustType = ModContent.DustType<Dusts.RadiumDust>();
        }

        public override bool PreDraw(int x, int y, SpriteBatch spriteBatch)
        {
            if (Main.dayTime)
            {
                BaseDrawing.DrawTileTexture(spriteBatch, TextureAssets.Tile[Type].Value, x, y, true, false, false);
            }
            else
            {
                BaseDrawing.DrawTileTexture(spriteBatch, Mod.GetTexture("Tiles/Bricks/DarkmatterBrick"), x, y, true, false, false);
            }
            return false;
        }
    }
}
