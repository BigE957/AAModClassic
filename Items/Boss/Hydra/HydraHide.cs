using Terraria.ID;

namespace AAMod.Items.Boss.Hydra
{
    public class HydraHide : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 22;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
			
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Hide");
            // Tooltip.SetDefault("The skin of a formidable foe");
        }
    }
}
