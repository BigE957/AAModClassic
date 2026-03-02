using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
	public class SludgeShot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sludge Shot");
			// Tooltip.SetDefault("Eew! It's mossy!");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 4;
			Item.width = 45;
			Item.height = 45;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = 3;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("SludgeShotP").Type;
			Item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "AbyssiumBar", 10);
			recipe.AddIngredient(null, "MirePod", 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}