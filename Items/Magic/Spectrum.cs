using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class Spectrum : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spectrum");
            /* Tooltip.SetDefault(@"Focuses a devastating beam of light
Last Prism EX"); */
           
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 150;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 14;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.reuseDelay = 5;
	        Item.useStyle = 5;
	        Item.UseSound = SoundID.Item13;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 0f;
	        Item.value = 1000000;
	        Item.shoot = Mod.Find<ModProjectile>("Spectrum").Type;
	        Item.shootSpeed = 30f;
			Item.rare = 9;
	    }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LastPrism);
			recipe.AddIngredient(Mod.Find<ModItem>("EXSoul").Type);
			recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}