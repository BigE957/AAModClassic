using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Akuma
{
    public class RadiantDawn : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Dawn");
            // Tooltip.SetDefault("Hold to fire more arrows");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 450;
            Item.shoot = ModContent.ProjectileType<RadiantDawn>();
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Orange;
            AARarity = 13;
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.DaedalusStormbow);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}