using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class CoralBow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Coral Bow");
			// Tooltip.SetDefault("Somehow better than a metallic bow");
        }

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 28;
			Item.height = 60;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = false;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 8f;
			Item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.IronBow);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.LeadBow);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
