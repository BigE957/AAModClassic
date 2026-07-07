using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Consumables
{
    public class GildedKey : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Consumables";
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
