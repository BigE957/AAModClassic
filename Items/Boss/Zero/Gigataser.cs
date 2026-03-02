using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Gigataser : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gigataser");
            // Tooltip.SetDefault(@"Fires void lightning");
        }

        public override void SetDefaults()
        {
            Item.noUseGraphic = false;
            Item.damage = 100;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 74;
            Item.height = 24;
            Item.useTime = 45;
            Item.useAnimation = 45; 
            Item.useStyle = 5;
            Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Shock");
            Item.shoot = Mod.Find<ModProjectile>("ZeroTaze").Type;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = 9;
            Item.shootSpeed = 12f;
            Item.crit += 5;
            Item.rare = 9;
            AARarity = 13;
            Item.autoReuse = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int num842 = 0; num842 < 3; num842++)
            {
                Vector2 vector82 = new Vector2(speedX, speedY);
                float ai = Main.rand.Next(100);
                Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.6)) * 14f;
                Projectile.NewProjectile(position.X, position.Y, vector83.X * 2, vector83.Y * 2, ModContent.ProjectileType<Projectiles.Zero.ZeroTaze>(), damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}