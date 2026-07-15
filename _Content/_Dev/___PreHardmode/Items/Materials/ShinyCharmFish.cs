using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Materials
{
    public class ShinyCharmFish : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Materials";
		public override void SetDefaults()
		{
			Item.width = 34;
            Item.height = 36;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Cyan;
		}

		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Shiny Charm Fish");
            // Tooltip.SetDefault("A kind of rare fish");

            Item.ResearchUnlockCount = 100;
        }
    }
}
