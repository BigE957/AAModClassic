using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra._PostMoonLord.Items.Tiles.Functional
{
    public class TerraCore : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Core of Terraria");
            /* Tooltip.SetDefault(@"Combines most crafting stations into one
Used to create ancient crafting stations"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 1000000;
            Item.createTile = ModContent.TileType<TerraCore_Tile>();
        }  

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("AAModClassic:AstralStations", 1);
            recipe.AddIngredient(ModContent.ItemType<TruePaladinsSmeltery>(), 1);
            recipe.Register();
        }
    }
}
