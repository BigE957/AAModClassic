using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class CovetiteOre : BaseAAItem
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
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("CovetiteOre").Type;
            Item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Covetite Ore");
            // Tooltip.SetDefault("Only a fool would want this. Makes sense why greed has it.");
        }

    }
}
