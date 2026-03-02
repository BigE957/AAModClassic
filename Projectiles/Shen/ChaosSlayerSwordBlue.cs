namespace AAModClassic.Projectiles.Shen
{
    public class ChaosSlayerSwordBlue : ChaosSlayerSword
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