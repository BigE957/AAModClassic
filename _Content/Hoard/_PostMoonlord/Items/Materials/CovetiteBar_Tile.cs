using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items.Materials
{
    public class CovetiteBar_Tile : ModTile
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

            RegisterItemDrop(ModContent.ItemType<CovetiteBar>());   
            DustType = DustID.Gold;
            AddMapEntry(new Color(150, 130, 0));
			MinPick = 0;
        }
    }
}