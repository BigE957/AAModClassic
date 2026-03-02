using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class ShroomDust : ModDust
	{

        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            if (!dust.noLight)
            {
                float strength = dust.scale;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, 0f * strength, 0f * strength, 0.5f * strength);
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}