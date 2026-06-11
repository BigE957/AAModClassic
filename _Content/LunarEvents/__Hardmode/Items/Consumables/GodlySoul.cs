using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.LunarEvents.__Hardmode.Items.Consumables
{
    public class GodlySoul : BaseAAItem
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
            // DisplayName.SetDefault("Godly Soul");
            /* Tooltip.SetDefault(
@"Gives a weapon the ''Godly'' prefix if it is possible
Right-click weapon with the soul to set prefix
Consumes in process"); */
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}