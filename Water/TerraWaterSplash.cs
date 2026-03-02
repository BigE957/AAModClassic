using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class TerraWaterSplash : ModDust
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