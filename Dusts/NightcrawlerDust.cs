using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic.Base.BaseMod.Base;


namespace AAModClassic.Dusts
{
    public class NightcrawlerDust : ModDust
    {
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;
			BaseDrawing.AddLight(dust.position, new Color(38, 152, 166));
            dust.scale -= 0.03f;
            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        } 
    }
}
