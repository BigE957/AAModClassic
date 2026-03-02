using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.DevTools
{
    public class AASummonerTest : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AA SummonerTest");
            // Tooltip.SetDefault(@"Test the minion's stat");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
        }

        public override bool CanUseItem(Player player)
        {
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string text = "";
            text += "Your max minionslots: " + Main.player[Main.myPlayer].maxMinions + "\n";
            text += "Used minionslots: " + Main.player[Main.myPlayer].slotsMinions;

            TooltipLine line = new TooltipLine(Mod, "newtooltip", text);
            list.Insert(2,line);
        } 
    }
}
