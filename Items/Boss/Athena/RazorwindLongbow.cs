using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class RazorwindLongbow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razorwind Longbow");
            // Tooltip.SetDefault("Replaces wooden arrows with wind arrows with high knockback");
        }

        public override void SetDefaults()
        {
            Item.damage = 50; 
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 62;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false;
            Item.shootSpeed = 14f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.Athena.WindArrow>(), damage, knockBack * 3, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SilverBow, 1);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TungstenBow, 1);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}