using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Boss.MushroomMonarch
{
    public class MonarchTrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushroom Monarch Trophy");
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
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 2000;
            Item.rare = ItemRarityID.Green;
			Item.createTile = ModContent.TileType<MonarchTrophy_Tile>();
		}
	}
}