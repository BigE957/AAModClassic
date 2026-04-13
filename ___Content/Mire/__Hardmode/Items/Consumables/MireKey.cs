using Terraria.ID;

namespace AAModClassic.___Content.Mire.__Hardmode.Items.Consumables
{
    public class MireKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mire Key");
			// Tooltip.SetDefault("'Unlocks the power of the wrathful abyss'");
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
