using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Materials
{
    public class AbyssiumBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssium Bar");
            // Tooltip.SetDefault("Solid Darkness");

            ItemTrader.ChlorophyteExtractinator.AddOption_Interchangable(ModContent.ItemType<AbyssiumBar>(), ModContent.ItemType<IncineriteBar>());
        }

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
            Item.createTile = ModContent.TileType<AbyssiumBar_Tile>();
			
        }

		public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumOre>(), 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
