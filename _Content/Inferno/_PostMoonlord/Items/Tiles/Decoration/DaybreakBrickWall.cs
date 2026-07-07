using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Tiles.Decoration
{
    public class DaybreakBrickWall : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<DaybreakBrickWall_Wall>(); //put your CustomBlock Tile name
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daybreak Brick Wall");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<DaybreakBrick>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
