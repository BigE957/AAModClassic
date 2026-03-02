using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Forsaken : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forsaken");
			// Description.SetDefault("You are forsaken");
			Main.debuff[Type] = true;
		}
    }
}
