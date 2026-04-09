using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class GroviteDust : ModDust
    {

        public override bool Update(Dust dust)
        {
            dust.rotation += (float)(Math.PI / 16f) * dust.scale;
            dust.scale *= .8f;
            dust.velocity *= 0.98f;
            dust.position += dust.velocity;
            if (dust.scale <= 0.2f) { dust.active = false; }
            return false;
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, AAPlayer.groviteColor.R / 255 * 0.3f * strength, AAPlayer.groviteColor.G / 255 * 0.3f * strength, AAPlayer.groviteColor.B / 255 * 0.3f * strength);
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}