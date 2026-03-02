using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Vortex : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Vortex");
            // Tooltip.SetDefault(@"Spins fast enough to drag all enemies into its gravitational pull");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Terrarian);
            Item.damage = 475;                            
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = 2;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = 5;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shoot = Mod.Find<ModProjectile>("Vortex").Type;
            Item.rare = 9; AARarity = 13;
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

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.Terrarian);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }

    }
}
