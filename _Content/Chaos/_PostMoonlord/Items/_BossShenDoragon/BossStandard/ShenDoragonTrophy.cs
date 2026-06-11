using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard
{
    public class ShenDoragonTrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Doragon Trophy");
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
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 2000;
			Item.createTile = ModContent.TileType<ShenDoragonTrophy_Tile>();
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        
    }
}