using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Tiles
{
    public class OroborosWood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            HitSound = SoundID.Dig;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<OroborosWood>());   
            DustType = ModContent.DustType<DoomDust>();
            AddMapEntry(new Color(60, 60, 60));
        }
    }
}