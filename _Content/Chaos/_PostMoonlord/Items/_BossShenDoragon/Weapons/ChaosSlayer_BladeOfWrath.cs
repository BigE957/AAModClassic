namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons
{
    public class ChaosSlayer_BladeOfWrath : ChaosSlayer_BladeOfChaos
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade of Wrath");
		}

        public override void SetDefaults()
        {
           base.SetDefaults();
		   swordType = 1;
		   offsetLeft = false;
		}	
    }
}