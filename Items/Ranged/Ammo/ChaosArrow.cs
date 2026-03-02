using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class ChaosArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Arrow");
            // Tooltip.SetDefault("Inflicts randomly selected debuff on hit");
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
			Item.value = 30;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = Mod.Find<ModProjectile>("ChaosArrow").Type;   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 1f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

       public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(ItemID.WoodenArrow, 150);
            recipe.AddIngredient(Mod, "RadiantIncinerite", 1);
			recipe.AddIngredient(Mod, "DeepAbyssium", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
