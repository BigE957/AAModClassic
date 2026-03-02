namespace AAMod.Projectiles.Shen
{
    public class ChaosSlayerSwordRed : ChaosSlayerSword
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