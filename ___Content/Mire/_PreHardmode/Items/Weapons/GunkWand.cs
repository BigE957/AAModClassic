using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons
{
    public class GunkWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gunk Wand");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.width = 36;
            Item.height = 38;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Gunk>();
            Item.shootSpeed = 4f;
        }
    }
}