using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Lifeline_ForsakenWeak : ModBuff
	{
        public override string Texture => ModContent.GetInstance<Lifeline_Forsaken>().Texture;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Forsaken Weakening");
			// Description.SetDefault("Weakens enemy contact damage");
			Main.debuff[Type] = true;
		}
    }
}
