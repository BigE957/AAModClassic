using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic._Content.Hoard.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Acropolis.World.Biomes
{
    internal class AcropolisTexGenAssets : ModSystem
    {
        internal static TexGenData AcropolisTileData;
        internal static TexGenData AcropolisWallData;
        internal static TexGenData AcropolisRoofData;

        public override void OnModLoad()
        {
            AcropolisTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/World/Biomes/Acropolis", AssetRequestMode.ImmediateLoad).Value);
            AcropolisWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/World/Biomes/AcropolisWalls", AssetRequestMode.ImmediateLoad).Value);
            AcropolisRoofData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/World/Biomes/AcropolisRoof", AssetRequestMode.ImmediateLoad).Value);;
        }
    }

    public class AcropolisGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, AcropolisTexGenAssets.AcropolisTileData.Width, AcropolisTexGenAssets.AcropolisTileData.Height), 20);

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<AcropolisBlock_Tile>(),
                [new Color(128, 128, 128)] = ModContent.TileType<AcropolisBlock2_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<SkyShard_Tile>(),
                [new Color(0, 255, 255)] = TileID.Grass,
                [new Color(0, 255, 0)] = TileID.Dirt,
                [new Color(0, 0, 255)] = TileID.Cloud,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.WallType<AcropolisBrickWall_Wall>(),
                [new Color(0, 255, 255)] = ModContent.WallType<AcropolisPillarWall_Wall>(),
                [new Color(0, 255, 0)] = WallID.Dirt,
                [new Color(0, 0, 255)] = WallID.Cloud,
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(AcropolisTexGenAssets.AcropolisTileData, colorToTile, AcropolisTexGenAssets.AcropolisWallData, colorToWall, null, AcropolisTexGenAssets.AcropolisRoofData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 79, origin.Y + 86, (ushort)ModContent.TileType<AcropolisAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 79, origin.Y + 87, (ushort)ModContent.TileType<AcropolisAltar_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }
}
