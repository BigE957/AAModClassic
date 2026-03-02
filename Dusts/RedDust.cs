using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class RedDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity.Y *= 3f;
			dust.velocity.X *= 3f;
			dust.scale *= 2f;
			dust.noGravity = true;
			dust.noLight = false;
		}
		
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return Color.White;
		}
	}
}