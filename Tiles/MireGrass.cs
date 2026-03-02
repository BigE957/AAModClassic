using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Tiles.Plants;

namespace AAModClassic.Tiles
{
    public class MireGrass : ModTile
    {
        public static int _type;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.JungleSpecial[Type] = true;
            DustType = Mod.Find<ModDust>("AbyssiumDust").Type;
            AddMapEntry(new Color(0, 50, 140));
            RegisterItemDrop(ItemID.MudBlock);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(40) == 0)
            {
                int style = Main.rand.Next(23);
                if (PlaceObject(i, j - 1, MireFoliage._type, false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, MireFoliage._type, style, 0, -1, -1);
            }
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(1500) == 0)
            {
                PlaceObject(i, j - 1, Mod.Find<ModTile>("Darkshroom").Type);
                NetMessage.SendObjectPlacement(-1, i, j - 1, Mod.Find<ModTile>("Darkshroom").Type, 0, 0, -1, -1);

            }
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.Next(10000) == 0)
            {
                PlaceObject(i, j - 1, Mod.Find<ModTile>("BlackLotus").Type);
                NetMessage.SendObjectPlacement(-1, i, j - 1, Mod.Find<ModTile>("BlackLotus").Type, 0, 0, -1, -1);

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