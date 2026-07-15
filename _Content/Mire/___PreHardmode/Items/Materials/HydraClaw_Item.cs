using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Materials
{
    public class HydraClaw_Item : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Claw");
            // Tooltip.SetDefault("Don't prick yourself");
            Item.ResearchUnlockCount = 25;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
        }
    }
}