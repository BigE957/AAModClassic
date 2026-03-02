using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
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
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 1000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Gunk").Type;
            Item.shootSpeed = 4f;
        }
    }
}