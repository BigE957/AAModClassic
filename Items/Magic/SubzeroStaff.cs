using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class SubzeroStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Storm Staff");
            // Tooltip.SetDefault(@"Blizzard Staff EX");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.mana = 11;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 220;
            Item.useAnimation = 3;
            Item.useTime = 3;
            Item.width = 62;
            Item.height = 62;
            Item.shoot = ModContent.ProjectileType<Projectiles.SubzeroSnowflake>();
            Item.shootSpeed = 17f;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.DamageType = DamageClass.Magic;
            Item.rare = ItemRarityID.Purple;
            Item.noMelee = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int num111 = 0; num111 < 2; num111++)
            {
                Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector2.Y -= 100 * num111;
                float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
                float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num82 < 0f)
                {
                    num82 *= -1f;
                }
                if (num82 < 20f)
                {
                    num82 = 20f;
                }
                float num83 = (float)Math.Sqrt(num81 * num81 + num82 * num82);
                num83 = Item.shootSpeed / num83;
                num81 *= num83;
                num82 *= num83;
                float speedX4 = num81 + Main.rand.Next(-40, 41) * 0.02f;
                float speedY5 = num82 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockback, Main.myPlayer, 0f, Main.rand.Next(5));
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BlizzardStaff, 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}