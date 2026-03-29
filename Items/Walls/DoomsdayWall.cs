using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Walls
{
    public class DoomsdayWall : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = Mod.Find<ModWall>("DoomsdayWall").Type; //put your CustomBlock Tile name
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Circuit Wall");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "DoomsdayPlating");
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
