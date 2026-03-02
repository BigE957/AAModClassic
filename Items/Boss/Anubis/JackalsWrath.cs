using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis
{
    public class JackalsWrath : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jackal's Wrath");
            // Tooltip.SetDefault("Shoots out a wall-piercing returning phantom blade on swing");
        }

		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.knockBack = 5f;
			Item.width = 24;
			Item.height = 28;
			Item.damage = 30;
			Item.UseSound = SoundID.Item71;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("PhantomBlade").Type;
			Item.shootSpeed = 14f;
			Item.value = 10000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GoldAxe, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register(); 
			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PlatinumAxe, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
