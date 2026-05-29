using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Stars.World.Altar;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Stars.World.Biomes
{
    internal class EquinoxShrineTexGenAssets : ModSystem
    {
        internal static TexGenData EquinoxTileData;
        internal static TexGenData EquinoxSlopeData;

        public override void OnModLoad()
        {
            EquinoxTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Stars/World/Biomes/EquinoxAltar", AssetRequestMode.ImmediateLoad).Value);
            EquinoxSlopeData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Stars/World/Biomes/EquinoxAltarSlope", AssetRequestMode.ImmediateLoad).Value);
        }
    }
    public class EquinoxShrineGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, EquinoxShrineTexGenAssets.EquinoxTileData.Width, EquinoxShrineTexGenAssets.EquinoxTileData.Height), 20);

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<GreedBrick_Tile>(),
                [new Color(0, 255, 255)] = ModContent.TileType<DayCrystal_Tile>(),
                [new Color(0, 255, 0)] = ModContent.TileType<NightCrystal_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<DaybringerBrick_Tile>(),
                [new Color(0, 0, 255)] = ModContent.TileType<NightcrawlerBrick_Tile>(),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = TexGen.GetTexGenerator(EquinoxShrineTexGenAssets.EquinoxTileData, colorToTile, null, null, null, EquinoxShrineTexGenAssets.EquinoxSlopeData);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 36, origin.Y + 39, ModContent.TileType<WormAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 36, origin.Y + 39, ModContent.TileType<WormAltar_Tile>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 30, origin.Y + 42, ModContent.TileType<StarAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 30, origin.Y + 42, ModContent.TileType<StarAltar_Tile>(), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 45, origin.Y + 42, ModContent.TileType<GravAltar_Tile>());
            NetMessage.SendObjectPlacement(-1, origin.X + 80, origin.Y + 88, ModContent.TileType<GravAltar_Tile>(), 0, 0, -1, -1);

            return true;
        }
    }
}
