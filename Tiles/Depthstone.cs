using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Depthstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][Mod.Find<ModTile>("AbyssiumOre").Type] = true;
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            HitSound = 21;
            MinPick = 65;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = Mod.Find<ModDust>("DeepAbyssiumDust").Type;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("Depthstone").Type;   
            AddMapEntry(new Color(27, 19, 50));
        }
    }
}