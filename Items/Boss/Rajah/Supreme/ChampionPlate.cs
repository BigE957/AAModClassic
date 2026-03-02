using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah.Supreme
{
    public class ChampionPlate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Plate");
            // Tooltip.SetDefault("Forged from Champium");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
			Item.maxStack = 99;
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

    }
}
