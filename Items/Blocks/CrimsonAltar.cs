using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.Items.Blocks
{
    public class CrimsonAltar : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Altar");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("EvilAltar").Type;
            Item.placeStyle = 1;
            Item.width = 28;
            Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.value = 1000;
            Item.accessory = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 15);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}

