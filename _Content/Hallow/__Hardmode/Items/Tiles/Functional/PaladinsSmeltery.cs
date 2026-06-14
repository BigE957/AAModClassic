using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional
{
    public class PaladinsSmeltery : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
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
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<PaladinsSmeltery_Tile>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<HallowedAnvil>(), 1);
                recipe.AddIngredient(ModContent.ItemType<HallowedForge>(), 1);
                recipe.Register();
            }
        }
    }
}
