using AAModClassic._Content._Misc.__Hardmode.Items.Ammo;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class LaserRifle : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Laser Carbine");
			// Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 46;
			Item.height = 22;
			Item.useAnimation = 14;
			Item.useTime = 14;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 4, 72, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 22f;
			Item.useAmmo = ModContent.ItemType<EnergyCell>();			
			Item.crit = 5;
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
	}
}
