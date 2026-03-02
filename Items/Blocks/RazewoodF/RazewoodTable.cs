using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.RazewoodF
{
    public class RazewoodTable : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Table");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 26;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("RazewoodTable").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Razewood").Type, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

    }
}