using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public class ColorUtils
    {
        public static Color COLOR_GLOWPULSE => Color.White * (Main.mouseTextColor / 255f);

        public static Color GetDamageClassColor(DamageClass damage)
        {
            DamageClass damageClassReal = PlayerUtils.StandardizeDamageClasses(damage);
            if (damageClassReal == DamageClass.Melee)
                return Color.Firebrick;
            else if (damageClassReal == DamageClass.Ranged)
                return Color.SeaGreen;
            else if (damageClassReal == DamageClass.Magic)
                return Color.Violet;
            else if (damageClassReal == DamageClass.Summon)
                return Color.Cyan;
            else if (damageClassReal == DamageClass.Throwing)
                return Color.DarkOrange;
            else
                return Color.White;
        }

        public static Color MulticolorLerp(float increment, params Color[] colors)
        {
            increment %= 0.999f;
            int currentColorIndex = (int)(increment * colors.Length);
            Color currentColor = colors[currentColorIndex];
            Color nextColor = colors[(currentColorIndex + 1) % colors.Length];
            return Color.Lerp(currentColor, nextColor, increment * colors.Length % 1f);
        }
    }
}
