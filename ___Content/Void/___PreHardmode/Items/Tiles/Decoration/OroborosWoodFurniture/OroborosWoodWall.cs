using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic.___Content.Void.___PreHardmode.Items.Tiles.Decoration.OroborosWoodFurniture
{
    public class OroborosWoodWall : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Wood Wall");
        }

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
            Item.createWall = ModContent.WallType<OroborosWoodWall_Wall>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<OroborosWood>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
