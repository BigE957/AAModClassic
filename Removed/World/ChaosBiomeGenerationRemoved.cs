using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Removed.Tiles.Fulgurite.Parthenan;
using AAModClassic.Removed.Tiles.Fulgurite.Parthenan.Ancient;
using AAModClassic.Removed.Tiles.Fulgurite.Parthenan.Ancient.Walls;
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

    // the original one just... doesnt try again when fail to place?
    public class SurfaceMushroom_Refactored : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            ushort tileGrass = (ushort)ModContent.TileType<Mycelium>(); 

            int worldSize = BaseWorldGen.GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150, biomeWidthHalf = biomeWidth / 2; 
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;

            WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[] 
            {
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.CrimsonGrass }),
                new RadialDitherCenter(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10),
                new SetModTile(tileGrass, true, true) 
            }));
            WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[] 
{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Ebonstone, TileID.Crimstone }), 
                new RadialDitherCenter(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10), 
                new SetModTile(TileID.Stone, true, true) 
            }));

            return true;
        }
    }

    public class RadialDitherCenter : GenAction
    {
        private int _width, _height;
        private float _innerRadius, _outerRadius;

        public RadialDitherCenter(int width, int height, float innerRadius, float outerRadius)
        {
            _width = width;
            _height = height;
            _innerRadius = innerRadius;
            _outerRadius = outerRadius;
        }

        public override bool Apply(Point origin, int x, int y, params object[] args)
        {
            Vector2 value = new((float)origin.X + _width / 2, (float)origin.Y + _height / 2);
            Vector2 value2 = new(x, y);
            float num = Vector2.Distance(value2, value);
            float num2 = Math.Max(0f, Math.Min(1f, (num - _innerRadius) / (_outerRadius - _innerRadius)));
            if (_random.NextDouble() > num2)
            {
                return UnitApply(origin, x, y, args);
            }
            return Fail();
        }
    }

    public class Parthenan : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = ModContent.TileType<AncientFulguritePlatingS>();
            colorToTile[new Color(255, 0, 0)] = ModContent.TileType<AncientFulguriteBrickS>();
            colorToTile[new Color(0, 0, 255)] = ModContent.TileType<StormCloud>();
            colorToTile[new Color(255, 0, 255)] = ModContent.TileType<AncientFulgurGlassS>();
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = ModContent.WallType<AncientFulguritePlatingWallS>();
            colorToWall[new Color(255, 0, 255)] = ModContent.WallType<AncientFulgurGlassWallS>();
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = TexGen.GetTexGenerator(TexGenAssetsRemoved.ParthenanTileData, colorToTile, TexGenAssetsRemoved.ParthenanWallData, colorToWall);
            
            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject((int)(origin.X) + 34, (int)(origin.Y) + 47, (ushort)ModContent.TileType<AncientDataBank>());
            WorldGen.PlaceChest((origin.X) + 32, (origin.Y) + 47, (ushort)ModContent.TileType<AncientStormChest>(), true);
            WorldGen.PlaceChest((origin.X) + 41, (origin.Y) + 47, (ushort)ModContent.TileType<AncientStormChest>(), true);
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