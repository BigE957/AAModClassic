using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroTrophy : BaseAAItem
	{
        public static int type;

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero Trophy");
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
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 2000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = Mod.Find<ModTile>("ZeroTrophy").Type;
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