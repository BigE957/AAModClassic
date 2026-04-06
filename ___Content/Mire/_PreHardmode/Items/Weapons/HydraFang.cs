using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons
{
	public class HydraFang : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.noUseGraphic = true;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.width = 28;
			Item.height = 34;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.shoot = ModContent.ProjectileType<HydraFangP>();
			Item.shootSpeed = 16f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item19;
			Item.autoReuse = true;
			Item.crit = 10;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydra Fang");
			// Tooltip.SetDefault("Pierces up to 3 enemies");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(99);
			recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>());
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
