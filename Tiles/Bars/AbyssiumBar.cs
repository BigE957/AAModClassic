using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Bars
{
    public class AbyssiumBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            HitSound = SoundID.Tink;// 21;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            RegisterItemDrop(ModContent.ItemType<AbyssiumBar>());   
            DustType = ModContent.DustType<AbyssiumDust>();
            AddMapEntry(new Color(0, 0, 255));
			MinPick = 0;
        }
    }
}