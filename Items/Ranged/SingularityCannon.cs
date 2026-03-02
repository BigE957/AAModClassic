using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class SingularityCannon : BaseAAItem
    {

        public override void SetDefaults()
        {
            Item.damage = 55;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 36;
            Item.height = 64;
            Item.useTime = 40;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Singularity>();
            Item.knockBack = 5;
            Item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            Item.rare = 11;
            Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shootSpeed = 22f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Singularity Cannon");
            // Tooltip.SetDefault("");
        }
    }
}
