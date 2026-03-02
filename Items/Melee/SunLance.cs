using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Melee
{
    public class SunLance : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sun Halberd");
            BaseUtility.AddTooltips(Item, new string[] { "Strikes foes in an arc, then stabs in the direction of the cursor"});			
		}
		
        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35;
            Item.maxStack = 1;
            Item.rare = 5;
            Item.value = BaseUtility.CalcValue(0, 15, 0, 0);

            Item.useStyle = 5;
            Item.useAnimation = 50;
            Item.useTime = 50;
            Item.UseSound = SoundID.Item1;
            Item.damage = 35;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = Mod.ProjType("SunLance");
            Item.shootSpeed = 4;			
        }
    }
}