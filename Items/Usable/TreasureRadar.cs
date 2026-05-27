using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;


namespace AAModClassic.Items.Usable
{
    public class TreasureRadar : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Treasure Hunter");
            /* Tooltip.SetDefault(@"200 Tile Range
            Lights up chests on the map");	*/		
		}

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 38;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 2, 0, 0);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 50;
            Item.useTime = 50;
            //item.UseSound = mod.SoundItem("LiquidRadarUse");			
        }

        public override bool? UseItem(Player p)/* tModPorter Suggestion: Return null instead of false */
            {
                if (Main.myPlayer == p.whoAmI && Main.netMode != NetmodeID.Server)
            {
                int cX = (int)(p.Center.X / 16f); int cY = (int)(p.Center.Y / 16f);
                int range = 200;
                int topX = Math.Max(10, cX - range);
                int topY = Math.Max(10, cY - range);
                int bottomX = Math.Min(Main.maxTilesX, cX + range);
                int bottomY = Math.Min(Main.maxTilesY, cY + range);
                bool updateMap = false;
                for (int x = topX; x < bottomX; x++)
                {
                    for (int y = topY; y < bottomY; y++)
                    {
                        if (Main.tile[x, y] == null) { continue; }
                        Tile tile = Main.tile[x, y];
                        if (tile.HasTile && (Main.tileContainer[tile.TileType] == true))
                        {
                            if (Main.Map.UpdateLighting(x, y, Math.Max(Main.Map[x, y].Light, (byte)255))) updateMap = true;
                        }
                    }
                }
                if (updateMap)
                {
                    Main.mapMinX = topX; Main.mapMinY = topY;
                    Main.mapMaxX = bottomX; Main.mapMaxY = bottomY;
                    Main.updateMap = Main.refreshMap = true;
                }
            }
            return true;
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, false, false); }
        public override void UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, Item); }
	}
}