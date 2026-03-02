using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Boss.Shen
{
    public class ShenTrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Trophy");
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
			Item.rare = ItemRarityID.Green;
			Item.createTile = Mod.Find<ModTile>("ShenTrophy").Type;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
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