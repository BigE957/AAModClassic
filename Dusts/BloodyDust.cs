using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class BloodyDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
			dust.velocity.X *= 0.3f;
			dust.scale *= 2f;
			dust.noGravity = true;
		}
	}
}