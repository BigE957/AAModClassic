using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Toad
{
    public class ToadTongue : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Toad Tongue");
            // Tooltip.SetDefault(@"Pulls enemies towards you when it retracts");
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 0, 70, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 8f;
            Item.damage = 30;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Toad.ToadTongue_Proj>();
            Item.shootSpeed = 14;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }
    }
}