using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    public class InfinityGauntletEffect_InfinityBurnout : ModBuff
	{
        public override void SetStaticDefaults()
        {
			/*DisplayName.SetDefault("Infinity Burnout");
            Description.SetDefault("They didn't go for the head.");*/
            Main.debuff[Type] = true;
        }

    }
}