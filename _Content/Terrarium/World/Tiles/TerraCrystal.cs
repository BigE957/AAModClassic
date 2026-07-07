using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class TerraCrystal : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Crystal");
        }

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
            Item.createTile = ModContent.TileType<TerraCrystal_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ModContent.ItemType<TerraShard>(), 1);
            recipe.AddIngredient(ItemID.StoneBlock, 2);
            recipe.AddTile(ModContent.TileType<TruePaladinsSmeltery_Tile>());
            recipe.Register();
        }
    }
}
