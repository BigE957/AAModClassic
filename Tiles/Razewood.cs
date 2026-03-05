using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    class Razewood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(Mod.Find<ModItem>("Razewood").Type);   
            AddMapEntry(new Color(20f, 20f, 20f));
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
        }
    }
}
