using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Doom
{
    public class DoomLamp: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Lamp");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("DoomLamp").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ApocalyptitePlate").Type, 3);
            recipe.AddIngredient(ItemID.Torch, 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();

        }

    }
}