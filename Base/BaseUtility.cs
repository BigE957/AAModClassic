using log4net;
using Microsoft.Xna.Framework;
using System;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;

namespace AAModClassic.Base
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
            if (Main.netMode == NetmodeID.SinglePlayer) 
            {
                Main.NewText(s, colorR, colorG, colorB); 
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient) 
            { 
                Main.NewText(s, colorR, colorG, colorB); 
            }
            else if (sync && Main.netMode == NetmodeID.Server) 
            { 
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB)); 
            }
        }
    }
}