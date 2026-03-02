using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
	public class MadnessKnife : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.noUseGraphic = true;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.shoot = Mod.Find<ModProjectile>("MadnessKnifeP").Type;
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 0, 0, 25);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Madness Knife");
			// Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(75);
			recipe.AddIngredient(Mod.Find<ModItem>("MadnessFragment").Type);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
