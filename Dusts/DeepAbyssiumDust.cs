using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class DeepAbyssiumDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
            dust.scale = 1.2f;
            dust.noGravity = false;
            dust.velocity /= 2f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;
            dust.scale -= 0.03f;
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
