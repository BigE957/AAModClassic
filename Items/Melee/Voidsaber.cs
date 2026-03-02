using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Voidsaber : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;
			Item.useAnimation = 25;
			Item.useTime = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.rare = ItemRarityID.Blue;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.noMelee = true;
			Item.damage = 9;
			Item.knockBack = 4f;
			Item.autoReuse = false;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.shoot = ModContent.ProjectileType<Projectiles.Voidslash>();
			Item.shootSpeed = 15f;
			Item.value = 5400;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Voidsaber");
			// Tooltip.SetDefault("");
		}
	}
}
