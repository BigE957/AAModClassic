using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class FulguriteDust : ModDust
	{
        public override bool Update(Dust dust)
        {
            dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
            Dust expr_43F_cp_0 = dust;
            expr_43F_cp_0.velocity.X *= 0.3f;
            dust.scale *= 0.7f;
            return true;
        }

        public override bool MidUpdate(Dust dust)
        {
            
            if (!dust.noGravity)
            {
                Dust expr_1256_cp_0 = dust;
                expr_1256_cp_0.velocity.Y += 0.05f;
            }
            if (!dust.noLight)
            {
                float num61 = dust.scale * 1.4f;
                if (num61 > 1f)
                {
                    num61 = 1f;
                }
                Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.2f, num61 * 0.7f, num61);
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B);
        }
    }
}