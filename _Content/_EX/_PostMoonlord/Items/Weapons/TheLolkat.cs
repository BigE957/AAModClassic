using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons   //where is located
{
    public class TheLolkat : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Lolkat");
            /* Tooltip.SetDefault(@"WHAT DOES IT MEAN?!?!?!111!!11
Meowmere EX"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 550;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 70;
            Item.useTime = 10;
            Item.useAnimation = 10;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item57;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.expert = true; Item.expertOnly = true;
			Item.shoot = ProjectileID.Meowmere;
			Item.shootSpeed = 12f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
            glowmaskDrawType = GLOWMASKTYPE_SWORD;
            glowmaskDrawColor = Color.White;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 30f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 2; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Meowmere);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}
