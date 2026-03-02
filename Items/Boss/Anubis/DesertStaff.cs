using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Anubis
{
    public class DesertStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Desert Staff");
			// Tooltip.SetDefault("Shoots enchanted sand bolt which explodes into bouncing balls on hit");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.width = 76;
			Item.height = 76;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DesertBlast").Type;
			Item.shootSpeed = 12f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AmberStaff, 1);
			recipe.AddIngredient(null, "ForsakenFragment", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}