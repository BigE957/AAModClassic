using AAModClassic._Unreleased.Content.SunkenShip.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.WorldGen;
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
            SmallShipTileData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Small_Tiles");
            SmallShipWallData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Small_Walls");
            SmallShipLiquidData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Small_Liquid");

            BigShipTileData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Tiles");
            BigShipThoriumTileData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Thorium_Tiles");
            BigShipWallData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Walls");
            BigShipLiquidData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/SunkenShip/World/Biomes/SunkenShipGen_Liquid");
        }
    }

    public class SunkenShipGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Dictionary<Color, int> colorToTile = [];
            //TODOREFACTOR see if rotted wood uses era accurate sprite
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<RottedDynastyWoodS_Tile>();
            colorToTile[new Color(0, 255, 0)] = ModContent.TileType<RottedPlatform_Tile>();// TileID.EmeraldGemspark;
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
            TexGenData tileTex = ModLoader.HasMod("ThoriumMod") ? SunkenShipTexGenAssets.BigShipThoriumTileData : SunkenShipTexGenAssets.BigShipTileData;
            gen = TexGen.GetTexGenerator(tileTex, colorToTile, SunkenShipTexGenAssets.BigShipWallData, colorToWall, SunkenShipTexGenAssets.BigShipLiquidData);

            int newOriginX = origin.X - (gen.width / 2);
            int newOriginY = origin.Y - (gen.height / 2) + 10;
            gen.Generate(newOriginX, newOriginY, false, true);

            AAWorld_Unreleased.shipPos = new Point(newOriginX, newOriginY);

            for (int x = newOriginX; x < newOriginX + tileTex.Width; x++)
            {
                for (int y = newOriginY; y < newOriginY + tileTex.Height; y++)
                {
                    if (Main.tile[x, y].TileType == TileID.EmeraldGemspark)
                    {
                        Main.tile[x, y].ClearTile();
                        WorldGen.PlaceTile(x, y, ModContent.TileType<RottedPlatform_Tile>());
                    }
                }
            }

            /*
            if (WorldGenUtils.GetWorldSize() == 1)
            {
                gen = TexGen.GetTexGenerator(SunkenShipTexGenAssets.SmallShipTileData, colorToTile, SunkenShipTexGenAssets.SmallShipWallData, colorToWall, SunkenShipTexGenAssets.SmallShipLiquidData);
                gen.Generate(origin.X, origin.Y, true, true);
                WorldGen.PlaceChest(origin.X + 13, origin.Y + 26, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);
            }
            else
            {
                
            }
            */

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                //Deity Statues
                WorldGen.PlaceObject(newOriginX + 132, newOriginY + 47, TileID.Statues, true, 35);
                WorldGen.PlaceObject(newOriginX + 136, newOriginY + 47, TileID.Statues, true, 30);
                WorldGen.PlaceObject(newOriginX + 140, newOriginY + 47, TileID.Statues, true, 65);
                WorldGen.PlaceObject(newOriginX + 144, newOriginY + 47, TileID.Statues, true, 15);
                WorldGen.PlaceObject(newOriginX + 148, newOriginY + 47, TileID.Statues, true, 39);
                WorldGen.PlaceObject(newOriginX + 152, newOriginY + 47, TileID.Statues, true, 71);

                //Captain's Quarters
                WorldGen.PlaceChest(newOriginX + 141, newOriginY + 54, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);
            }
            else
                WorldGen.PlaceChest(newOriginX + 66, newOriginY + 54, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);

            return true;
        }
    }
}