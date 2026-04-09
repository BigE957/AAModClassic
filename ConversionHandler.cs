using AAModClassic.World.Conversions;
using System;
using System.Threading;
using Terraria;
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
            int centerX = startX, y = startY;
            for (int x1 = 0; x1 < genWidth; x1++)
            {
                while (y < (Main.maxTilesY - 50))
                {
                    WorldGen.Convert(centerX + x1, y, conversionType == ConversionType.MIRE ? ModContent.GetInstance<MireConversion>().Type : ModContent.GetInstance<InfernoConversion>().Type, genWidth, true, true);
                    y += genWidth * 2;
                }
            }
        }
    }
}