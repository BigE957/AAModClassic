using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hell.___PreHardmode.Items.Materials
{
    public class DevilSilk : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Devil Silk");
            // Tooltip.SetDefault("Physical Sin; feels good, but it isn't a good long-lasting material");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
