using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.ReforgeSouls
{
    public class Unreal : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unreal Soul");
            /* Tooltip.SetDefault(
@"Gives a weapon the ''Unreal'' prefix if it is possible
Right-click weapon with the soul to set prefix
Consumes in process"); */
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Terraria.ID.ItemID.FragmentVortex, 15);
            recipe.AddTile(Terraria.ID.TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}