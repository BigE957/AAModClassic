using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Plants
{
    public class Darkshroom_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;

            Main.tileMergeDirt[Type] = true;
            //Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type);
            RegisterItemDrop(ModContent.ItemType<Darkshroom>());
        }

        public override bool IsTileDangerous(int i, int j, Player player)
        {
            return true;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            return false;
        }


        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }

}