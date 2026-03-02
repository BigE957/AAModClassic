using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodChair : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Chair");
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
            Item.createTile = Mod.Find<ModTile>("BogwoodChair").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Bogwood").Type, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

    }
}