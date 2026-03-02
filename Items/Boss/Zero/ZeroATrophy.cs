using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Boss.Zero
{
    public class ZeroATrophy : BaseAAItem
	{
        public static int type;

        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Zero Protocol Trophy");
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

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 2000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = Mod.Find<ModTile>("ZeroATrophy").Type;
		}
    }
}