using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class SoulFragment : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Fragment");
		}

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.value = 20000;
			Item.rare = ItemRarityID.Pink;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }
    }
}