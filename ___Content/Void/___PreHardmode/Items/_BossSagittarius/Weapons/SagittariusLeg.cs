using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Sagittarius
{
    public class SagittariusLeg : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sagittarius' Leg");
            // Tooltip.SetDefault("It's a piece of metal. You beat things with it. Pretty basic concept.");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 38;
            Item.useTime = 38;
            Item.knockBack = 8f;
            Item.width = 50;
            Item.height = 92;
            Item.damage = 42;
            Item.scale = 1.05f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 150000;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
        }
    }
}