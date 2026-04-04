using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Throwing
{
    public class BurningGel : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 18;
			Item.noUseGraphic = true;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.shoot = ModContent.ProjectileType<BurningGelP>();
			Item.shootSpeed = 9f;
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
			// DisplayName.SetDefault("Burning Gel");
			// Tooltip.SetDefault("Ignites target on hit");
		}
	}
}
