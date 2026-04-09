using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class Firebuster : BaseAAItem
    {

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.width = 54;
            Item.height = 24;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11;
            Item.damage = 36;
            Item.shootSpeed = 9f;
            Item.noMelee = true;
            Item.value = 100000;
            Item.knockBack = 10f;
            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 10;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
    }
}
