using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic;

namespace AAModClassic.Items.Blocks.Oroboros
{
    public class OroborosWall : BaseAAItem
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
            Item.createWall = ModContent.WallType<OroborosWall>();
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(null, "OroborosWood");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
