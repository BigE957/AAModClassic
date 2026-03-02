using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class StarWrathEX : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.StarWrath);
			Item.autoReuse = true;
			Item.rare = 10;
			Item.width = 48;
			Item.height = 56;
			Item.scale = 1.2f;
			Item.shootSpeed = 10f;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.damage = 220;
			Item.useTime = 12;
			Item.useAnimation = 12;
        }

		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("Cosmic Fury");
		  // Tooltip.SetDefault("Causes stars to rain from the sky\nStars can reach enemies through any obstacles\nStar Wrath EX");
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vector12 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float num75 = Item.shootSpeed;
			for (int num120 = 0; num120 < 3; num120++)
			{
                Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                vector2.Y -= 100 * num120;
				Vector2 vector13 = vector12 - vector2;
				if (vector13.Y < 0f)
				{
					vector13.Y *= -1f;
				}
				if (vector13.Y < 20f)
				{
					vector13.Y = 20f;
				}
				vector13.Normalize();
				vector13 *= num75;
				float num82 = vector13.X;
				float num83 = vector13.Y;
				float speedX5 = num82;
				float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, Mod.Find<ModProjectile>("StarWrathEXP").Type, damage*3/2, knockBack, Main.myPlayer);
			}
			return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.StarWrath);
			recipe.AddIngredient(Mod.Find<ModItem>("EXSoul").Type);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}
