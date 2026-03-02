using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class HK_MP5 : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("HK MP5");
			// Tooltip.SetDefault("Turns bullets into explosive bullets!");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 24;
			Item.useAnimation = 8;
			Item.useTime = 8;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 3, 0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item40;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            type = ProjectileID.ExplosiveBullet;
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(04));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 2);
		}
	}
}
