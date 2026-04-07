using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons
{
	public class SludgeShot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sludge Shot");
			// Tooltip.SetDefault("Eew! It's mossy!");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 4;
			Item.width = 45;
			Item.height = 45;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<SludgeShot_Proj>();
			Item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
			recipe.AddIngredient(ModContent.ItemType<MirePod>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}