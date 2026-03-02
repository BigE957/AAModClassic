using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.RazewoodF
{
    public class RazewoodChair : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Chair");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("RazewoodChair").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Razewood").Type, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

    }
}