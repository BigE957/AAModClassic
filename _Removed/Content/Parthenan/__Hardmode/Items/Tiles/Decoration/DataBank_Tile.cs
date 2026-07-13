using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration.Ancient;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration
{
    public class DataBank_Tile : AncientDataBank_Tile, IGlowmaskTile
    {
        public new Color GlowColor => BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, Color.White, Color.White, Color.Violet, Color.White, Color.Violet, Color.White, Color.White, Color.White, Color.White, Color.Violet, Color.White, Color.Violet);

        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<DataBank>());
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Data Bank");
            AddMapEntry(new Color(60, 0, 120), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[] { TileID.Bookcases };
        }
    }
}