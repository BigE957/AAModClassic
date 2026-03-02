using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class CthulhuDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
        }

        public override bool Update(Dust dust)
        {
            if (!dust.noGravity)
            {
                Dust expr_1256_cp_0 = dust;
                expr_1256_cp_0.velocity.Y += 0.05f;
            }
            if (dust.customData != null && dust.customData is NPC)
            {
                NPC nPC = (NPC)dust.customData;
                dust.position += nPC.position - nPC.oldPos[1];
            }
            else if (dust.customData != null && dust.customData is Player)
            {
                Player player5 = (Player)dust.customData;
                dust.position += player5.position - player5.oldPosition;
            }
            else if (dust.customData != null && dust.customData is Vector2)
            {
                Vector2 vector2 = (Vector2)dust.customData - dust.position;
                if (vector2 != Vector2.Zero)
                {
                    vector2.Normalize();
                }
                dust.velocity = (dust.velocity * 4f + vector2 * dust.velocity.Length()) / 5f;
            }
            if (!dust.noLight)
            {
                float num61 = dust.scale * 1.4f;

                if (num61 > 1f)
                {
                    num61 = 1f;
                }
                Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.3f, num61 * 0.3f, num61 * 0.7f);
                
            }
            return true;
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
                Lighting.AddLight(dust.position, 0f * strength, 0.05f * strength, 0.1f * strength);
            }
            if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
            {
                dust.scale *= 0.9f;
                dust.velocity *= 0.10f;
            }
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }
}