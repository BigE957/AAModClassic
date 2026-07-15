using AAModClassic._Content._Tinker._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra._PostMoonLord.Items.Tiles.Functional
{
    public class TerraPrismStation : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinity Core");
            /* Tooltip.SetDefault(@"The 'craft-all'.
Combiles all vanilla and Ancients Awakened crafting stations together"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 100000;
            Item.createTile = ModContent.TileType<TerraPrismStation_Tile>();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FurnitureDynamo>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TerraCore>(), 1);
            recipe.AddRecipeGroup("AAModClassic:AncientCraftingStation");
            recipe.Register();
        }
    }
}
