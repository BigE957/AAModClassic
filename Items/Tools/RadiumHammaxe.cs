using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class RadiumHammaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 70;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 44;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.axe = 50;
            Item.hammer = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Cyan;
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

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Hammaxe");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 12);
            recipe.AddTile(null, "QuantumFusionAccelerator");   
            recipe.Register();
        }
    }
}
