using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class Depthsprayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Depthsprayer");
			// Tooltip.SetDefault("Covers enemies in Hydratoxin");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.mana = 9;
			Item.autoReuse = true;
			Item.useStyle = 5;
			Item.useAnimation = 15;
			Item.useTime = 5;
			Item.knockBack = 4f;
			Item.width = 38;
			Item.height = 10;
			Item.damage = 35;
			Item.shoot = Mod.Find<ModProjectile>("Depthsprayer").Type;
			Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item13;
			Item.rare = 6;
			Item.value = 250000;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("DeepAbyssium").Type);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}