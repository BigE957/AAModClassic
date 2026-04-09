using AAModClassic.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class TerraFocus : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Focus");
            // Tooltip.SetDefault(@"Fires shots of terra magic at your foes");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 12;
            Item.useTime = 4;
            Item.reuseDelay = Item.useAnimation + 6;
            Item.shootSpeed = 14f;
            Item.knockBack = 6f;
            Item.width = 16;
            Item.height = 16;
            Item.damage = 50;
            Item.UseSound = SoundID.Item9;
            Item.crit = 20;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<MagicBlastF>();
            Item.mana = 14;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 300000;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
        }
    }
}