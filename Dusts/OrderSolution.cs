using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Dusts
{
    public class OrderSolution : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = DustID.PureSpray;
		}
	}
}