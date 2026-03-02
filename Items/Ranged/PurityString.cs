using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class PurityString : BaseAAItem
    {

        public override void SetDefaults()
        {

            Item.damage = 50;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 34;
            Item.height = 60;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 5;
            Item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            Item.rare = 7;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 22f;

        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Crystal Bow");
          // Tooltip.SetDefault("");
        }
    }
}
