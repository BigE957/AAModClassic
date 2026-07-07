using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories;
using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ModLoader;
using static AAModClassic.Utilities.SummonEquipUtils;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories
{
    public class SeraphHarp_Buff : MinionBuffAbstract<SeraphHarp, SeraphHarp_Seraph>
    {
        public override int MinionDamage => 60;
        public override float MinionKnockback => 2;
        public override bool ShouldScaleWithClassDamage => true;
        public override bool MinionHasVanitySupport => true;

        public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Seraph");
			// Description.SetDefault("Small but feisty");
		}
	}
}