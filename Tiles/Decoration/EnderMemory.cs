
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic;

namespace AAModClassic.Tiles.Decoration
{
    public class EnderMemory : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            DustType = DustID.Gold;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.Height = 11;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Direction = TileObjectDirection.None;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Eternal Memory");
            AddMapEntry(new Color(150, 100, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            RegisterItemDrop(Mod.Find<ModItem>("EnderMemory").Type);
        }

        public override bool RightClick(int i, int j)
        {
            BaseUtility.Chat(Lang.TilesInfo("EnderMemoryInfo"), Color.Goldenrod);
            return true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 32, Mod.Find<ModItem>("EnderMemory").Type);
        }

    }
}