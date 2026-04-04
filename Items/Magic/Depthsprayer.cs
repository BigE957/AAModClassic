using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
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
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 15;
			Item.useTime = 5;
			Item.knockBack = 4f;
			Item.width = 38;
			Item.height = 10;
			Item.damage = 35;
			Item.shoot = ModContent.ProjectileType<Depthsprayer>();
			Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item13;
			Item.rare = ItemRarityID.LightPurple;
			Item.value = 250000;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DeepAbyssium>());
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}