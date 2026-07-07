using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Materials
{
    public class BlackLotus : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Lotus");
            // Tooltip.SetDefault("It's said that someone offered $160000 for this thing.");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
