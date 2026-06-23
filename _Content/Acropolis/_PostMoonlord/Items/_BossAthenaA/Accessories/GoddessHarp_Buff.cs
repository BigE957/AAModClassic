using AAModClassic.Base.BaseMod.Base;
using Terraria;
using Terraria.ModLoader;
using static AAModClassic.Utilities.SummonEquipUtils;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories
{
    public class GoddessHarp_Buff : MinionBuffAbstract<GoddessHarp, GoddessHarp_Athena>
	{
        public override int MinionDamage => 100;
        public override float MinionKnockback => 2;
        public override bool ShouldScaleWithClassDamage => true;
        public override bool MinionHasVanitySupport => true;

		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Athena");
			// Description.SetDefault("'I'll help you, but but I'll still thrash you someday.'");
		}
	}
}