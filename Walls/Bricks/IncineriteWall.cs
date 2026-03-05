using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Walls.Bricks
{
    public class IncineriteWall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(Mod.Find<ModItem>("IncineriteWall").Type);
            AddMapEntry(new Color(40, 30, 10));
            DustType = Mod.Find<ModDust>("IncineriteDust").Type;
        }
    }
}