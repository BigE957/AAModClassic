using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional
{
    public class ACS_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            DustType = ModContent.DustType<Dusts.DoomDust>();
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Any Ancient Crafting Station");
            AddMapEntry(new Color(40, 0, 0), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            RegisterItemDrop(ModContent.ItemType<BinaryReassembler>());
        }
        

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0;
            b = 0f;
        }
    }
}