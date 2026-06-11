using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Consumables
{
    public class GildedKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gilded Key");
			// Tooltip.SetDefault("This probably unlocks...something?");
		}

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightPurple;
            Item.maxStack = Item.CommonMaxStack;
			Item.value = 800000;
            Item.noMelee = true;
        }
    }
}
