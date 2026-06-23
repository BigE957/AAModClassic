using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons
{
    public class TheAvenger : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Avenger");
            // Tooltip.SetDefault(@"The Punisher EX");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.autoReuse = true;
            Item.knockBack = 7f;
            Item.width = 30;
            Item.height = 10;
            Item.damage = 250;
            Item.shoot = ModContent.ProjectileType<TheAvenger_Holdout>();
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noUseGraphic = true;
            Item.channel = true;
        }
    }
}