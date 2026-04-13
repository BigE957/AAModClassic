using AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Items.Boss.Broodmother;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
	public class BlazePike : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blaze Pike");
			// Tooltip.SetDefault("Very hot to touch");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 3;
			Item.width = 56;
			Item.height = 56;
			Item.useTime = 27;
			Item.useAnimation = 27;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.DD2FlameBurstTowerT1Shot;
			Item.shootSpeed = 6f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 10);
			recipe.AddIngredient(ModContent.ItemType<BroodScale>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}