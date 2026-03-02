using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged.Ammo
{
    public class DragonfireDart : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Dragonfire Dart");
		}

		public override void SetDefaults()
		{
			Item.shoot = Mod.Find<ModProjectile>("DragonfireDart").Type;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.ammo = AmmoID.Dart;
			Item.damage = 11;
			Item.knockBack = 3f;
			Item.shootSpeed = 4f;
			Item.DamageType = DamageClass.Ranged;
			Item.rare = ItemRarityID.LightRed;
			Item.consumable = true;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(Mod.Find<ModItem>("DragonFire").Type, 1);
			recipe.Register();
		}
	}
}
