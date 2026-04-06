using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Sagittarius
{
    public class NeutronStaff : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.mana = 2;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.SagStar>();
            Item.shootSpeed = 7f;
        }   

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Neutron Rod");
            // Tooltip.SetDefault("Fires spinning stars that bounce on walls");
            Item.staff[Item.type] = true;
        }
    }
}
