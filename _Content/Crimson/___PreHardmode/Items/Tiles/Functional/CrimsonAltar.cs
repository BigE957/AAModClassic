using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Evil.___PreHardmode.Items.Tiles.Functional;

namespace AAModClassic._Content.Crimson.___PreHardmode.Items.Tiles.Functional
{
    public class CrimsonAltar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Altar");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EvilAltarSafe_Tile>();
            Item.placeStyle = 1;
            Item.width = 28;
            Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.accessory = false;
            Item.maxStack = Item.CommonMaxStack;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 15);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}

