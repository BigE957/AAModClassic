using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class SunHalberd : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Sun Halberd");
            // Tooltip.SetDefault("Strikes foes in an arc, then stabs in the direction of the cursor");			
        }

        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 35;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 15, 0, 0);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 50;
            Item.useTime = 50;
            Item.UseSound = SoundID.Item1;
            Item.damage = 35;
            Item.knockBack = 6;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<SunHalberd_Holdout>();
            Item.shootSpeed = 4;			
        }
    }
}