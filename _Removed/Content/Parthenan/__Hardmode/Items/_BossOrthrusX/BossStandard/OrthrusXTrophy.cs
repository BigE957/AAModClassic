using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard
{
    public class OrthrusXTrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orthrus X Trophy");
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
			Item.createTile = ModContent.TileType<OrthrusXTrophy_Tile>();
		}
	}
}