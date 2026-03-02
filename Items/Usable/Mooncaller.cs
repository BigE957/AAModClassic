using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Usable
{
    public class Mooncaller : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mooncaller");
            /* Tooltip.SetDefault(@"Brings forth the shimmering moon.
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime || Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */)
            {
                return false;
            }
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Main.dayTime = false;
            Main.time = 0;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "DeepAbyssium", 10);
            recipe.AddIngredient(null, "SoulOfSpite", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
