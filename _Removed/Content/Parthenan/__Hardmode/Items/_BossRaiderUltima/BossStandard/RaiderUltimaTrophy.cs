using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard
{
    public class RaiderUltimaTrophy : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Raider Ultima Trophy");
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
			Item.rare = ItemRarityID.Blue;
			Item.createTile = ModContent.TileType<RaiderUltimaTrophy_Tile>();
		}
    }
}