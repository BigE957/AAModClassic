using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class MireSporeDust : ModDust
	{
        public override void SetStaticDefaults()
        {
            UpdateType = DustID.JungleSpore; // probably not needed but keeping for good luck
        }

        public override void OnSpawn(Dust dust)
        {

        }

        public override bool Update(Dust dust)
        {
            dust.velocity.X += (float)Main.rand.Next(-10, 11) * 0.003f;
            dust.velocity.Y += (float)Main.rand.Next(-10, 11) * 0.003f;
            if ((double)dust.velocity.X > 0.35)
            {
                dust.velocity.X = 0.35f;
            }
            if ((double)dust.velocity.X < -0.35)
            {
                dust.velocity.X = -0.35f;
            }
            if ((double)dust.velocity.Y > 0.35)
            {
                dust.velocity.Y = 0.35f;
            }
            if ((double)dust.velocity.Y < -0.35)
            {
                dust.velocity.Y = -0.35f;
            }
            dust.scale += 0.0085f;
            float num7 = dust.scale * 0.7f;
            if (num7 > 1f)
            {
                num7 = 1f;
            }
            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num7, num7 * 0.3f, num7);

            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            float num = (float)(255 - dust.alpha) / 255f;
            num = (num + 3f) / 4f;
            int num4 = (int)((float)(int)lightColor.R * num);
            int num3 = (int)((float)(int)lightColor.G * num);
            int num2 = (int)((float)(int)lightColor.B * num);
            int num6 = lightColor.A - dust.alpha;
            if (num6 < 0)
            {
                num6 = 0;
            }
            if (num6 > 255)
            {
                num6 = 255;
            }
            return new Color(num4, num3, num2, num6);
        }
    }
}