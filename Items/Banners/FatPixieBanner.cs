using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Banners
{
    public class FatPixieBanner : BaseAAItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults() {
			Item.width = 36;
			Item.height = 36;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.createTile = Mod.Find<ModTile>("FatPixieBanner").Type;
			Item.placeStyle = 0;
		}
	}
}