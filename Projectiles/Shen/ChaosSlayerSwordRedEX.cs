namespace AAModClassic.Projectiles.Shen
{
    public class ChaosSlayerSwordRedEX : ChaosSlayerSword
    {
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