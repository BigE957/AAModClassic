using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.___Content.Inferno.__Hardmode.Items.Materials
{
    public class RadiantIncineriteBar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            HitSound = SoundID.Tink;

            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<Dusts.RadiantIncineriteDust>();
            RegisterItemDrop(ModContent.ItemType<RadiantIncineriteBar>());   
            AddMapEntry(new Color(100, 50, 0));
			MinPick = 0;
        }
    }
}