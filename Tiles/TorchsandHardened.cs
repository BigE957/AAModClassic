using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class TorchsandHardened : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.HardenedSand[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = Mod.Find<ModDust>("RazewoodDust").Type;
            RegisterItemDrop(Mod.Find<ModItem>("TorchsandHardened").Type);   
            AddMapEntry(new Color(50, 30, 17));
            MinPick = 65;
        }
    }
}