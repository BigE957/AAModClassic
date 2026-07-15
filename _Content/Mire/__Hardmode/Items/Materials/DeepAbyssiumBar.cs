using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Materials
{
    public class DeepAbyssiumBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deep Abyssium Bar");
            // Tooltip.SetDefault("It's a wonder you can even see it, it's so dark");

            ItemTrader.ChlorophyteExtractinator.AddOption_Interchangable(ModContent.ItemType<DeepAbyssiumBar>(), ModContent.ItemType<RadiantIncineriteBar>());

            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {

            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DeepAbyssiumBar_Tile>();
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

		public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 1);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();
        }
    }
}
