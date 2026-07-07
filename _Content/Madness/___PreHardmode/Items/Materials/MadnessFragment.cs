using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Madness.___PreHardmode.Items.Materials
{
    public class MadnessFragment : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Fragment");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Green;
        }
    }
}