using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class GreedKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gilded Key");
			// Tooltip.SetDefault("This probably unlocks...something?");
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
