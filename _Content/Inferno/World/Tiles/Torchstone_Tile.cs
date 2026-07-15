using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class Torchstone_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<IncineriteOre_Tile>()] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Tink;
            DustType = ModContent.DustType<Dusts.RazewoodDust>();
            RegisterItemDrop(ModContent.ItemType<Torchstone>());   
            AddMapEntry(new Color(50, 25, 12));
			MinPick = 65;
        }
    }
}