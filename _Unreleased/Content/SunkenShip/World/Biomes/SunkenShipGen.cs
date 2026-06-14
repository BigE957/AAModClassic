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
                WorldGen.PlaceChest(newOriginX + 141, newOriginY + 54, (ushort)ModContent.TileType<SunkenChest_Tile>(), false);
                WorldGen.PlaceObject(newOriginX + 139, newOriginY + 54, TileID.Bookcases, true, 25);
                WorldGen.PlaceObject(newOriginX + 145, newOriginY + 54, TileID.Tables, true, 28);
                WorldGen.PlaceObject(newOriginX + 145, newOriginY + 52, TileID.Books, true);
                WorldGen.PlaceObject(newOriginX + 144, newOriginY + 52, TileID.Candles, true, 0);
                Main.tile[newOriginX + 144, newOriginY + 52].TileFrameX += 18;
                WorldGen.PlaceObject(newOriginX + 147, newOriginY + 54, TileID.Chairs, true, 15, direction: -1);
                WorldGen.PlaceObject(newOriginX + 149, newOriginY + 50, TileID.Painting3X3, true, 53);
                WorldGen.PlaceObject(newOriginX + 153, newOriginY + 54, TileID.Beds, true, 24);

                //Sleeping Quarters
                for(int i = 0; i < 2; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        Point baseOffset = new(newOriginX + 139, newOriginY + 67 - (i == 0 ? j : j + 3));

                        switch (j)
                        {
                            case 0:
                                for(int k = 0; k < 16; k++)
                                {
                                    if (k % 5 == 0)
                                        WorldGen.PlaceTile(baseOffset.X + k, baseOffset.Y, TileID.WoodenBeam, true);
                                    else
                                    {
                                        Tile Mtile = Framing.GetTileSafely(baseOffset.X + k, baseOffset.Y);
                                        Mtile.HasTile = true;
                                        Mtile.TileType = TileID.Platforms;
                                        Mtile.TileFrameY = (short)(18 * 19); //Boreal Wood Platform
                                        Mtile.Slope = SlopeType.Solid;
                                        Mtile.IsHalfBlock = false;
                                    }
                                }
                                break;
                            case 1:
                                for (int k = 0; k < 16; k += 5)
                                {
                                    WorldGen.PlaceTile(baseOffset.X + k, baseOffset.Y, TileID.WoodenBeam, true);
                                    if(k != 15)
                                        WorldGen.PlaceObject(baseOffset.X + k + 2, baseOffset.Y, TileID.Beds, true, 21);
                                }
                                break;
                            case 2:
                                for (int k = 0; k < 16; k += 5)
                                {
                                    WorldGen.PlaceTile(baseOffset.X + k, baseOffset.Y, TileID.WoodenBeam, true);
                                    if (i == 1 && (k == 5 || k == 10))
                                        WorldGen.PlaceTile(baseOffset.X + k, baseOffset.Y - 1, TileID.WoodenBeam, true);
                                }
                                break;
                        }
                    }
                }

                //Supplies Storage
                WorldGen.PlaceObject(newOriginX + 109, newOriginY + 63, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 113, newOriginY + 63, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 121, newOriginY + 63, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 112, newOriginY + 59, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 118, newOriginY + 59, TileID.FishingCrate, true, 0);

                WorldGen.PlaceChest(newOriginX + 111, newOriginY + 63, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 115, newOriginY + 63, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 118, newOriginY + 63, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 123, newOriginY + 63, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 110, newOriginY + 59, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 114, newOriginY + 59, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 120, newOriginY + 59, TileID.Containers, false, 5);
                WorldGen.PlaceChest(newOriginX + 122, newOriginY + 59, TileID.Containers, false, 5);

                //Medical Ward
                WorldGen.PlaceObject(newOriginX + 72, newOriginY + 80, TileID.Beds, true, 9);
                WorldGen.PlaceObject(newOriginX + 67, newOriginY + 80, TileID.Beds, true, 9);

                Point off = new(newOriginX + 63, newOriginY + 77);
                for (int i = 0; i < 11; i++)
                {
                    Tile Mtile = Framing.GetTileSafely(off.X + i, off.Y);
                    Mtile.HasTile = true;
                    Mtile.TileType = TileID.Platforms;
                    Mtile.TileFrameY = (short)(18 * 19); //Boreal Wood Platform
                    Mtile.Slope = SlopeType.Solid;
                    Mtile.IsHalfBlock = false;
                }

                WorldGen.PlaceObject(newOriginX + 64, newOriginY + 76, TileID.Bottles, true, 1);
                WorldGen.PlaceObject(newOriginX + 65, newOriginY + 76, TileID.Bottles, true, 2);
                WorldGen.PlaceObject(newOriginX + 67, newOriginY + 76, TileID.Bottles, true, 1);
                WorldGen.PlaceObject(newOriginX + 69, newOriginY + 76, TileID.Bottles, true, 2);
                WorldGen.PlaceObject(newOriginX + 71, newOriginY + 76, TileID.Bottles, true, 1);
                WorldGen.PlaceObject(newOriginX + 72, newOriginY + 76, TileID.Bottles, true, 1);

                WorldGen.PlaceChest(newOriginX + 62, newOriginY + 80, TileID.Containers, true, 12);

                //Kitchen
                off = new(newOriginX + 140, newOriginY + 77);
                for (int i = 0; i < 11; i++)
                {
                    Tile Mtile = Framing.GetTileSafely(off.X + i, off.Y);
                    Mtile.HasTile = true;
                    Mtile.TileType = TileID.Platforms;
                    Mtile.TileFrameY = (short)(18 * 19); //Boreal Wood Platform
                    Mtile.Slope = SlopeType.Solid;
                    Mtile.IsHalfBlock = false;
                }

                off = new(newOriginX + 140, newOriginY + 75);
                for (int i = 0; i < 11; i++)
                {
                    Tile Mtile = Framing.GetTileSafely(off.X + i, off.Y);
                    Mtile.HasTile = true;
                    Mtile.TileType = TileID.Platforms;
                    Mtile.TileFrameY = (short)(18 * 19); //Boreal Wood Platform
                    Mtile.Slope = SlopeType.Solid;
                    Mtile.IsHalfBlock = false;
                }

                WorldGen.PlaceObject(newOriginX + 144, newOriginY + 80, TileID.Sinks, true, 0);
                WorldGen.PlaceObject(newOriginX + 147, newOriginY + 80, TileID.Tables, true, 17);
                WorldGen.PlaceObject(newOriginX + 150, newOriginY + 80, TileID.CookingPots, true, 0);
                WorldGen.PlaceObject(newOriginX + 152, newOriginY + 80, TileID.Kegs, true, 0);
                WorldGen.PlaceChest(newOriginX + 154, newOriginY + 80, TileID.Containers, false, 5);

                WorldGen.PlaceObject(newOriginX + 143, newOriginY + 76, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass
                WorldGen.PlaceObject(newOriginX + 149, newOriginY + 76, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass
                WorldGen.PlaceObject(newOriginX + 141, newOriginY + 74, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass
                WorldGen.PlaceObject(newOriginX + 148, newOriginY + 74, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass
                WorldGen.PlaceObject(newOriginX + 152, newOriginY + 74, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass

                WorldGen.PlaceObject(newOriginX + 141, newOriginY + 76, TileID.Bowls, true, Main.rand.NextBool() ? 2 : 3); //Dishes, Bowel
                WorldGen.PlaceObject(newOriginX + 146, newOriginY + 76, TileID.Bowls, true, Main.rand.NextBool() ? 2 : 3); //Dishes, Bowel
                WorldGen.PlaceObject(newOriginX + 151, newOriginY + 76, TileID.Bowls, true, Main.rand.NextBool() ? 2 : 3); //Dishes, Bowel
                WorldGen.PlaceObject(newOriginX + 144, newOriginY + 74, TileID.Bowls, true, Main.rand.NextBool() ? 2 : 3); //Dishes, Bowel
                WorldGen.PlaceObject(newOriginX + 149, newOriginY + 74, TileID.Bowls, true, Main.rand.NextBool() ? 2 : 3); //Dishes, Bowel

                //Cafeteria
                WorldGen.PlaceObject(newOriginX + 107, newOriginY + 80, TileID.Tables, true, 16);
                WorldGen.PlaceObject(newOriginX + 112, newOriginY + 80, TileID.Tables, true, 16);
                WorldGen.PlaceObject(newOriginX + 117, newOriginY + 80, TileID.Tables, true, 16);
                WorldGen.PlaceObject(newOriginX + 121, newOriginY + 80, TileID.Tables, true, 16);

                WorldGen.PlaceObject(newOriginX + 105, newOriginY + 80, TileID.Chairs, true, 21);
                WorldGen.PlaceObject(newOriginX + 109, newOriginY + 80, TileID.Chairs, true, 21);
                WorldGen.PlaceObject(newOriginX + 110, newOriginY + 80, TileID.Chairs, true, 21);
                WorldGen.PlaceObject(newOriginX + 114, newOriginY + 80, TileID.Chairs, true, 21);
                WorldGen.PlaceObject(newOriginX + 115, newOriginY + 80, TileID.Chairs, true, 21);
                WorldGen.PlaceObject(newOriginX + 123, newOriginY + 80, TileID.Chairs, true, 21);

                WorldGen.PlaceObject(newOriginX + 106, newOriginY + 78, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass
                WorldGen.PlaceObject(newOriginX + 117, newOriginY + 78, TileID.Bottles, true, Main.rand.Next(4, 7)); //Cup, Mug, Glass

                //Cargo Storage
                WorldGen.PlaceObject(newOriginX + 106, newOriginY + 103, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 113, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 115, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 114, newOriginY + 100, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 119, newOriginY + 103, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 121, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 131, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 133, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 135, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 132, newOriginY + 100, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 134, newOriginY + 100, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 139, newOriginY + 102, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 144, newOriginY + 100, TileID.FishingCrate, true, 0);

                //Misc
                WorldGen.PlaceObject(newOriginX + 102, newOriginY + 61, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 104, newOriginY + 61, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 103, newOriginY + 59, TileID.FishingCrate, true, 0);

                WorldGen.PlaceObject(newOriginX + 48, newOriginY + 68, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 50, newOriginY + 68, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 49, newOriginY + 66, TileID.FishingCrate, true, 0);

                WorldGen.PlaceObject(newOriginX + 52, newOriginY + 74, TileID.FishingCrate, true, 0);
                WorldGen.PlaceObject(newOriginX + 54, newOriginY + 74, TileID.FishingCrate, true, 0);

                WorldGen.PlaceObject(newOriginX + 42, newOriginY + 62, TileID.Toilets, true, 0, direction: 1);
            }
            else
                WorldGen.PlaceChest(newOriginX + 66, newOriginY + 54, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);

            return true;
        }
    }
}