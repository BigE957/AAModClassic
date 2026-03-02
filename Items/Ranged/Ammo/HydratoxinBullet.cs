using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged.Ammo
{
    public class HydratoxinBullet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Hydratoxin Bullet");
		}

		public override void SetDefaults()
		{
			Item.shootSpeed = 5f;
			Item.shoot = Mod.Find<ModProjectile>("HydratoxinBullet").Type;
			Item.damage = 12;
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
			recipe.AddIngredient(Mod.Find<ModItem>("HydraToxin").Type, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
