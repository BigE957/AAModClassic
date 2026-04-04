using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class Depthstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<AbyssiumOre>()] = true;
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            Main.tileBlendAll[Type] = false;
			Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Dig;
            MinPick = 65;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = ModContent.DustType<DeepAbyssiumDust>();
            RegisterItemDrop(ModContent.ItemType<Depthstone>());   
            AddMapEntry(new Color(27, 19, 50));
        }
    }
}