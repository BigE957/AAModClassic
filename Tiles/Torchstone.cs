using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Torchstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("Incinerite").Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = 21;
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("Torchstone").Type;   
            AddMapEntry(new Color(50, 25, 12));
			MinPick = 65;
        }
    }
}