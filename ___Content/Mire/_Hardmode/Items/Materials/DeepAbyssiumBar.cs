using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Materials
{
    public class DeepAbyssiumBar : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DeepAbyssium_Tile>();
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deep Abyssium Bar");
            // Tooltip.SetDefault("It's a wonder you can even see it, it's so dark");
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
