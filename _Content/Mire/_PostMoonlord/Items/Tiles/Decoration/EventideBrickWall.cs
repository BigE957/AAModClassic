using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Tiles.Decoration
{
    public class EventideBrickWall : BaseAAItem
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
            Item.createWall = ModContent.WallType<EventideBrickWall_Wall>();
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eventide Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBrick>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
