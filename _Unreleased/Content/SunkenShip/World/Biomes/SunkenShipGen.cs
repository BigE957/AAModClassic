using AAModClassic._Unreleased.Content.LostKeep.World.Biomes;
using AAModClassic._Unreleased.Content.SunkenShip.Tiles;
using AAModClassic._Unreleased.Content.SunkenShip.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles;
using AAModClassic.Tiles.Keep;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Biomes
{
    public class SunkenShipTexGenAssets : ModSystem
    {
        internal static TexGenData SmallShipTileData;
        internal static TexGenData SmallShipWallData;
        internal static TexGenData SmallShipLiquidData;

        internal static TexGenData BigShipTileData;
        internal static TexGenData BigShipThoriumTileData;
        internal static TexGenData BigShipWallData;
        internal static TexGenData BigShipLiquidData;

        public override void OnModLoad()
        {
            SmallShipTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Small_Tiles", AssetRequestMode.ImmediateLoad).Value);
            SmallShipWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Small_Walls", AssetRequestMode.ImmediateLoad).Value);
            SmallShipLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Small_Liquid", AssetRequestMode.ImmediateLoad).Value);

            BigShipTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Tiles", AssetRequestMode.ImmediateLoad).Value);
            BigShipThoriumTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Thorium_Tiles", AssetRequestMode.ImmediateLoad).Value);
            BigShipWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Walls", AssetRequestMode.ImmediateLoad).Value);
            BigShipLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Liquid", AssetRequestMode.ImmediateLoad).Value);
        }
    }

    public class SunkenShipGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Dictionary<Color, int> colorToTile = [];
            //TODOREFACTOR see if rotted wood uses era accurate sprite
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<RottedDynastyWoodS_Tile>();
            colorToTile[new Color(0, 255, 0)] = TileID.EmeraldGemspark;
            colorToTile[new Color(0, 0, 255)] = TileID.Rope;
            colorToTile[new Color(255, 255, 0)] = TileID.Sand;
            colorToTile[new Color(0, 255, 255)] = ModContent.TileType<CthulhuPortal_Tile>();
            colorToTile[new Color(150, 150, 150)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = [];
            colorToWall[new Color(255, 0, 0)] = ModContent.WallType<RottedFence>();
            colorToWall[new Color(255, 0, 255)] = ModContent.WallType<RottedWall_Wall>(); //Magenta
            colorToWall[new Color(255, 255, 0)] = ModContent.WallType<RottedWall_Wall>(); //Yellow
            colorToWall[new Color(0, 255, 0)] = ModContent.WallType<RottedWall_Wall>(); //Green
            colorToWall[new Color(255, 255, 255)] = ModContent.WallType<RottedWall_Wall>(); //White
            colorToWall[new Color(0, 255, 255)] = ModContent.WallType<RottedWall_Wall>(); //Cyan
            colorToWall[new Color(0, 0, 255)] = WallID.Sail;
            colorToWall[new Color(150, 150, 150)] = -2;

            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen;
            origin.Y -= 28;
            if (WorldGenUtils.GetWorldSize() == 1)
            {
                gen = TexGen.GetTexGenerator(SunkenShipTexGenAssets.SmallShipTileData, colorToTile, SunkenShipTexGenAssets.SmallShipWallData, colorToWall, SunkenShipTexGenAssets.SmallShipLiquidData);
                gen.Generate(origin.X, origin.Y, true, true);
                WorldGen.PlaceChest(origin.X + 13, origin.Y + 26, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);
            }
            else
            {
                TexGenData tileTex = ModLoader.HasMod("ThoriumMod") ? SunkenShipTexGenAssets.BigShipThoriumTileData : SunkenShipTexGenAssets.BigShipTileData;
                gen = TexGen.GetTexGenerator(tileTex, colorToTile, SunkenShipTexGenAssets.BigShipWallData, colorToWall, SunkenShipTexGenAssets.BigShipLiquidData);

                int newOriginX = origin.X - (gen.width / 2);
                int newOriginY = origin.Y - (gen.height / 2) + 10;
                gen.Generate(newOriginX, newOriginY, true, true);

                for (int x = newOriginX; x < newOriginX + tileTex.Width; x++)
                {
                    for (int y = newOriginY; y < newOriginY + tileTex.Height; y++)
                    {
                        if (Main.tile[x, y].TileType == TileID.EmeraldGemspark)
                        {
                            Main.tile[x, y].ClearTile();
                            WorldGen.PlaceTile(x, y, ModContent.TileType<RottedPlatform_Tile>(), mute: true);
                        }
                    }
                }

                WorldGen.PlaceChest(newOriginX + 66, newOriginY + 54, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);
            }

            return true;
        }
    }
}