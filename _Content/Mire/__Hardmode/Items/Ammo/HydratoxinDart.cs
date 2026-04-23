using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Ammo
{
    public class HydratoxinDart : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Hydratoxin Dart");
		}

		public override void SetDefaults()
		{
			Item.shoot = ModContent.ProjectileType<HydratoxinDart_Proj>();
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.ammo = AmmoID.Dart;
			Item.damage = 7;
			Item.knockBack = 3f;
			Item.shootSpeed = 4f;
			Item.DamageType = DamageClass.Ranged;
			Item.rare = ItemRarityID.LightRed;
			Item.consumable = true;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ModContent.ItemType<Bogtoxin>(), 1);
			recipe.Register();
		}
	}
}
