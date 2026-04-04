using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class MagicFlower : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Magic Flower");
            // Tooltip.SetDefault("Pretty");
		}

		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ManaShot>();
			Item.shootSpeed = 10f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(ItemID.NaturesGift, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}