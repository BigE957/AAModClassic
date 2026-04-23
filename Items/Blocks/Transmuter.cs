using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class Transmuter : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Material Transmuter");
            // Tooltip.SetDefault(@"Allows for Transmutation of materials into their counterparts");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 34;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.expert = true; Item.expertOnly = true;
            Item.createTile = ModContent.TileType<Transmuter_Tile>();
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
            recipe.AddRecipeGroup("AAModClassic:Altar");
            recipe.Register();
        }
    }
}