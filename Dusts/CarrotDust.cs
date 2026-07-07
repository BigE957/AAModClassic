using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class CarrotDust : ModDust
	{
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = false;
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            return false;
        }
    }
}