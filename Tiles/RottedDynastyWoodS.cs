using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class RottedDynastyWoodS : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("RottedDynastyWood").Type;   
            AddMapEntry(new Color(39, 34, 8));
			MinPick = 0;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (AAWorld.downedShen)
            {
                return true;
            }
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}