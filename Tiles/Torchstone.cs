using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Torchstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<IncineriteOre>()] = true;
            Terraria.ID.TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Dig;
            DustType = ModContent.DustType<RazewoodDust>();
            RegisterItemDrop(ModContent.ItemType<Torchstone>());   
            AddMapEntry(new Color(50, 25, 12));
			MinPick = 65;
        }
    }
}