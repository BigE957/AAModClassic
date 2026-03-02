
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class GlowingMushiumBar : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = 1;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("GlowingMushiumBar").Type;
            Item.value = Item.sellPrice(0, 0, 9, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Mushium Bar");
            // Tooltip.SetDefault("Glowy");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.White, lightColor, lightColor, Color.White);
        }

        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "GlowingMushium", 3);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
