using System;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic
{
    internal enum ConversionType
    {
        MIRE,
        INFERNO,
    }

    class ConversionHandler
    {
        public static int startMireX = -1;
        public static int startMireY = -1;
        public static int genMireWidth = -1;

        public static int startInfernoX = -1;
        public static int startInfernoY = -1;
        public static int genInfernoWidth = -1;

        public static void ConvertDown(int centerX, int y, int width, ConversionType convertType)
        {
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 220 : worldSize == 2 ? 180 : 150;
            biomeRadius /= 2;
            switch (convertType)
            {
                case ConversionType.MIRE:
                    {
                        startMireX = centerX;
                        startMireY = y;
                        genMireWidth = width;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertDownMireCallback), null);
                        break;
                    }

                case ConversionType.INFERNO:
                    {
                        startInfernoX = centerX;
                        startInfernoY = y;
                        genInfernoWidth = width;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertDownInfernoCallback), null);
                        break;
                    }
            }
        }

        public static int GetWorldSize()
        {
            switch (Main.maxTilesX)
            {
                case 4200:
                    return 1;

                case 6400:
                    return 2;

                case 8400:
                    return 3;

                default:
                    return 1;
            }
        }

        #region Thread Callback Stuff
        public static void ConvertDownMireCallback(object threadContext)
        {
            try
            {
                Do_ConvertDownMire(threadContext);
            }
            catch (Exception)
            {
            }
        }

        public static void Do_ConvertDownMire(object threadContext)
        {
            Dodo_ConvertDown(startMireX, startMireY, genMireWidth, ConversionType.MIRE);
        }

        public static void ConvertDownInfernoCallback(object threadContext)
        {
            try
            {
                Do_ConvertDownInferno(threadContext);
            }
            catch (Exception)
            {
            }
        }

        public static void Do_ConvertDownInferno(object threadContext)
        {
            Dodo_ConvertDown(startInfernoX, startInfernoY, genInfernoWidth, ConversionType.INFERNO);
        }
        #endregion

        public static void Dodo_ConvertDown(int startX, int startY, int genWidth, ConversionType conversionType)
        {
            Mod mod = AAMod.instance;
            int tileGrass = 0, wallGrass = 0, tileStone = 0, wallStone = 0, tileSand = 0, tileSandHard = 0, wallSandHard = 0, 
                tileSandstone = 0, wallSandstone = 0, tileIce = 0, tileThorn = 0, tileWood = 0, tileLeaves = 0, wallLeaves = 0,
                livingwoodWall = 0;

            switch (conversionType)
            {
                case ConversionType.MIRE:
                    {
                        tileGrass = mod.Find<ModTile>("MireGrass").Type;
                        wallGrass = mod.Find<ModWall>("MireJungleWall").Type;
                        tileStone = mod.Find<ModTile>("Depthstone").Type;
                        wallStone = mod.Find<ModWall>("DepthstoneWall").Type;
                        tileSand = mod.Find<ModTile>("Depthsand").Type;
                        tileSandHard = mod.Find<ModTile>("DepthsandHardened").Type;
                        wallSandHard = mod.Find<ModWall>("DepthsandHardenedWall").Type;
                        tileSandstone = mod.Find<ModTile>("Depthsandstone").Type;
                        wallSandstone = mod.Find<ModWall>("DepthsandstoneWall").Type;
                        tileIce = mod.Find<ModTile>("IndigoIce").Type;
                        tileWood = mod.Find<ModTile>("LivingBogwood").Type;
                        tileLeaves = mod.Find<ModTile>("LivingBogleaves").Type;
                        wallLeaves = mod.Find<ModTile>("LivingBogleafWall").Type;
                        livingwoodWall = mod.Find<ModTile>("LivingBogwoodWall").Type;
                        break;
                    }

                case ConversionType.INFERNO:
                    {
                        tileGrass = mod.Find<ModTile>("InfernoGrass").Type;
                        wallGrass = mod.Find<ModWall>("InfernoGrassWall").Type;
                        tileStone = mod.Find<ModTile>("Torchstone").Type;
                        wallStone = mod.Find<ModWall>("TorchstoneWall").Type;
                        tileSand = mod.Find<ModTile>("Torchand").Type;
                        tileSandHard = mod.Find<ModTile>("TorchsandHardened").Type;
                        wallSandHard = mod.Find<ModWall>("TorchsandHardenedWall").Type;
                        tileSandstone = mod.Find<ModTile>("Infernosandstone").Type;
                        wallSandstone = mod.Find<ModWall>("InfernosandstoneWall").Type;
                        tileIce = mod.Find<ModTile>("Torchice").Type;
                        tileWood = mod.Find<ModTile>("LivingRazewood").Type;
                        tileLeaves = mod.Find<ModTile>("LivingRazeleaves").Type;
                        wallLeaves = mod.Find<ModTile>("LivingRazeleafWall").Type;
                        break;
                    }

                default:
                    break;
            }

            int centerX = startX, y = startY;
            for (int x1 = 0; x1 < genWidth; x1++)
            {
                while (y < (Main.maxTilesY - 50))
                {
                    Convert(centerX + x1, y, genWidth, tileGrass, wallGrass, tileStone, wallStone, tileSand, tileSandHard, wallSandHard, tileSandstone, wallSandstone, tileIce, tileThorn, tileWood, tileLeaves, wallLeaves, livingwoodWall);
                    y += genWidth * 2;
                }
            }
        }

        public static void Convert(int i, int j, int size, int tileGrass, int wallGrass, int tileStone, int wallStone, int tileSand, int tileSandHard, int wallSandHard, int tileSandstone, int wallSandstone, int tileIce, int tileThorn, int tileWood, int tileLeaves, int wallLeaves, int wallWood = 0)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1))
                    {
                        int type = Main.tile[k, l].TileType;
                        int wall = Main.tile[k, l].WallType;

                        if (wallGrass != 0 && WallID.Sets.Conversion.Grass[wall] && wall != wallGrass)
                        {
                            Main.tile[k, l].WallType = (ushort)wallGrass;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallStone != 0 && WallID.Sets.Conversion.Stone[wall] && wall != wallStone)
                        {
                            Main.tile[k, l].WallType = (ushort)wallStone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallSandHard != 0 && WallID.Sets.Conversion.HardenedSand[wall] && wall != wallSandHard)
                        {
                            Main.tile[k, l].WallType = (ushort)wallSandHard;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallSandstone != 0 && WallID.Sets.Conversion.Sandstone[wall] && wall != wallSandstone)
                        {
                            Main.tile[k, l].WallType = (ushort)wallSandstone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallLeaves != 0 && wall == WallID.LivingLeaf && wall != wallLeaves)
                        {
                            Main.tile[k, l].WallType = (ushort)wallLeaves;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallWood != 0 && wall == WallID.LivingWood && wall != wallLeaves)
                        {
                            Main.tile[k, l].WallType = (ushort)wallWood;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }

                        if (tileStone != 0 && (Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != tileStone)
                        {
                            Main.tile[k, l].TileType = (ushort)tileStone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileGrass != 0 && TileID.Sets.Conversion.Grass[type] && type != tileGrass)
                        {
                            Main.tile[k, l].TileType = (ushort)tileGrass;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileIce != 0 && TileID.Sets.Conversion.Ice[type] && type != tileIce)
                        {
                            Main.tile[k, l].TileType = (ushort)tileIce;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileSand != 0 && TileID.Sets.Conversion.Sand[type] && type != tileSand)
                        {
                            Main.tile[k, l].TileType = (ushort)tileSand;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileSandHard != 0 && TileID.Sets.Conversion.HardenedSand[type] && type != tileSandHard)
                        {
                            Main.tile[k, l].TileType = (ushort)tileSandHard;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileSandstone != 0 && TileID.Sets.Conversion.Sandstone[type] && type != tileSandstone)
                        {
                            Main.tile[k, l].TileType = (ushort)tileSandstone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileThorn != 0 && TileID.Sets.Conversion.Thorn[type] && type != tileThorn)
                        {
                            Main.tile[k, l].TileType = (ushort)tileThorn;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileWood != 0 && (type == TileID.LivingMahogany || type == TileID.LivingWood) && type != tileWood)
                        {
                            Main.tile[k, l].TileType = (ushort)tileWood;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileLeaves != 0 && (type == TileID.LivingMahoganyLeaves || type == TileID.LeafBlock) && type != tileLeaves)
                        {
                            Main.tile[k, l].TileType = (ushort)tileLeaves;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                    }
                }
            }
        }
    }
}