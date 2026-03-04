using System;
using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class Skystrike : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skystrike");
            /* Tooltip.SetDefault("Drops Aerial Bane arrows from the sky"
			+"\nInitial arrows ignore tiles"
			+"\nAerial Bane EX"); */
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DD2BetsyBow);
			Item.damage = 475;
			Item.shootSpeed = 16f;
        }
		
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DD2BetsyBow);
			recipe.AddIngredient(ItemID.DaedalusStormbow);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 0);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = Item.shootSpeed;
			float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num83 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
			}
			float num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
			float num85 = num84;
			if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
			{
				num82 = player.direction;
				num83 = 0f;
				num84 = 11f;
			}
			else
			{
				num84 = 11f / num84;
			}
			num82 *= num84;
			num83 *= num84;
			Vector2 vector6 = new Vector2(num82, num83);
			vector6.X = Main.mouseX + Main.screenPosition.X - vector2.X;
			vector6.Y = Main.mouseY + Main.screenPosition.Y - vector2.Y - 1000f;
			player.itemRotation = (float)Math.Atan2(vector6.Y * player.direction, vector6.X * player.direction);
			NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			int num90 = 5;
			if (Main.rand.Next(2) == 0)
			{
				num90++;
			}
			for (int num91 = 0; num91 < num90; num91++)
			{
				vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X * 10f + player.Center.X) / 11f + Main.rand.Next(-100, 101);
				vector2.Y -= 150 * num91;
				num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
				num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (num83 < 0f)
				{
					num83 *= -1f;
				}
				if (num83 < 20f)
				{
					num83 = 20f;
				}
				num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
				num84 = num75 / num84;
				num82 *= num84;
				num83 *= num84;
				float num92 = num82 + Main.rand.Next(-40, 41) * 0.03f;
				float SpeedY = num83 + Main.rand.Next(-40, 41) * 0.03f;
				num92 *= Main.rand.Next(75, 150) * 0.01f;
				vector2.X += Main.rand.Next(-50, 51);
				int num93 = Projectile.NewProjectile(source, vector2.X, vector2.Y, num92, SpeedY, 710, damage, knockback, player.whoAmI, 0f, 0f);
				Main.projectile[num93].noDropItem = true;
				Main.projectile[num93].tileCollide = false;
			}
            return false;
        }
    }
}