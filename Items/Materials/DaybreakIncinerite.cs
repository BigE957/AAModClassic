using AAModClassic.Tiles.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Materials
{
    public class DaybreakIncinerite : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daybreak Incinerite");
            // Tooltip.SetDefault("Bright as the radiant sun");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
			Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DaybreakIncineriteBar_Tile>();
            Item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
        }
        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DaybreakIncineriteOre", 5);
            recipe.AddIngredient(null, "RadiantIncinerite", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}
