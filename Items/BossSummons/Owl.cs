using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class Owl : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Owl Statue");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons Athena
Can only be used in the Acropolis at the Owl Altar
'It stares into your soul.'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.maxStack = 20;
            Item.rare = 6;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SeraphFeather", 15);
            recipe.AddIngredient(ItemID.Sapphire, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}