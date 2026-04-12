using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Tiles.Plants;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Mire.World.Tiles
{
    public class MireGrass_Tile : ModTile
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
            DustType = ModContent.DustType<Dusts.AbyssiumDust>();
            AddMapEntry(new Color(0, 50, 140));
            RegisterItemDrop(ItemID.MudBlock);
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(40))
            {
                int style = Main.rand.Next(23);
                if (PlaceObject(i, j - 1, MireFoliage_Tile._type, false, style))
                    NetMessage.SendObjectPlacement(-1, i, j - 1, MireFoliage_Tile._type, style, 0, -1, -1);
            }
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1500))
            {
                PlaceObject(i, j - 1, ModContent.TileType<Darkshroom_Tile>());
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Darkshroom_Tile>(), 0, 0, -1, -1);

            }
            if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(10000))
            {
                PlaceObject(i, j - 1, ModContent.TileType<BlackLotus_Tile>());
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<BlackLotus_Tile>(), 0, 0, -1, -1);

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