using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration.Ancient
{
    public class AncientDataBank_Tile : ModTile, IGlowmaskTile
    {
        public Color GlowColor => BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.White, Color.White, Color.Violet, Color.White, Color.Violet, Color.White, Color.White, Color.White, Color.White, Color.Violet, Color.White, Color.Violet);

        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<AncientDataBank>());
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Data Bank");
            AddMapEntry(new Color(60, 0, 120), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[] { TileID.Bookcases };
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(ZAAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}