using AAModClassic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
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
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.Shuriken;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 5;
            Item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Lime;
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
