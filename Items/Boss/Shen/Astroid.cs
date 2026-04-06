using AAModClassic;
using AAModClassic.Globals;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Shen
{
    public class Astroid : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Asteroid");
            /* Tooltip.SetDefault(@"Crashes into enemies with the force of an astroid crashing into earth
Inflicts Discordian Inferno"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.rare = ItemRarityID.Cyan;

            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 7.5F;
            Item.damage = 300;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Shen.Astroid>();
            Item.shootSpeed = 32F;
            Item.UseSound = SoundID.Item20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.autoReuse = true;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Daycrusher", 1);
            recipe.AddIngredient(null, "Flairdra", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}