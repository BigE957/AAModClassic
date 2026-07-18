using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushWall_Tile : ModWall 
    {
        public override void SetStaticDefaults() 
        {
			Main.wallHouse[Type] = true;

            HitSound = SoundID.Dig;
			DustType = ModContent.DustType<MushDust>();

			VanillaFallbackOnModDeletion = WallID.Mushroom;
            AddMapEntry(new Color(190, 50, 50));
        }
    }
}