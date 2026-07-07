using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Weapons
{
    public class Voidsaber : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
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
			Item.shoot = ModContent.ProjectileType<Voidsaber_Holdout>();
			Item.shootSpeed = 15f;
			Item.value = 5400;

            if (ModLoader.TryGetMod("Redemption", out var redemption))
                redemption.Call("setSlashBonus", Item);
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Voidsaber");
			// Tooltip.SetDefault("");
		}
	}
}
