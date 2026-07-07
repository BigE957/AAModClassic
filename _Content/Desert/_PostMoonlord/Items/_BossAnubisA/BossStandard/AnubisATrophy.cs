using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.BossStandard
{
    public class AnubisATrophy : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Placeables.Boss.Trophy";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forsaken Anubis Trophy");
		}

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 2000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = ModContent.TileType<AnubisATrophy_Tile>();
		}
	}
}