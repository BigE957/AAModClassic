using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class GreedLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stone Lantern");
		}


		public override void SetDefaults()
		{
            Item.width = 64;
			Item.height = 34;
            Item.value = 150;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("GreedLantern").Type;
		}
	}
}