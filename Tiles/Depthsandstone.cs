using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Depthsandstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Sandstone[Type] = true;
            Main.tileLighted[Type] = false;
            DustType = Mod.Find<ModDust>("DeepAbyssiumDust").Type;
            RegisterItemDrop(Mod.Find<ModItem>("Depthsandstone").Type);   
            AddMapEntry(new Color(0, 20, 127));
			MinPick = 65;
        }
    }
}