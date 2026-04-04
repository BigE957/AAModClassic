using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged.Ammo
{
    public class DaybreakBullet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Daybreak Bullet");
		}

		public override void SetDefaults()
		{
			Item.shootSpeed = 5f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ammo.DaybreakBullet>();
			Item.damage = 20;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.ammo = AmmoID.Bullet;
			Item.knockBack = 2f;
			Item.value = 15;
            Item.rare = ItemRarityID.LightRed;
            Item.DamageType = DamageClass.Ranged;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(500);
            recipe.AddIngredient(ItemID.ExplodingBullet, 500);
			recipe.AddIngredient(ModContent.ItemType<DaybreakIncinerite>(), 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
