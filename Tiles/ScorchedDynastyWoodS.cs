using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class ScorchedDynastyWoodS : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("ScorchedDynastyWood").Type;   
            AddMapEntry(new Color(153, 100, 0));
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