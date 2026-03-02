using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class MireBubbleDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 1;
			dust.scale = 1.2f;
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
		}

		public override bool Update(Dust dust)
		{
            dust.velocity.Y -= 0.05f;
            dust.rotation += dust.velocity.X / 3f;
			dust.position += dust.velocity;
			int oldAlpha = dust.alpha;
			dust.alpha = (int)(dust.alpha * 1.2);
			if (dust.alpha == oldAlpha)
			{
				dust.alpha++;
			}
			if (dust.alpha >= 255)
			{
				dust.alpha = 255;
				dust.active = false;
			}
			return false;
		}
	}
}