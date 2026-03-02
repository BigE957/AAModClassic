using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.RazewoodF
{
    public class RazewoodChandelier : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Chandelier");
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
            Item.createTile = Mod.Find<ModTile>("RazewoodChandelier").Type;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Razewood").Type, 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddIngredient(ItemID.Chain);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

    }
}