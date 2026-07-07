using log4net;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;

namespace AAModClassic.Base.BaseMod.Base
{
    public class BaseUtility
    {
        //------------------------------------------------------//
        //------------------BASE UTILITY CLASS------------------//
        //------------------------------------------------------//
        // Contains utility methods a mod might want to use.    //
        //------------------------------------------------------//
        //  Author(s): Grox the Great                           //
        //------------------------------------------------------// 
	

        public static void LogFancy(string prefix, Exception e)
        {
            LogFancy(prefix, null, e);
        }

        public static void LogFancy(string prefix, string logText, Exception e = null)
        {
            ILog logger = LogManager.GetLogger("Terraria");
            if (e != null)
            {
                logger.Info(">---------<");
                logger.Error(prefix + e.Message);
                logger.Error(e.StackTrace);
                logger.Info(">---------<");
                //ErrorLogger.Log(prefix + e.Message); ErrorLogger.Log(e.StackTrace);	ErrorLogger.Log(">---------<");	
            }
            else
            {
                logger.Info(">---------<");
                logger.Info(prefix + logText);
                logger.Info(">---------<");
                //ErrorLogger.Log(prefix + logText);
            }
        }

        public static bool CanHit(Rectangle rect, Rectangle rect2)
        {
            return Collision.CanHit(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, new Vector2(rect2.X, rect2.Y), rect2.Width, rect2.Height);
        }

        /*
         * Fills an int array entirely with the value.
         */
        public static int[] FillArray(int[] array, int value)
        {
            for (int m = 0; m < array.Length; m++) { array[m] = value; }
            return array;
        }

        /*
         * Returns true if value is in the given int array.
         */
        public static bool InArray(int[] array, int value)
        {
            for (int m = 0; m < array.Length; m++) { if (value == array[m]) { return true; } }
            return false;
        }

        /*
         * Returns true if value is in the given int array.
         * 
         * index : sets this to the index of the value in the array.
         */
        public static bool InArray(int[] array, int value, ref int index)
        {
            for (int m = 0; m < array.Length; m++) { if (value == array[m]) { index = m; return true; } }
            return false;
        }

        /*
		 * Alters the brightness of the color by the multiplier.
		 */
        public static Color ColorMult(Color color, float mult)
        {
            int r = Math.Max(0, Math.Min(255, (int)(color.R * mult)));
            int g = Math.Max(0, Math.Min(255, (int)(color.G * mult)));
            int b = Math.Max(0, Math.Min(255, (int)(color.B * mult)));
            return new Color(r, g, b, color.A);
        }

        /*
         * Clamps the first color to be no lower then the values of the second color.
         */
        public static Color ColorClamp(Color color1, Color color2)
        {
            int r = color1.R;
            int g = color1.G;
            int b = color1.B;
            int a = color1.A;
            if (r < color2.R) { r = color2.R; }
            if (g < color2.G) { g = color2.G; }
            if (b < color2.B) { b = color2.B; }
            if (a < color2.A) { a = color2.A; }
            return new Color(r, g, b, a);
        }

        /*
		 * Allows lerping between N float values.
		 */
        public static float MultiLerp(float percent, params float[] floats)
        {
            float per = 1f / ((float)floats.Length - 1);
            float total = per;
            int currentID = 0;
            while (percent / total > 1f && currentID < floats.Length - 2) { total += per; currentID++; }
            return MathHelper.Lerp(floats[currentID], floats[currentID + 1], (percent - per * currentID) / per);
        }

        /*
		 * Allows lerping between N vector values.
		 */
        public static Vector2 MultiLerpVector(float percent, params Vector2[] vectors)
        {
            float per = 1f / ((float)vectors.Length - 1);
            float total = per;
            int currentID = 0;
            while (percent / total > 1f && currentID < vectors.Length - 2) { total += per; currentID++; }
            return Vector2.Lerp(vectors[currentID], vectors[currentID + 1], (percent - per * currentID) / per);
        }

        /*
		 * Allows lerping between N color values.
		 */
        public static Color MultiLerpColor(float percent, params Color[] colors)
        {
            float per = 1f / ((float)colors.Length - 1);
            float total = per;
            int currentID = 0;
            while (percent / total > 1f && currentID < colors.Length - 2) { total += per; currentID++; }
            return Color.Lerp(colors[currentID], colors[currentID + 1], (percent - per * currentID) / per);
        }

        /*
         * Returns a rotation from startPos pointing to endPos.
         */
        public static float RotationTo(Vector2 startPos, Vector2 endPos)
        {
            return (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
        }

        /*
         * Rotates a vector based on the origin and the given point to 'look' at.
         * The rotation vector is *NOT* relative to the origin.
         */
        public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }

        /*
         * Returns a random position near the position given.
         * 
         * rand : a Random to use to get the position.
         * minDistance : the minimum amount of distance from the position.
         * maxDistance : the maximum amount of distance from the position.
         * circular : If true, gets a random point around a circle instead of a square.
         */
        public static Vector2 GetRandomPosNear(Vector2 pos, UnifiedRandom rand, int minDistance, int maxDistance, bool circular = false)
        {
            int distance = maxDistance - minDistance;
            if (!circular)
            {
                float newPosX = pos.X + (Main.rand.NextBool(2) ? -(minDistance + rand.Next(distance)) : minDistance + rand.Next(distance));
                float newPosY = pos.Y + (Main.rand.NextBool(2) ? -(minDistance + rand.Next(distance)) : minDistance + rand.Next(distance));
                return new Vector2(newPosX, newPosY);
            }

            return RotateVector(pos, pos + new Vector2(minDistance + rand.Next(distance)), MathHelper.Lerp(0, (float)(Math.PI * 2f), (float)rand.NextDouble()));
        }

        /*
         * Sends the given string to chat, with the given color.
         */
        public static void Chat(string s, Color color, bool sync = true)
        {
            Chat(s, color.R, color.G, color.B, sync);
        }

        /*
         * Sends the given string to chat, with the given color values.
         */
        public static void Chat(string s, byte colorR = 255, byte colorG = 255, byte colorB = 255, bool sync = true)
        {
            if (Main.netMode == NetmodeID.SinglePlayer) { Main.NewText(s, colorR, colorG, colorB); }
            else
            if (Main.netMode == NetmodeID.MultiplayerClient) { Main.NewText(s, colorR, colorG, colorB); }
            else //if(sync){ NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), Main.myPlayer); } }else
            if (sync && Main.netMode == NetmodeID.Server) { ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB)); }
        }
    }
}