using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Greed
{
    public class DesireCharm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charm of Desire");
            /* Tooltip.SetDefault(@"Grabbing coins boosts your damage by 1% for 4 seconds
Grabbing another coin increases the damage by 1% and resets the countdown
Caps out at 20% damage"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 6, 0, 0);
            Item.rare = 8;
            Item.accessory = true;
            Item.expertOnly = true;
            Item.expert = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            TooltipLine DamageTooltip = new TooltipLine(Mod, "Damage", Language.GetTextValue("Mods.AAMod.Common.DesireCharmInfo") + modPlayer.GreedyDamage + "%");
            tooltips.Add(DamageTooltip);

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.GreedCharm = true;
        }
    }
}