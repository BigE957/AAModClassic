using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Decoration;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Altar
{
    class DaybringerBrick_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<RadiumBrick>());   
            AddMapEntry(Color.DarkGoldenrod);
            DustType = ModContent.DustType<Dusts.RadiumDust>();
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged) => AAWorld.downedEquinox;

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => AAWorld.downedEquinox;
    }
}
