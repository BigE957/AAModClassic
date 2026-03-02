using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class SwarmDust : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = DustID.PureSpray;
		}
	}
}