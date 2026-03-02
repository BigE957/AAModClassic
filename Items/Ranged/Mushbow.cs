using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class  Mushbow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Bow");
        }

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 40;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 0, 10, 50) ;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = false;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 7f;
			Item.useAmmo = AmmoID.Arrow;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(null, "MushiumBar", 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
