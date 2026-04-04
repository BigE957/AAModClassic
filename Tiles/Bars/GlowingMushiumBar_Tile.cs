using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Bars
{
    public class GlowingMushiumBar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            HitSound = SoundID.Tink;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<ShroomDust>();
            RegisterItemDrop(ModContent.ItemType<GlowingMushiumBar>());   
            AddMapEntry(new Color(0, 100, 255));
			MinPick = 0;
        }
    }
}