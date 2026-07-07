using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Tiles
{
    public class RottedShingles_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<RottedShingles>());   
            AddMapEntry(new Color(0, 0, 50));
			MinPick = 0;
        }
    }
}