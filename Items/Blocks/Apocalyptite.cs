using AAModClassic;
using AAModClassic.Globals;
using AAModClassic.Tiles.Ore;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
    public class Apocalyptite : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Apocalyptite");
            // Tooltip.SetDefault(@"");
        }

        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Red;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ApocalyptiteOre_Tile>(); //put your CustomBlock Tile name
            
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }
        
    }
}
