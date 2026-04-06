using AAModClassic.Items.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class IncineriteBrick_Wall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<IncineriteWall>());
            AddMapEntry(new Color(40, 30, 10));
            DustType = ModContent.DustType<Dusts.IncineriteDust>();
        }
    }
}