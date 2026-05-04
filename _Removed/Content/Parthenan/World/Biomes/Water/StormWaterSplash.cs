using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.World.Biomes.Water
{
    public class StormWaterSplash : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = 33;
		}

		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 170;
			dust.velocity *= 0.5f;
			dust.velocity.Y += 1f;
		}
	}
}