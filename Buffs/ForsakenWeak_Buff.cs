using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ForsakenWeak_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forsaken Weakening");
			// Description.SetDefault("Weakens enemy contact damage");
			Main.debuff[Type] = true;
		}
    }
}
