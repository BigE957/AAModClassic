using AAModClassic._Content.Stars.World.Biomes;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles.Decoration;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content._Dev.World.Biomes
{
    internal class CrystalOfMemoriesTexGenAssets : ModSystem
    {
        internal static TexGenData EnderCrystalTileData;
        internal static TexGenData EnderCrystalWallData;
        internal static TexGenData EnderCrystalSlopeData;

        public override void OnModLoad()
        {
            EnderCrystalTileData = TexGen.GetTextureForGen("AAModClassic/_Content/_Dev/World/Biomes/EnderCrystal");
            EnderCrystalWallData = TexGen.GetTextureForGen("AAModClassic/_Content/_Dev/World/Biomes/EnderCrystalWall");
            EnderCrystalSlopeData = TexGen.GetTextureForGen("AAModClassic/_Content/_Dev/World/Biomes/EnderCrystalSlope");
        }
    }

    public class CrystalOfMemoriesGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, CrystalOfMemoriesTexGenAssets.EnderCrystalTileData.Width, CrystalOfMemoriesTexGenAssets.EnderCrystalTileData.Height), 20);

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = TileID.CrystalBlock,
                [new Color(0, 0, 255)] = TileID.GraniteBlock,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = WallID.Crystal,
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(CrystalOfMemoriesTexGenAssets.EnderCrystalTileData, colorToTile, CrystalOfMemoriesTexGenAssets.EnderCrystalWallData, colorToWall, null, CrystalOfMemoriesTexGenAssets.EnderCrystalSlopeData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 27, origin.Y + 26, (ushort)ModContent.TileType<EnderMemory_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 27, origin.Y + 26, (ushort)ModContent.TileType<EnderMemory_Tile>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 16, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 16, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier_Tile>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 41, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 41, origin.Y + 27, (ushort)ModContent.TileType<CrystalChandelier_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }
}
