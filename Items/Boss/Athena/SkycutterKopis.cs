using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class SkycutterKopis : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skycutter Kopis");
			// Tooltip.SetDefault("");
        }
        public override void SetDefaults()
		{
			Item.damage = 70;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 40;
			Item.height = 50;
            Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = 1;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = 8;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Skyblade").Type;
            Item.shootSpeed = 10;
		}

		public override void AddRecipes()
		{
			Recipe recipe;
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SilverBroadsword, 1);
			recipe.AddIngredient(null, "GoddessFeather", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TungstenBroadsword, 1);
			recipe.AddIngredient(null, "GoddessFeather", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
