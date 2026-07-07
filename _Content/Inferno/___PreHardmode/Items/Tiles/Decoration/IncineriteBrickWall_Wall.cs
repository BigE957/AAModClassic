using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration
{
    public class IncineriteBrickWall_Wall : ModWall
	{
        public override void SetStaticDefaults()
        {
            Main.wallLight[Type] = true;
            Main.wallHouse[Type] = true;
            HitSound = SoundID.Tink;
            RegisterItemDrop(ModContent.ItemType<IncineriteBrickWall>());
            AddMapEntry(new Color(40, 30, 10));
            DustType = ModContent.DustType<Dusts.IncineriteDust>();
        }
    }
}