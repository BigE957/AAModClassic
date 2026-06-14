using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.NPCs
{
    public class FatPixie_Banner : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Banners";

        // The tooltip for this item is automatically assigned from .lang files
        public override void SetDefaults() {
			Item.width = 36;
			Item.height = 36;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.createTile = ModContent.TileType<FatPixie_Banner_Tile>();
			Item.placeStyle = 0;
		}
	}
}