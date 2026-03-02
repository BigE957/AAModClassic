using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Bars
{
    public class RadiumBarTile : ModTile
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

            DustType = ModContent.DustType<Dusts.RadiumDust>();
            AddMapEntry(new Color(200, 170, 100));
			MinPick = 0;
        }
    }
}