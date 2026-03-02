using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class Fireblast : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 42;                        
			Item.DamageType = DamageClass.Magic;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 3, 0, 0);
			Item.rare = 4;
			Item.mana = 10;
			Item.UseSound = SoundID.Item21;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("FireblastP").Type;
			Item.shootSpeed = 8f;
		}   

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fireblast");
			// Tooltip.SetDefault("Shoots an explosive bolt of dragonflame");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SpellTome);
			recipe.AddIngredient(Mod.Find<ModItem>("DragonFire").Type, 20);
			recipe.AddIngredient(null, "SoulOfSmite", 15);
			recipe.AddTile(TileID.Bookcases);
			recipe.Register();
		}
	}
}
