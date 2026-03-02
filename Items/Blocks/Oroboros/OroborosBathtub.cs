using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Oroboros
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
            Item.createTile = Mod.Find<ModTile>("OroborosBathtub").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("OroborosWood").Type, 14);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
            
        }

    }
}