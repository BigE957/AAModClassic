using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class TimeDust : ModDust
	{
        public override bool Update(Dust dust)
        {
            dust.rotation += 1f;
            if (!dust.noLight)
            {
                float num17 = dust.scale * 0.25f;
                if (num17 > 1f)
                {
                    num17 = 1f;
                }
                float num18 = num17;
                float num19 = num17;
                float num20 = num17;
                num18 *= 1f;
                num19 *= 0.2f;
                num20 *= 0.1f;
                Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num17 * num18, num17 * num19, num17 * num20);
            }
            if (dust.customData != null && dust.customData is Player)
            {
                Player player3 = (Player)dust.customData;
                dust.position += player3.position - player3.oldPosition;
                dust.customData = null;
            }
            return true;
        }
        public static Color GetAlpha(Color newColor)
        {
            return new Color(255, 255, 255, 0);
        }
        
    }
}