using AAModClassic.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Content.RedMushroom.World.Biomes
{
    public class RedMushroomGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            ushort tileGrass = (ushort)ModContent.TileType<Mycelium_Tile>(); //change to types in your mod

            int worldSize = GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150, biomeWidthHalf = biomeWidth / 2; //how wide the biome is (scaled by world size)
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;

            //ok time to check to see if this spot is actually a good place to gen
            Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
            Point newOrigin = new Point(origin.X - biomeWidthHalf, origin.Y - 10);
            WorldUtils.Gen(newOrigin, new Shapes.Rectangle(biomeWidth, biomeHeight), new Actions.TileScanner(new ushort[]
            {
                TileID.Grass,
                TileID.Dirt,
                TileID.Stone,
                TileID.Sand,
                TileID.SnowBlock,
                TileID.IceBlock,
                TileID.BlueDungeonBrick,
                TileID.PinkDungeonBrick,
                TileID.GreenDungeonBrick
            }).Output(dictionary));

            int normalBiomeCount = dictionary[TileID.Grass] + dictionary[TileID.Dirt] + dictionary[TileID.Stone];
            int IceBlockBiomeCount = dictionary[TileID.SnowBlock] + dictionary[TileID.IceBlock];
            int sandBiomeCount = dictionary[TileID.Sand];
            int dungeonCount = dictionary[TileID.BlueDungeonBrick] + dictionary[TileID.PinkDungeonBrick] + dictionary[TileID.GreenDungeonBrick];

            if (dungeonCount > 0 || IceBlockBiomeCount >= normalBiomeCount || sandBiomeCount >= normalBiomeCount) //don't gen if you're in the Dungeon at all or if the Ice count (Snow) or the Sand count (desert) is too high
            {
                return false;
            }
            WorldUtils.Gen(newOrigin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[] //gen grass...
            {
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass }), //ensure we only replace the intended tile (in this case, grass)
                new RadialDitherTopMiddle(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10), //this provides the 'blending' on the edges (except the top)
                new SetModTile(tileGrass, true, true) //actually place the tile
            }));
            return true;
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6300) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

}
