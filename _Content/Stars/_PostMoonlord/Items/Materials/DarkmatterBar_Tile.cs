using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Materials
{
    public class DarkmatterBar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            HitSound = SoundID.Dig;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<Dusts.DarkmatterDust>();
            RegisterItemDrop(ModContent.ItemType<DarkmatterBar>());   
            AddMapEntry(new Color(0, 0, 255));
			MinPick = 0;
        }
    }
}