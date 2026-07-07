using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Ikari_BladeOfAbyssalWrath : ChaosSlayer_BladeOfChaos, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade of Abyssal Wrath");
		}

        public override void SetDefaults()
        {
           base.SetDefaults();
		   swordType = 1;
		   offsetLeft = false;
		}	
    }
}