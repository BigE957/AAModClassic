using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Doom
{
    public class DoomPiano : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Piano");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<DoomPiano>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 15);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddIngredient(ItemID.Bone, 4);
            recipe.AddTile(null, "ACS");
            recipe.Register();

        }

    }
}