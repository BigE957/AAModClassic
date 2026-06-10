using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Tiles
{
    public class RottedDynastyWoodS_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            
            RegisterItemDrop(ModContent.ItemType<RottedDynastyWood>());   
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