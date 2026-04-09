using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Tiles.Plants;

namespace AAModClassic.Tiles
{
    public class Mycelium_Tile : ModTile
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            DustType = ModContent.DustType<Dusts.MushDust>();
			AddMapEntry(new Color(100, 100, 0));
            RegisterItemDrop(ItemID.DirtBlock);
		}

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(30) == 0)
            {
                PlaceObject(i, j - 1, ModContent.TileType<Mushroom_Tile>());
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Mushroom_Tile>(), Main.rand.Next(5), 0, -1, -1);
            }
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(1000) == 0)
            {
                int style = Main.rand.Next(5);
                if (PlaceObject(i, j - 1, ModContent.TileType<MadnessShroom_Tile>(), false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MadnessShroom_Tile>(), style, 0, -1, -1);
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }
            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }
            return false;
        }
    }
}