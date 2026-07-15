using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class ScorchedDynastyWoodWall : BaseAAItem, ILocalizedModType
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
            Item.createWall = ModContent.WallType<ScorchedDynastyWoodWall_Wall>(); //put your CustomBlock Tile name
        }


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scorched Dynasty Wood Wall");
            Item.ResearchUnlockCount = 400;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<ScorchedDynastyWood>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
