using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class MushDust : ModDust
	{
        public override bool Update(Dust dust)
        {
            dust.alpha = 50;
            return true;
        }
        public override bool MidUpdate(Dust dust)
        {
            dust.rotation += dust.velocity.X / 3f;
            if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
            {
                dust.scale *= 0.9f;
                dust.velocity *= 0.10f;
            }
            return false;
        }
        
    }
}