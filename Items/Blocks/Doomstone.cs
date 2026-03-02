using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Blocks
{
    public class Doomstone : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.rare = 9;
            AARarity = 13;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("Doomstone").Type; //put your CustomBlock Tile name
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

        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Charged Doomstone");
            // Tooltip.SetDefault("");

        }
    }
}
