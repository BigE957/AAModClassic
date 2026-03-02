using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class PrismDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
            dust.velocity.X *= 0.3f;
        }

        public Color dustColor = Color.White;

        public override bool MidUpdate(Dust dust)
        {
            dust.scale *= 0.9f;
            if (!dust.noGravity)
            {
                dust.velocity.Y += 0.05f;
            }
            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, dustColor.R / 255 * strength, dustColor.G / 255 * strength, dustColor.B / 255 * strength);
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            dustColor = lightColor;
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}