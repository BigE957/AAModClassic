using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
    public class SoapBlaster : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soap Blaster");
            // Tooltip.SetDefault("Rapidly shoots destructive bubbles");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BubbleGun);
			Item.useTime = 3;
			Item.useAnimation = 3;
            Item.damage = 125;
            Item.rare = 11;
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = Item.shootSpeed;
			float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
			float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
			float num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
			if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
			{
				num82 = player.direction;
				num83 = 0f;
				num84 = num75;
			}
			else
			{
				num84 = num75 / num84;
			}
			num82 *= num84;
			num83 *= num84;
			for (int num179 = 0; num179 < 3; num179++)
			{
				float num180 = num82;
				float num181 = num83;
				num180 += Main.rand.Next(-20, 20) * 0.1f;
				num181 += Main.rand.Next(-20, 20) * 0.1f;
				Projectile.NewProjectile(vector2.X, vector2.Y, num180*2, num181*2, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}



        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BubbleGun);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}