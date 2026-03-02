using AAModClassic;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic.Items.Blocks
{
    public class DoomsdayChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomsday Chest");
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
			Item.createTile = Mod.Find<ModTile>("DoomsdayChest").Type;
		}
	}
}