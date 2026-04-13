using Terraria.ID;

namespace AAModClassic.___Content.Inferno.__Hardmode.Items.Consumables
{
    public class InfernoKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inferno Key");
			// Tooltip.SetDefault("'Unlocks the power of the blazing sun'");
		}


        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.LightPurple;
            Item.maxStack = 99;
			Item.value = 800000;
            Item.noMelee = true;
        }

       
    }
}
