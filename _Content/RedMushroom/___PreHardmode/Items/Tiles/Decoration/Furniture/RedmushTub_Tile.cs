using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushTub_Tile : ModTile 
    {
        public override void SetStaticDefaults() 
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 18];
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, -2);
			TileObjectData.addTile(Type);

            HitSound = SoundID.Dig;
			DustType = ModContent.DustType<MushDust>();

			AdjTiles = [TileID.Bathtubs];
			VanillaFallbackOnModDeletion = TileID.Bathtubs;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 150, 20), name);

			RegisterItemDrop(ModContent.ItemType<RedmushTub>(), 0);
        }

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}