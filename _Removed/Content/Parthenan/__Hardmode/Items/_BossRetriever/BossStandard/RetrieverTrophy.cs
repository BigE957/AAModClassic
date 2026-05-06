using Terraria.ModLoader;
namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard
{
    public class RetrieverTrophy : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Retriever Trophy");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = 1;
            Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 2000;
			Item.rare = 1;
			Item.createTile = ModContent.TileType<RetrieverTrophy_Tile>();
		}
    }
}