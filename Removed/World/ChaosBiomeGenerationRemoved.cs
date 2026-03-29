using AAModClassic.Base.BaseMod.Base;
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

namespace AAModClassic.Removed.World
{
    public class TexGenAssetsRemoved : ModSystem
    {
        public static TexGenData ParthenanTileData;
        public static TexGenData ParthenanWallData;

        public static TexGenData ShipTileData;
        public static TexGenData ShipWallData;
        public static TexGenData ShipLiquidData;

        public override void OnModLoad()
        {
            ParthenanTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/Removed/World/Parthenan", AssetRequestMode.ImmediateLoad).Value);
            ParthenanWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/Removed/World/ParthenanWalls", AssetRequestMode.ImmediateLoad).Value);

            ShipTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/Removed/World/Ship", AssetRequestMode.ImmediateLoad).Value);
            ShipWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/Removed/World/ShipWalls", AssetRequestMode.ImmediateLoad).Value);
            ShipLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/Removed/World/ShipWater", AssetRequestMode.ImmediateLoad).Value);
        }
    }

    public class Parthenan : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = mod.Find<ModTile>("FulguritePlatingS").Type;
            colorToTile[new Color(255, 0, 0)] = mod.Find<ModTile>("FulguriteBrickS").Type;
            colorToTile[new Color(0, 0, 255)] = mod.Find<ModTile>("StormCloud").Type;
            colorToTile[new Color(255, 0, 255)] = mod.Find<ModTile>("FulgurGlassS").Type;
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = mod.Find<ModWall>("FulguritePlatingWallS").Type;
            colorToWall[new Color(255, 0, 255)] = mod.Find<ModTile>("FulgurGlassWall").Type;
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(TexGenAssetsRemoved.ParthenanTileData, colorToTile, TexGenAssetsRemoved.ParthenanWallData, colorToWall);
            
            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject((int)(origin.X) + 34, (int)(origin.Y) + 47, (ushort)mod.Find<ModTile>("DataBank").Type);
            WorldGen.PlaceChest((origin.X) + 32, (origin.Y) + 47, (ushort)mod.Find<ModTile>("StormChest").Type, true);
            WorldGen.PlaceChest((origin.X) + 41, (origin.Y) + 47, (ushort)mod.Find<ModTile>("StormChest").Type, true);
            return true;
        }
    }

    public class BOTE : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;


            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.Find<ModTile>("RottedDynastyWoodS").Type;
            colorToTile[new Color(0, 255, 0)] = mod.Find<ModTile>("RottedPlatform").Type;
            colorToTile[new Color(0, 0, 255)] = TileID.Rope;
            colorToTile[new Color(0, 255, 255)] = mod.Find<ModTile>("CthulhuPortal").Type;
            colorToTile[new Color(150, 150, 150)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.Find<ModWall>("RottedWall").Type;
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(TexGenAssetsRemoved.ShipTileData, colorToTile, TexGenAssetsRemoved.ShipWallData, colorToWall, TexGenAssetsRemoved.ShipLiquidData);
            
            gen.Generate(origin.X, origin.Y - 28, true, true);
            
            WorldGen.PlaceChest((origin.X) + 13, (origin.Y - 28) + 26, (ushort)mod.Find<ModTile>("SunkenChest").Type, true);
            return true;
        }
    }
}