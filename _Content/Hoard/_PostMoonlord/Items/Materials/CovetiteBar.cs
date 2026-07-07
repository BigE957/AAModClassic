using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items.Materials
{
    public class CovetiteBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetDefaults()
        {

            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.value = 16000;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<CovetiteBar_Tile>();
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Covetite Bar");
            // Tooltip.SetDefault("It's somehow shiny but not at the same time. How did greed fall for this?");
        }

		public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CovetiteOre>(), 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
