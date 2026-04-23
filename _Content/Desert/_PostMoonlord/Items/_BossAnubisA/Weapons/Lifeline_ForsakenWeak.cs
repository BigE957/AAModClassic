using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons
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
