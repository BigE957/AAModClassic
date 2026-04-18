using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Rajah.Supreme
{
    public class PunisherEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Avenger");
            // Tooltip.SetDefault(@"The Punisher EX");
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
            Item.damage = 500;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Rajah.Supreme.PunisherEX_Proj>();
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true; Item.expertOnly = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noUseGraphic = true;
        }
    }
}