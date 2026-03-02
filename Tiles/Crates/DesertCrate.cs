using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Crates
{
    public class DesertCrate : ModTile
    {
        public override void SetStaticDefaults()
        {
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Desert Crate");
            Main.tileFrameImportant[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileNoAttach[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 200), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, Mod.Find<ModItem>("DesertCrate").Type);
        }
    }
}
