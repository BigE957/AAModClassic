using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Materials
{
    public class EventideAbyssium : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eventide Abyssium");
            // Tooltip.SetDefault("Cold as the evening moon");
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
            Item.createTile = Mod.Find<ModTile>("EventideAbyssiumBar").Type;
            Item.value = Terraria.Item.sellPrice(0, 3, 0, 0);
        }
        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssiumOre", 5);
            recipe.AddIngredient(null, "DeepAbyssium", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}
