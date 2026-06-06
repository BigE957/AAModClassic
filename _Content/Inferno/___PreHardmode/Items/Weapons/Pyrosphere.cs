using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
    public class Pyrosphere : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Pyrosphere");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }		
		
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 90, 50);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.UseSound = SoundID.Item1;
            Item.damage = 8;
            Item.knockBack = 7;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.shoot = ModContent.ProjectileType<Pyrosphere_Holdout>();
            Item.shootSpeed = 10;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;		
        }
	}
}