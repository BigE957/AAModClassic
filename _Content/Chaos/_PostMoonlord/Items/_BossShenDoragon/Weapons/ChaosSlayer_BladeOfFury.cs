namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class ChaosSlayer_BladeOfFury : ChaosSlayer_BladeOfChaos
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade of Fury");
		}

        public override void SetDefaults()
        {
           base.SetDefaults();
		   swordType = 2;
		   offsetLeft = true;
		}	
    }
}