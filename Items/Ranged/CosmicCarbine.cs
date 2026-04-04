using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class CosmicCarbine : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Carbine");
			// Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 54;
			Item.height = 24;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 22f;
			Item.useAmmo = ModContent.ItemType<Energy_Cell>();
			Item.crit = 5;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
		
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
		{
			if (type == ModProjectile.Energy_Cell_Pro) // or ProjectileID.WoodenArrowFriendly
			{
				type = ModProjectile.CosmicLaser; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/
	}
}
