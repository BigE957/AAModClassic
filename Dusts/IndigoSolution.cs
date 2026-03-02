using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class IndigoSolution : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = DustID.PureSpray;
		}
	}
}