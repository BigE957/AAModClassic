using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.Weapons
{
    public class ThePunisher : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Punisher");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.autoReuse = true;
            Item.knockBack = 6.5f;
            Item.width = 30;
            Item.height = 10;
            Item.damage = 45;
            Item.shoot = ModContent.ProjectileType<ThePunisher_Holdout>();
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;
        }
    }
}