using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Biomes.Waters
{
    public class TerraWaterSplash : ModDust
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