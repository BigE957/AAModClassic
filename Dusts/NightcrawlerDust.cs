using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace AAModClassic.Dusts
{
    public class NightcrawlerDust : ModDust
    {
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;
            Lighting.AddLight(dust.position, new Color(38, 152, 166).ToVector3());
            dust.scale -= 0.03f;
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}
