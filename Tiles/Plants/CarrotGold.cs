using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;

namespace AAModClassic.Tiles.Plants
{
    public class CarrotGold : ModTile
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
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Items.BossSummons.GoldenCarrot>();
            DustType = DustID.GoldCoin;
            HitSound = 6;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }

}