using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.Oroboros
{
    public class OroborosBathtub : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Bathtub");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<OroborosBathtub>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OroborosWood>(), 14);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
            
        }

    }
}