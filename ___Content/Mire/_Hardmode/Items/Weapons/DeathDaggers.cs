using System;
using AAModClassic.___Content.Mire._Hardmode.Items.Materials;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Weapons
{
    public class DeathDaggers : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Death Daggers");
            // Tooltip.SetDefault("Throw life stealing daggers that inflict Hydratoxin");
        }

        public override void SetDefaults()
		{
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<DeathDaggers_DeathDagger>();
            Item.damage = 29;
            Item.width = 54;
            Item.height = 54;
            Item.scale *= 0.5f;
            Item.UseSound = SoundID.Item39;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.knockBack = 1f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = ItemRarityID.LightRed;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<HydraToxin>(), 10);
		    recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
    }
}
