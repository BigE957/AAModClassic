using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class Suncaller : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Suncaller");
            /* Tooltip.SetDefault(@"Brings forth the morning sun.
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = 8;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime || Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */)
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Main.dayTime = true;
            Main.time = 0;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(null, "SoulOfSmite", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
