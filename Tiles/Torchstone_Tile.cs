using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Items.Blocks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Torchstone_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<IncineriteOre_Tile>()] = true;
            Terraria.ID.TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Dig;
            DustType = ModContent.DustType<Dusts.RazewoodDust>();
            RegisterItemDrop(ModContent.ItemType<Torchstone>());   
            AddMapEntry(new Color(50, 25, 12));
			MinPick = 65;
        }
    }
}