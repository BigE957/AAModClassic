using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.__Hardmode.Items.Weapons
{
    public class OdinsBlade : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Odin's Iceblade");
            // Tooltip.SetDefault(@"Looks like Greed accidentally snagged this at some point from someone");
        }

        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<OdinsBlade_Proj>();
            Item.shootSpeed = 10f;
            Item.damage = 70;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.width = 20;
            Item.height = 20;
            Item.consumable = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = 100000;
            Item.rare = ItemRarityID.Lime;
        }
    }
}
