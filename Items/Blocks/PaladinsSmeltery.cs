using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class PaladinsSmeltery : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Paladin's Smeltery Forge");
            /* Tooltip.SetDefault(
@"This thing can make a lot of stuff
Functions as most hardmode crafting stations + A workbench and heavy workbench"); */
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<PaladinsSmeltery>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "HallowedAnvil", 1);
                recipe.AddIngredient(null, "HallowedForge", 1);
                recipe.Register();
            }
        }
    }
}
