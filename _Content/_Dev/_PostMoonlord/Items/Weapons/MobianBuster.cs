using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class MobianBuster : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mobian Buster");
            /* Tooltip.SetDefault("A standard issue Mobian blaster.\n" +
                "Hold the use button to charge, and then release a powerful Charged Shot!\n" +
                "\"Remember, the charged shot fires when you RELEASE the trigger, not the other way around.\" \n" +
                "- Tails\n"); */
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 100;
            Item.shoot = ModContent.ProjectileType<Projectiles.MobianBuster>();
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
			Item.rare = ItemRarityID.Red;
			Item.shootSpeed = 12f;
			Item.noUseGraphic = true;
        }
    }
}
