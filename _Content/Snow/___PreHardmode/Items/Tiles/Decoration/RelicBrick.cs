using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Tiles.Decoration
{
    public class RelicBrick : BaseAAItem, ILocalizedModType
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
            Item.createTile = ModContent.TileType<RelicBrick_Tile>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Relic Brick");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ModContent.ItemType<VikingRelic>(), 1);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RelicBrickWall>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
