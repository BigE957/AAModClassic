using AAModClassic.___Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomBathtub : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Bathtub");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 26;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 250;
            Item.createTile = ModContent.TileType<DoomBathtub_Tile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomsdayCircuitPlating>(), 14);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
            
        }

    }
}