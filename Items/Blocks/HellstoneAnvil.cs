using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class HellstoneAnvil : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hellstone Anvil");
            // Tooltip.SetDefault("Is this thing supposed to be on fire?");
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<HellstoneAnvil_Tile>();
        }

        public override void AddRecipes()
        { 
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.HellstoneBar, 20);
                recipe.AddIngredient(ItemID.IronAnvil, 1);
                recipe.AddIngredient(ItemID.ObsidianWorkBench, 1);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.HellstoneBar, 20);
                recipe.AddIngredient(ItemID.LeadAnvil, 1);
                recipe.AddIngredient(ItemID.ObsidianWorkBench, 1);
                recipe.Register();
            }
        }
    }
}
