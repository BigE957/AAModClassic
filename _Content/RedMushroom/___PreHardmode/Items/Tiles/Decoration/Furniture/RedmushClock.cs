using AAModClassic._Content.RedMushroom.World.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushClock : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Furniture.RedMushroom";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
		}

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.value = Item.sellPrice(0, 0, 0, 50);

            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<RedmushClock_Tile>(); 
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 3)
                .AddIngredient(ItemID.Glass, 6)
                .AddIngredient(ModContent.ItemType<MushroomBlock>(), 10)
                .AddTile(TileID.Sawmill)
                .Register();
        }
    }
}