using Microsoft.Xna.Framework;
using Terraria;
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
    }
}
