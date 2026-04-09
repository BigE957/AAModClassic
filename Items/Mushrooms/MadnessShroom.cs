using Terraria.ID;

namespace AAModClassic.Items.Mushrooms
{
    public class MadnessShroom : BaseAAItem
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
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }
    }
}