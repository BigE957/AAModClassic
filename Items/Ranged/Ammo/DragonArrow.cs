using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged.Ammo
{
    public class DragonArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Dragon Arrow");
			// Tooltip.SetDefault("Has stronger knockback than most arrows");
		}

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.maxStack = 9999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 4f;
			Item.value = 30;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = ModContent.ProjectileType<DragonArrow>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 1f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
			recipe.AddIngredient(ItemID.WoodenArrow, 50);
			recipe.AddIngredient(null, "IncineriteBar", 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
