using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.Materials
{
    public class HeroShards : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hero Relics");
            // Tooltip.SetDefault("Shards of a shattered relic");
        }

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Yellow;
		}
    }
}
