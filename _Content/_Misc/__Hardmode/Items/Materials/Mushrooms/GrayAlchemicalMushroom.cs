using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Materials.Mushrooms
{
    public class GrayAlchemicalMushroom : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gray Alchemical Mushroom");
            // Tooltip.SetDefault(@"It smells weird");

            Item.ResearchUnlockCount = 3;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
        }
    }
}