using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Tiles.Functional
{
    public class HellstoneAnvil : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hellstone Anvil");
            // Tooltip.SetDefault("Is this thing supposed to be on fire?");
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<HellstoneAnvil_Tile>();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
        }

        public override void AddRecipes()
        { 
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.HellstoneBar, 20);
                recipe.AddIngredient(ItemID.IronAnvil, 1);
                recipe.AddIngredient(ItemID.ObsidianWorkBench, 1);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.HellstoneBar, 20);
                recipe.AddIngredient(ItemID.LeadAnvil, 1);
                recipe.AddIngredient(ItemID.ObsidianWorkBench, 1);
                recipe.Register();
            }
        }
    }
}
