using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials
{
    public class FulguriteBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fulgurite Bar");
            // Tooltip.SetDefault("It's static-y");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<FulguriteBar_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteShard>(), 3);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}
