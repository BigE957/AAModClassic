using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TorchsandHardened : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.HardenedSand[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("TorchsandHardened").Type;   
            AddMapEntry(new Color(50, 30, 17));
            MinPick = 65;
        }
    }
}