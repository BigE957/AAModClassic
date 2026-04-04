using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class ScorchedDynastyWood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<ScorchedDynastyWood>());   
            AddMapEntry(new Color(153, 100, 0));
			MinPick = 0;
        }
    }
}