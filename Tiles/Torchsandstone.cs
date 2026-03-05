using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Torchsandstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.Sandstone[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
            RegisterItemDrop(Mod.Find<ModItem>("Torchsandstone").Type);   
            AddMapEntry(new Color(50, 40, 40));
            MinPick = 65;
        }
    }
}