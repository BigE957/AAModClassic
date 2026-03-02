using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles.Bricks
{
    class EventideBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("EventideBrick").Type;   
            AddMapEntry(AAColor.Yamata);
            DustType = ModContent.DustType<Dusts.AbyssDust>();
        }
    }
}
