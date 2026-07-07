using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Ikari_BladeOfBlazingFury : Ikari_BladeOfUnyieldingChaos, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade of Blazing Fury");
		}

        public override void SetDefaults()
        {
           base.SetDefaults();
		   swordType = 2;
		   offsetLeft = true;
		}	
    }
}