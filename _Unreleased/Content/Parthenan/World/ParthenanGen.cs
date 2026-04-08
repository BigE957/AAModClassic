using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles;
using AAModClassic.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using AAModClassic._Removed.Content.Parthenan.Tiles;
using AAModClassic._Removed.Content.Parthenan.Tiles.Ancient.Walls;
using AAModClassic._Removed.Content.Parthenan.Tiles.Ancient;

namespace AAModClassic._Unreleased.Content.Parthenan.World
{
    public class ParthenanGen : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = ModContent.TileType<AncientFulguritePlatingS_Tile>();
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<AncientFulguriteBrickS_Tile>();
            colorToTile[new Color(0, 0, 255)] = ModContent.TileType<StormCloud_Tile>();
            colorToTile[new Color(255, 0, 255)] = ModContent.TileType<AncientFulgurGlassS_Tile>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = ModContent.WallType<AncientFulguritePlatingS_Wall>();
            colorToWall[new Color(255, 0, 255)] = ModContent.WallType<AncientFulgurGlassS_Wall>();
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(TexGenAssets_Unreleased.ParthenanTileData, colorToTile, TexGenAssets_Unreleased.ParthenanWallData, colorToWall);

            //TODOSIEGE some of these dont actually place in world
            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject(origin.X + 37, origin.Y + 45, (ushort)ModContent.TileType<AncientDataBank_Tile>());
            WorldGen.PlaceChest(origin.X + 32, origin.Y + 47, (ushort)ModContent.TileType<AncientStormChest_Tile>());
            WorldGen.PlaceChest(origin.X + 41, origin.Y + 47, (ushort)ModContent.TileType<AncientStormChest_Tile>());
            return true;
        }
    }
}
