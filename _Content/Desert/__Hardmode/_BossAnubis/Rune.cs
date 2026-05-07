using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode._BossAnubis
{
    public class Rune : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, Main.rand.Next(8) * 20, 16, 20);
            dust.scale *= .8f;
            dust.velocity *= 2;
        }

        public override bool Update(Dust dust)
        {
            dust.scale -= 0.02f;

            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }

            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, 0.3f * strength, 0.0f * strength, 0f * strength);
            }

            return false;
        }
    }
}