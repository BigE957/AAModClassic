using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Banners
{
	public class TerraSerpentBanner : BaseAAItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults() {
			Item.width = 10;
			Item.height = 24;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = 1000;
			Item.createTile = Mod.Find<ModTile>("Banners").Type;
			Item.placeStyle = 47;        //Place style means which frame(Horizontally, starting from 0) of the tile should be placed
		}
	}
}
