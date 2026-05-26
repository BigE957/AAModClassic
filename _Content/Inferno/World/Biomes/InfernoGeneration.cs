using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Conversions;
using AAModClassic.Tiles;
using AAModClassic.Tiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    internal class InfernoTexGenAssets : ModSystem
    {
        internal static TexGenData VolcanoTileData;
        internal static TexGenData VolcanoWallData;
        internal static TexGenData VolcanoLiquidData;

        public override void OnModLoad()
        {
            VolcanoTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Volcano", AssetRequestMode.ImmediateLoad).Value);
            VolcanoWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/VolcanoWalls", AssetRequestMode.ImmediateLoad).Value);
            VolcanoLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/VolcanoLava", AssetRequestMode.ImmediateLoad).Value);
        }
    }

    public class InfernoGeneration : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            //--- Initial variable creation
            //ushort tileGrass = (ushort)mod.Find<ModTile>("InfernoGrass").Type, tileStone = (ushort)mod.Find<ModTile>("Torchstone").Type, tileSnow = (ushort)mod.Find<ModTile>("TorchAsh").Type,
            //tileIce = (ushort)mod.Find<ModTile>("Torchice").Type, tileSand = (ushort)mod.Find<ModTile>("Torchsand").Type, tileSandHardened = (ushort)mod.Find<ModTile>("TorchsandHardened").Type, tileSandstone = (ushort)mod.Find<ModTile>("Torchsandstone").Type,
            //LivingWood = (ushort)ModContent.TileType<LivingRazewood_Tile>(), LivingLeaves = (ushort)ModContent.TileType<LivingRazeleaves_Tile>();

            ushort StoneWall = (ushort)ModContent.WallType<TorchstoneWall_Wall>(),
            SandstoneWall = (ushort)ModContent.WallType<TorchsandstoneWall_Wall>(),
            HardenedSandWall = (ushort)ModContent.WallType<TorchsandHardenedWall_Wall>(),
            GrassWall = (ushort)ModContent.WallType<InfernoGrassWall_Wall>();


            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<Torchstone_Tile>(),
                [new Color(0, 0, 255)] = ModContent.TileType<Torchstone_Tile>(),
                [new Color(0, 255, 0)] = ModContent.TileType<ScorchedDynastyWoodUnsafe_Tile>(),
                [new Color(255, 255, 0)] = ModContent.TileType<ScorchedShingles_Tile>(),
                [new Color(255, 0, 255)] = ModContent.TileType<ScorchedPlatform_Tile>(),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            HashSet<int> protectedTiles = [
                ModContent.TileType<ScorchedDynastyWoodUnsafe_Tile>(),
                ModContent.TileType<ScorchedShingles_Tile>(),
                ModContent.TileType<ScorchedPlatform_Tile>(),
            ];

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.WallType<TorchstoneWall_Wall>(),
                [new Color(0, 0, 255)] = ModContent.WallType<ScorchedDynastyWoodWall_Wall>(),
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGen gen = TexGen.GetTexGenerator(InfernoTexGenAssets.VolcanoTileData, colorToTile, InfernoTexGenAssets.VolcanoWallData, colorToWall, InfernoTexGenAssets.VolcanoLiquidData, unbreakableTiles: protectedTiles, unbreakableWalls: [ModContent.WallType<ScorchedDynastyWoodWall_Wall>()]);
            Point newOrigin = new Point(origin.X, origin.Y - 30); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(1, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0)
            }));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //convert tiles
			{
                new InWorld(),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new ConvertTile(ModContent.GetInstance<InfernoConversion>().Type) //actually place the tile
			}));

            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 80;
            gen.Generate(genX, genY, true, true);

            //WorldGen.PlaceObject(genX + 65, genY + 4, Terraria.ModLoader.ModContent.TileType<DracoAltarS_Tile>());
            WorldGen.PlaceObject(genX + 24, genY + 307, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 33, genY + 313, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 46, genY + 314, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 57, genY + 316, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 67, genY + 316, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 78, genY + 317, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 87, genY + 315, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 96, genY + 312, ModContent.TileType<DragonEgg_Tile>());
            WorldGen.PlaceObject(genX + 103, genY + 307, ModContent.TileType<DragonEgg_Tile>());
            NetMessage.SendObjectPlacement(-1, genX + 24, genY + 307, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 33, genY + 313, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 46, genY + 314, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 57, genY + 316, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 67, genY + 316, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 78, genY + 317, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 87, genY + 315, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 96, genY + 312, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);
            NetMessage.SendObjectPlacement(-1, genX + 103, genY + 307, (ushort)ModContent.TileType<DragonEgg_Tile>(), 0, 0, -1, -1);

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        if (Main.rand.NextBool(15))
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<DragonAltarUnsafe_Tile>());
                        }
                    }
                }
            }

            return true;
        }
    }

    public class InfernoDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [new Color(0, 0, 255)] = -2,
                [new Color(0, 255, 0)] = -2,
                [new Color(255, 255, 0)] = -2,
                [new Color(255, 0, 255)] = -2,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = TexGen.GetTexGenerator(InfernoTexGenAssets.VolcanoTileData, colorToTile);
            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 80;
            gen.Generate(genX, genY, true, true);

            return true;
        }
    }

}
