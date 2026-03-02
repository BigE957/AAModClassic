using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Bars
{
    public class DaybreakIncineriteBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            HitSound = 21;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DaybreakIncinerite").Type;   
            DustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
            AddMapEntry(new Color(160, 100, 0));
			MinPick = 0;
        }
    }
}