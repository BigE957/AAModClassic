using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons
{
    public class Lifeline_Forsaken : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forsaken");
			// Description.SetDefault("You are forsaken");
			Main.debuff[Type] = true;
		}
    }
}
