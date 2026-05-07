using System;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Conversions
{
    public enum ConversionType
    {
        MIRE,
        INFERNO,
    }

    public static class ConversionHandler
    {
        public static void ConvertDown(int centerX, int y, int width, ConversionType convertType)
        {
            var args = (centerX, y, width, convertType);
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try { Dodo_ConvertDown(args.centerX, args.y, args.width, args.convertType); }
                catch (Exception e) { AAMod.instance.Logger.Error("Conversion thread error: " + e); }
            });
        }

        public static void ConvertDownBoth(int mireX, int infernoX, int y, int width)
        {
            var args = (mireX, infernoX, y, width);
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    Dodo_ConvertDown(args.mireX, args.y, args.width, ConversionType.MIRE);
                    Dodo_ConvertDown(args.infernoX, args.y, args.width, ConversionType.INFERNO);
                }
                catch (Exception e) { AAMod.instance.Logger.Error("Conversion thread error: " + e); }
            });
        }

        public static void Dodo_ConvertDown(int startX, int startY, int genWidth, ConversionType conversionType)
        {
            AAMod.instance.Logger.Info("Beginning conversion of type: " + conversionType);
            AAMod.instance.Logger.Info($"Start Position: ({startX}, {startY})");
            AAMod.instance.Logger.Info("Width: " + genWidth);

            int convType = conversionType == ConversionType.MIRE
                ? ModContent.GetInstance<MireConversion>().Type
                : ModContent.GetInstance<InfernoConversion>().Type;

            int iterations = 0;
            int finalY = startY;
            for (int x1 = -genWidth; x1 < genWidth; x1++)
            {
                int y = startY;
                while (y < Main.maxTilesY - 50)
                {
                    if (WorldGen.InWorld(startX + x1, y))
                    {
                        Tile tile = Main.tile[startX + x1, y];
                        if (tile != null && (tile.HasTile || tile.WallType != WallID.None))
                        {
                            WorldGen.Convert(startX + x1, y, convType, 1, true, true);
                            iterations++;
                        }
                    }
                    y++;
                }
                finalY = y;
            }

            AAMod.instance.Logger.Info("Ending conversion of type: " + conversionType);
            AAMod.instance.Logger.Info($"End Position: ({startX + genWidth}, {finalY})");
            AAMod.instance.Logger.Info("Convert Calls: " + iterations);
        }
    }
}