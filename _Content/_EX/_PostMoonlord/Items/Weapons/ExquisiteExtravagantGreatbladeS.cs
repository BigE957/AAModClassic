using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class ExquisiteExtravagantGreatbladeS : ExquisiteExtravagantGreatblade
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Exquisite, Extravagant Greatblade");
            // Tooltip.SetDefault(@"Extravagant Longsword EX");
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ExquisiteExtravagantGreatblade>());
                recipe.AddIngredient(ModContent.ItemType<ShinyCharm>());
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ExtravagantLongswordS>());
                recipe.AddIngredient(ModContent.ItemType<EXSoul>());
                recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
                recipe.Register();
            }
        }
    }
}
