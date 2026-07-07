using AAModClassic._Content.Terrarium.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Terrarium.World.Biomes
{
    internal class TerrariumTexGenAssets : ModSystem
    {
        internal static TexGenData TerrariumSmallDeletionData;
        internal static TexGenData TerrariumMediumDeletionData;

        internal static TexGenData TerrariumSmallTileData;
        internal static TexGenData TerrariumMediumTileData;

        internal static TexGenData TerrariumSmallWallData;
        internal static TexGenData TerrariumMediumWallData;

        public override void OnModLoad()
        {
            TerrariumSmallDeletionData = TexGen.GetTextureForGen("AAModClassic/_Content/Terrarium/World/Biomes/TerrariumDelete");
            TerrariumMediumDeletionData = TexGen.GetTextureForGen("AAModClassic/_Content/Terrarium/World/Biomes/TerrariumMedDelete");

            TerrariumSmallTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Terrarium/World/Biomes/Terrarium");
            TerrariumMediumTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Terrarium/World/Biomes/TerrariumMed");

            TerrariumSmallWallData = TexGen.GetTextureForGen("AAModClassic/_Content/Terrarium/World/Biomes/TerrariumWalls");
            TerrariumMediumWallData = TexGen.GetTextureForGen("AAModClassic/_Content/Terrarium/World/Biomes/TerrariumMedWalls");
        }
    }

    public class TerrariumDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            int worldSize = WorldGenUtils.GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning		
            };


            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning	

            TexGenData Terrasphere = null;

            if (Terrasphere == null)
            {
                if (worldSize == 1)
                {
                    Terrasphere = TerrariumTexGenAssets.TerrariumSmallDeletionData;
                }
                else
                {
                    Terrasphere = TerrariumTexGenAssets.TerrariumMediumDeletionData;
                }
            }

            TexGen gen = TexGen.GetTexGenerator(Terrasphere, colorToTile, Terrasphere, colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
                new WorldGenUtils.InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
                new WorldGenUtils.InWorld(),
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
    }

    public class TerrariumGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
            int worldSize = WorldGenUtils.GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = ModContent.TileType<TerraCrystal_Tile>(),
                [new Color(255, 0, 255)] = ModContent.TileType<PermeableTerraWood_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<TerraLeaves_Tile>(),
                [new Color(0, 0, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            HashSet<int> protectedTiles = [
                ModContent.TileType<TerraCrystal_Tile>(),
                ModContent.TileType<PermeableTerraWood_Tile>(),
                ModContent.TileType<TerraLeaves_Tile>(),
            ];

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGenData Terrasphere = null;

            TexGenData TerraWalls = null;

            if (Terrasphere == null)
            {
                if (worldSize == 1)
                {
                    Terrasphere = TerrariumTexGenAssets.TerrariumSmallTileData;

                    TerraWalls = TerrariumTexGenAssets.TerrariumSmallWallData;
                }
                else
                {
                    Terrasphere = TerrariumTexGenAssets.TerrariumMediumTileData;

                    TerraWalls = TerrariumTexGenAssets.TerrariumMediumWallData;
                }
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, Terrasphere.Width, Terrasphere.Height), 20);

            TexGen gen = TexGen.GetTexGenerator(Terrasphere, colorToTile, TerraWalls, colorToWall, unbreakableTiles: protectedTiles);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
                new WorldGenUtils.InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
                new WorldGenUtils.InWorld(),
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
    }
}
