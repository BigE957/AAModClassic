using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee   //where is located
{
    public class ChaosScythe : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 350;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;           
            Item.width = 56;              
            Item.height = 56;          
            Item.knockBack = 6;
            Item.value = 300000;
            Item.autoReuse = true;   
            Item.useTurn = false;
            Item.expert = true; Item.expertOnly = true;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.shootSpeed = 5;
            Item.shoot = ModContent.ProjectileType<ChaosScythe>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Final Chaos");
            /* Tooltip.SetDefault(@"'I CAN DO ANYTHING'
Legendary Weapon"); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 7f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(new Color(85, 145, 93), new Color(64, 61, 99), Pie);
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = color1;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DeathSickle, 1);
            recipe.AddIngredient(ItemID.IceSickle, 1);
            recipe.AddIngredient(ItemID.Sickle, 1); ;
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();

        }
    }
}
