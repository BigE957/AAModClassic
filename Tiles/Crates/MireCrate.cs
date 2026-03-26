using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Crates
{
    public class MireCrate : ModTile
    {
        public override void SetStaticDefaults()
        {
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Mire Crate");
            Main.tileFrameImportant[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 200), name);
        }
    }
}
