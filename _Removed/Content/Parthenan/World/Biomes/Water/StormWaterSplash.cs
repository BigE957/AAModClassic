using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.World.Biomes.Water
{
    public class StormWaterSplash : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = DustID.Water;
		}

		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 170;
			dust.velocity *= 0.5f;
			dust.velocity.Y += 1f;
		}
	}
}