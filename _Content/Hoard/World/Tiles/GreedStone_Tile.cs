using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.World.Tiles
{
    public class GreedStone_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<GreedStone>());   
            AddMapEntry(new Color(125, 59, 42));
            HitSound = SoundID.Tink;
            MinPick = 200;
        }
    }
}