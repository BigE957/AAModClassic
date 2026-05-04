using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Tiles.Decoration.Ancient;

namespace AAModClassic._Unreleased.Content.Parthenan.World
{
    public class ParthenanGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = ModContent.TileType<FulguritePlating_Tile>();
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<FulguriteBrick_Tile>();
            colorToTile[new Color(0, 0, 255)] = ModContent.TileType<StormCloud_Tile>();
            colorToTile[new Color(255, 0, 255)] = ModContent.TileType<FulgurGlass_Tile>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = ModContent.WallType<FulguritePlating_Wall>();
            colorToWall[new Color(255, 0, 255)] = ModContent.WallType<FulgurGlass_Wall>();
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets_Unreleased.ParthenanTileData, colorToTile, TexGenAssets_Unreleased.ParthenanWallData, colorToWall);

            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject(origin.X + 37, origin.Y + 45, (ushort)ModContent.TileType<AncientDataBank_Tile>());
            WorldGen.PlaceChest(origin.X + 32, origin.Y + 47, (ushort)ModContent.TileType<StormChest_Tile>());
            WorldGen.PlaceChest(origin.X + 41, origin.Y + 47, (ushort)ModContent.TileType<StormChest_Tile>());
            return true;
        }
    }
}
