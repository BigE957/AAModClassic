using System;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Independence : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Independence");
			/* Tooltip.SetDefault("Shoots 3 firework rockets"
			+"\nCelebration EX"); */
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.FireworksLauncher);
			Item.damage = 375;
         

        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FireworksLauncher);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>());
			recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12, -6);
		}
		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num83 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
			}
			float num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
			if (float.IsNaN(num82) && float.IsNaN(num83) || num82 == 0f && num83 == 0f)
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
			for (int num212 = 0; num212 < 3; num212++)
			{
				float num213 = num82;
				float num214 = num83;
				num213 += Main.rand.Next(-40, 41) * 0.05f;
				num214 += Main.rand.Next(-40, 41) * 0.05f;
				Vector2 vector29 = vector2 + Vector2.Normalize(new Vector2(num213, num214).RotatedBy(-1.57079637f * player.direction)) * 6f;
				Projectile.NewProjectile(source, vector29.X, vector29.Y, num213*1.5f, num214*1.5f, 167 + Main.rand.Next(4), damage, knockback, player.whoAmI, 0f, 1f);
			}
            return false;
        }
    }
}
