using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Content.Inferno.World.BiomeChest
{
    public class InfernoChest : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inferno Chest");
		}


		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 28;
            Item.value = 500;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
			Item.createTile = ModContent.TileType<InfernoChest_Tile>();
		}
	}
}