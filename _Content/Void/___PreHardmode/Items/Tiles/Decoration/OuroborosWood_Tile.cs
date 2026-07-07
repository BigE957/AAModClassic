using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration
{
    public class OuroborosWood_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            //true for block to emit light
            HitSound = SoundID.Dig;
            Main.tileBlockLight[Type] = true;
            RegisterItemDrop(ModContent.ItemType<OuroborosWood>());   
            DustType = ModContent.DustType<Dusts.DoomDust>();
            AddMapEntry(new Color(60, 60, 60));
        }
    }
}