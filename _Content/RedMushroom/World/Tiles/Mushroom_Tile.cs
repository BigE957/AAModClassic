using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace AAModClassic._Content.RedMushroom.World.Tiles
{
    public class Mushroom_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;

            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.RandomStyleRange = 5;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            RegisterItemDrop(ItemID.Mushroom);
            DustType = ModContent.DustType<Dusts.MushDust>();
            HitSound = SoundID.Grass;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }

}