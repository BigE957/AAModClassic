using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class RajahBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit Music Box");
            // Tooltip.SetDefault(@"Plays 'JUSTICE' by Spectral Aves");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("RajahBox").Type;
            Item.width = 36;
            Item.height = 36;
            Item.rare = 4;
            Item.value = 10000;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "Carrot", 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
