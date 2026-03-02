using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks.RazewoodF
{
    public class RazewoodDoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Door");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("RazewoodDoorClosed").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Razewood").Type, 6);
            //recipe.AddIngredient(ItemID.Torch, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

    }
}