using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class CodeMagnetOff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binary Code Magnet");
			/* Tooltip.SetDefault(@"Pulls items to you by moving its code closer to you
Right click the item to turn it on"); */
		}

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 1;
			Item.value = 8000;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<CodeMagnet>());
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "DoomiteScrap", 20);
            recipe.AddIngredient(null, "Doomite", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
