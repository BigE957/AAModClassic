using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Oroboros
{
    public class OroborosCandle : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Candle");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 18;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<OroborosCandle>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OroborosWood>(), 4);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

    }
}