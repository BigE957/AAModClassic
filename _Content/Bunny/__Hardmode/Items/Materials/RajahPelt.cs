using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items.Materials
{
    public class RajahPelt : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Materials";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Rabbit Pelt");
            // Tooltip.SetDefault("Surpisingly durable for a pelt of fur");
            Item.ResearchUnlockCount = 25;
        }

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}
