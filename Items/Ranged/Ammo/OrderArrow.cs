using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged.Ammo
{
    public class OrderArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Order Arrow");
			// Tooltip.SetDefault("Ignores up to 30 of enemy defense");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.maxStack = 999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 4f;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = Mod.Find<ModProjectile>("OrderArrow").Type;   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 1f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(ItemID.WoodenArrow, 250);
			recipe.AddIngredient(Mod.Find<ModItem>("OrderBar").Type, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
