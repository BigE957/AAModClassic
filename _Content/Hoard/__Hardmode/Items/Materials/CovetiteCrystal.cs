using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Hoard.__Hardmode.Items.Materials
{
    public class CovetiteCrystal : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Covetite Crystal");
            /* Tooltip.SetDefault(@"You have a strange desire for this crystal, 
despite you already owning it."); */
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightPurple;
        }
    }
}
