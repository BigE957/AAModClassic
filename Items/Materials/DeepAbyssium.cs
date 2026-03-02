using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Materials
{
    public class DeepAbyssium : BaseAAItem
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
            Item.createTile = Mod.Find<ModTile>("DeepAbyssium").Type;
            Item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
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
            recipe.AddIngredient(null, "AbyssiumBar", 1);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();
        }
    }
}
