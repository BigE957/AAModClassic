using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.__Hardmode.Items.Materials
{
    public class ForsakenFragment : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forsaken Fragment");
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = 20000;
			Item.rare = ItemRarityID.Pink;
		}
	}
}