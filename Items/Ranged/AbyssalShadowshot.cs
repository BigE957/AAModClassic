using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Projectiles;
using AAModClassic;

namespace AAModClassic.Items.Ranged
{
    public class AbyssalShadowshot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Shadowshot");
            // Tooltip.SetDefault("Fires a homing shadow blast as well as a a blast of 6 bullets");
        }

        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item14;
            Item.shootSpeed = 12f;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 6; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Shadowshot>(), (int)(damage * 1.0f), knockBack, Main.myPlayer);
            return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssalPentashot", 1);
            recipe.AddIngredient(null, "HeroShards", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
