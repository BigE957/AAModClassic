using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Ranged
{
    public class AbyssalPentashot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Pentashot");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item11;
            Item.shootSpeed = 12f;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 5; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
		    }
		    return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "HydraTrishot", 1);
            recipe.AddIngredient(null, "OceanWhaler", 1);
            recipe.AddIngredient(null, "DoomiteAssaultBlaster", 1);
            recipe.AddIngredient(ItemID.SnowballCannon, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
