using AAModClassic._Content._Misc.__Hardmode.Items.Ammo;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.MartianMadness.__Hardmode.Items.Weapons
{
    public class AlienRifle : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Alien Rifle");
			// Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = 94;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 48;
			Item.height = 18;
			Item.useAnimation = 9;
			Item.useTime = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
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
