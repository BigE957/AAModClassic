using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Firebuster : BaseAAItem
    {

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.autoReuse = true;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.width = 54;
            Item.height = 24;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11;
            Item.damage = 36;
            Item.shootSpeed = 9f;
            Item.noMelee = true;
            Item.value = 100000;
            Item.knockBack = 10f;
            Item.rare = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 10;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 0);
        }
    }
}
