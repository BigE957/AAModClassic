using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items.Tiles.Decoration
{
    public class RitualAltar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ritual Altar");
            // Tooltip.SetDefault(@"'An old altar whose function has been lost to time...'");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 34;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.createTile = ModContent.TileType<RitualAltar_Tile>();
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
            recipe.Register();
        }
    }
}