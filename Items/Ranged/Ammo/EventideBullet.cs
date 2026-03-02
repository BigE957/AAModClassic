using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class EventideBullet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Eventide Bullet");
		}

		public override void SetDefaults()
		{
			Item.shootSpeed = 5f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ammo.EventideBullet>();
			Item.damage = 25;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.ammo = AmmoID.Bullet;
			Item.knockBack = 2f;
			Item.value = 15;
            Item.rare = 4;
            Item.DamageType = DamageClass.Ranged;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(500);
            recipe.AddIngredient(ItemID.ExplodingBullet, 500);
			recipe.AddIngredient(Mod.Find<ModItem>("EventideAbyssium").Type, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
