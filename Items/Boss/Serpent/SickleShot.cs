using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class SickleShot : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sickleshot");
            // Tooltip.SetDefault("Shoots 2 ice arrows with high velocity ");
        }

        public override void SetDefaults()
        {

            Item.damage = 16;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 62;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Serpent.IceArrow>();
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 30f;

        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 15f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 2; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockBack, Main.myPlayer);
		    }
		    return false;
		}
    }
}
