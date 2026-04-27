using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Usable
{
    public class RiftMirror : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rift Mirror");
            /* Tooltip.SetDefault(@"Pressing the rift hotkey returns you home
Pressing the rift return hotkey brings you back to your most recent rift location"); */
        }    

		public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicMirror);
            Item.maxStack = Item.CommonMaxStack;
			Item.useAnimation = 15;
            Item.useTime = 15;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.IceMirror);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}
