using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.BogwoodF
{
    public class BogwoodChandelier : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Chandelier");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 38;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = Mod.Find<ModTile>("BogwoodChandelier").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Bogwood").Type, 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddIngredient(ItemID.Chain);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

    }
}