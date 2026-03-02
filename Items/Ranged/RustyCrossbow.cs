using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class RustyCrossbow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rusty Crossbow");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.width = 32;
            Item.height = 20;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;
            Item.damage = 25;
            Item.shootSpeed = 10f;
            Item.knockBack = 2f;
            Item.rare = 3;
            Item.noMelee = true;
            Item.value = 10000;
            Item.DamageType = DamageClass.Ranged;
        }
    }
}