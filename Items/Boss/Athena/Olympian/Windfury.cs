using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class Windfury : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Windfury");
            // Tooltip.SetDefault("Replaces wooden arrows with gale arrows with high knockback and infinite piercing");
        }

        public override void SetDefaults()
        {
            Item.damage = 140; 
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 26;
            Item.height = 50;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 0;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
            Item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.Athena.GaleArrow>(), damage, knockBack * 5, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "RazorwindLongbow", 1);
            recipe.AddIngredient(null, "StormSphere", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}