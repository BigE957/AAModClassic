namespace AAMod.Items.Boss.Anubis
{
    public class ForsakenFragment : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forsaken Fragment");
		}

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.value = 20000;
			Item.rare = 5;
		}
	}
}