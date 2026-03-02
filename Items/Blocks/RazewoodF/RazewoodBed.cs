using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.RazewoodF
{
    public class RazewoodBed : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Bed");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("RazewoodBed").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Razewood").Type, 15);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

        }

    }
}