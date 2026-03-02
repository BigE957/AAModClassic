namespace AAMod.Items.Materials
{
    public class MadnessFragment : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Fragment");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = 2;
        }
    }
}