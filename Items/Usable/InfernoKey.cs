namespace AAMod.Items.Usable
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
            Item.rare = 6;
            Item.maxStack = 99;
			Item.value = 800000;
            Item.noMelee = true;
        }

       
    }
}
