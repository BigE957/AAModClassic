using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Ammo
{
    public class HydratoxinArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Hydratoxin Arrow");
			// Tooltip.SetDefault("Inflicts Hydratoxin debuff on hit");
		}

		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.maxStack = 9999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 4f;
			Item.value = 100;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ModContent.ProjectileType<HydratoxinArrow>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 1f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(ItemID.WoodenArrow, 150);
			recipe.AddIngredient(ModContent.ItemType<HydraToxin>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
