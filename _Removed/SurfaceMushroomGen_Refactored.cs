using AAModClassic._Content.RedMushroom.World.Tiles;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Removed
{
    // this had to be majorly rewritten to function at any level consistently
    // sorry accuracyheads but the artistic vision wins out
    public class SurfaceMushroomGen_Refactored : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ushort tileGrass = (ushort)ModContent.TileType<Mycelium_Tile>();

            int worldSize = GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150, biomeWidthHalf = biomeWidth / 2;
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;

            WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[]
            {
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.CrimsonGrass }),
                new RadialDitherCenter(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10),
                new SetModTile(tileGrass, true, true)
            }));
            WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[]
{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Ebonstone, TileID.Crimstone }),
                new RadialDitherCenter(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10),
                new SetModTile(TileID.Stone, true, true)
            }));

            return true;
        }
    }
}
