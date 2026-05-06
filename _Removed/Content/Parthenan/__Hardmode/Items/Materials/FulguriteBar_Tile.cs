using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials
{
    public class FulguriteBar_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            //TODO
            //HitSound = 21;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            DustType = Mod.Find<ModDust>("FulguriteDust").Type;
            AddMapEntry(new Color(100, 0, 126));
			MinPick = 0;
        }
    }
}