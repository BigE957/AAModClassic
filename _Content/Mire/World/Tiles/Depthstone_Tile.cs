using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Tiles
{
    public class Depthstone_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<AbyssiumOre_Tile>()] = true;
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Dig;
            MinPick = 65;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = ModContent.DustType<Dusts.DeepAbyssiumDust>();
            RegisterItemDrop(ModContent.ItemType<Depthstone>());   
            AddMapEntry(new Color(27, 19, 50));
        }
    }
}