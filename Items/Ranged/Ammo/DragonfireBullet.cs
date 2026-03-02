using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class DragonfireBullet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Dragonfire Bullet");
		}

		public override void SetDefaults()
		{
			Item.shootSpeed = 5f;
			Item.shoot = Mod.Find<ModProjectile>("DragonfireBullet").Type;
			Item.damage = 13;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.ammo = AmmoID.Bullet;
			Item.knockBack = 2f;
			Item.value = 15;
            Item.rare = ItemRarityID.LightRed;
            Item.DamageType = DamageClass.Ranged;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ItemID.MusketBall, 100);
			recipe.AddIngredient(Mod.Find<ModItem>("DragonFire").Type, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
