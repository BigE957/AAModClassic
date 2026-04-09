using AAModClassic._Unreleased.Content.SunkenShip.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.SunkenShip.World
{
    public class SunkenShipGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;


            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            //TODOREFACTOR see if rotted wood uses era accurate sprite
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<RottedDynastyWoodS_Tile>();
            colorToTile[new Color(0, 255, 0)] = ModContent.TileType<RottedPlatform_Tile>();
            colorToTile[new Color(0, 0, 255)] = TileID.Rope;
            colorToTile[new Color(0, 255, 255)] = ModContent.TileType<CthulhuPortal_Tile>();
            colorToTile[new Color(150, 150, 150)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = ModContent.WallType<RottedWall_Wall>();
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets_Unreleased.ShipTileData, colorToTile, TexGenAssets_Unreleased.ShipWallData, colorToWall, TexGenAssets_Unreleased.ShipLiquidData);
            
            gen.Generate(origin.X, origin.Y - 28, true, true);
            
            WorldGen.PlaceChest(origin.X + 13, origin.Y - 28 + 26, (ushort)ModContent.TileType<SunkenChest_Tile>(), true);
            return true;
        }
    }
}