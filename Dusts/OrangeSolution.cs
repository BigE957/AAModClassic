using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class OrangeSolution : ModDust
	{
		public override void SetStaticDefaults()
		{
			UpdateType = DustID.PureSpray;
		}
	}
}