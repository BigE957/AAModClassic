using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Biomes.Water
{
    public class FogWaterSplash : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = DustID.Water;
		}

		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 255;
			dust.velocity *= 0.5f;
			dust.velocity.Y += 1f;
		}
	}
}