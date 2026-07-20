using AAModClassic._Content.RedMushroom.World.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Decoration.Furniture
{
    public class RedmushPlatform : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults() 
        {
			Item.ResearchUnlockCount = 200;
		}

        public override void SetDefaults() 
        {
            Item.width = 20;
            Item.height = 20;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.value = Item.sellPrice(0, 0, 0, 0);

            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<RedmushPlatform_Tile>(); 
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes() 
        {
            CreateRecipe(2)
                .AddIngredient(ModContent.ItemType<MushroomBlock>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}