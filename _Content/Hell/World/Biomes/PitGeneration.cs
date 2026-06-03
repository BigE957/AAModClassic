using AAModClassic._Content.Hell.World.Tiles;
using AAModClassic._Content.Hoard.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Content.Hell.World.Biomes
{
    internal class PitTexGenAssets : ModSystem
    {
        internal static TexGenData PitContructionTileData;
        internal static TexGenData PitTileData;
        internal static TexGenData PitWallData;
        internal static TexGenData PitLiquidData;
        internal static TexGenData PitSlopeData;

        public override void OnModLoad()
        {
            PitContructionTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Hell/World/Biomes/PitConstruction");
            PitTileData = TexGen.GetTextureForGen("AAModClassic/_Content/Hell/World/Biomes/Pit");
            PitWallData = TexGen.GetTextureForGen("AAModClassic/_Content/Hell/World/Biomes/PitWall");
            PitLiquidData = TexGen.GetTextureForGen("AAModClassic/_Content/Hell/World/Biomes/PitLava");
            PitSlopeData = TexGen.GetTextureForGen("AAModClassic/_Content/Hell/World/Biomes/PitSlope");
        }
    }

    public class PitGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, PitTexGenAssets.PitTileData.Width, PitTexGenAssets.PitTileData.Height), 20);

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = ModContent.TileType<Pitstone_Tile>(),
                [new Color(0, 0, 255)] = ModContent.TileType<PitBars_Tile>(),
                [new Color(0, 255, 0)] = ModContent.TileType<PitBridge_Tile>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = ModContent.WallType<PitBarWall_Wall>(),
                [new Color(255, 0, 0)] = ModContent.WallType<PitStoneWall_Wall>(),
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(336, 145), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0),
                new Actions.SetSlope(0)
            }));

            TexGen gen = TexGen.GetTexGenerator(PitTexGenAssets.PitTileData, colorToTile, PitTexGenAssets.PitWallData, colorToWall, PitTexGenAssets.PitLiquidData, PitTexGenAssets.PitSlopeData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 281, origin.Y + 52, ModContent.TileType<Throne_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 281, origin.Y + 52, ModContent.TileType<Throne_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }

    public class PitTeaserGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, PitTexGenAssets.PitContructionTileData.Width, PitTexGenAssets.PitContructionTileData.Height), 20);

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = ModContent.TileType<Pitstone_Tile>(),
                [new Color(0, 0, 255)] = ModContent.TileType<PitBars_Tile>(),
                [new Color(0, 255, 0)] = ModContent.TileType<PitBridge_Tile>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(90, 103), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetSlope(0)
            }));

            TexGen gen = TexGen.GetTexGenerator(PitTexGenAssets.PitContructionTileData, colorToTile);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 35, origin.Y + 20, ModContent.TileType<Throne_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 30, origin.Y + 20, ModContent.TileType<Throne_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }
}
