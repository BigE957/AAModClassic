using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class VoidDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 0.7f;
			dust.noGravity = true;
		}
		public override bool MidUpdate(Dust dust)
		{
			if (!dust.noLight)
			{
				float strength = dust.scale * 1.4f;
				if (strength > 1f)
				{
					strength = 1f;
				}
				Lighting.AddLight(dust.position, 0.3f * strength, 0.0f * strength, 0.1f * strength);
			}
			return false;
		}

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(200, 0, 0, 25);
        }
    }
}