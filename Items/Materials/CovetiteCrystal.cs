using AAModClassic;
using Terraria.ID;

namespace AAModClassic.Items.Materials
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
			Item.maxStack = 99;
            Item.rare = ItemRarityID.LightPurple;
        }
    }
}
