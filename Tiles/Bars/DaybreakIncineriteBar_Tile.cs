using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Bars
{
    public class DaybreakIncineriteBar_Tile : ModTile
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

            RegisterItemDrop(ModContent.ItemType<DaybreakIncinerite>());   
            DustType = ModContent.DustType<Dusts.DaybreakIncineriteDust>();
            AddMapEntry(new Color(160, 100, 0));
			MinPick = 0;
        }
    }
}