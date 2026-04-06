using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.___Content.Mire.World.who
{
    public class MireChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mire Chest");
		}


		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 28;
            Item.value = 500;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
			Item.createTile = ModContent.TileType<MireChest_Tile>();
		}
	}
}