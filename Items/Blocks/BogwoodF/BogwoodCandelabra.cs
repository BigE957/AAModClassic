using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.BogwoodF
{
    public class BogwoodCandelabra : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Candelabra");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<BogwoodCandelabra>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bogwood>(), 5);
            recipe.AddIngredient(ItemID.Torch, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

    }
}