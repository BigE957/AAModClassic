using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class DaybreakIncineriteOre : BaseAAItem
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
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("DaybreakIncineriteOre").Type; //put your CustomBlock Tile name
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
            // DisplayName.SetDefault("Daybreak Incinerite Ore");
            // Tooltip.SetDefault("It's warm to the touch, like a bright summer morning.");
        }
    }
}
