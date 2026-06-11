using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Quest
{
    public class MadnessMushroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Mushroom");
            /* Tooltip.SetDefault(@"An exceedingly rare mushroom
Maybe the Mushman knows what to do with it?"); */
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Purple;
        }
    }
}