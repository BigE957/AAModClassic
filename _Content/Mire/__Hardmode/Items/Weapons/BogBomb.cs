using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Weapons        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class BogBomb : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 90; //Projectile Damage
            Item.DamageType = DamageClass.Magic; //It's a magic tome
            Item.mana = 12; //It will use that much
            Item.width = 8;
            Item.height = 8;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //Why would you hit anyone with a book?
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BogBomb_BogOrb>();
            Item.shootSpeed = 8f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bog Bomb");
            // Tooltip.SetDefault("Fires an explosive bomb that inflicts venom upon whatever it strikes");
        }
    }
}