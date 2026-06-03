using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials
{
    public class FulguriteShard_Tile : ModTile, IGlowmaskTile
    {
        public Color GlowColor => BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.Black, Color.Violet, Color.Black, Color.Violet, Color.Black, Color.Black, Color.Black);

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;  //true for block to emit light
            Main.tileLighted[Type] = true;
            //TODO
            //HitSound = 21;
            DustType = ModContent.DustType<FulguriteDust>();
            AddMapEntry(new Color(204, 0, 150));
			MinPick = 180;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[x, y];
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.Violet, Color.White, Color.White);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}