using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class CoinPistol : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Coin Pistol");
			// Tooltip.SetDefault("Coins do half of their normal damage");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.width = 50;
			Item.height = 18;
			Item.shoot = ProjectileID.CopperCoin;
			Item.useAmmo = AmmoID.Coin;
			Item.UseSound = SoundID.Item11;
			Item.damage = 0;
			Item.shootSpeed = 10f;
			Item.noMelee = true;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
			Item.knockBack = 2f;
			Item.DamageType = DamageClass.Ranged;
		}
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, -1);
        }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			damage /= 2;
			return true;
		}
	}
}
