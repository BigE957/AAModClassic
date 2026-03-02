using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class DesireTalisman : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Talisman of Desire");
            /* Tooltip.SetDefault(@"Grabbing coins boosts your damage by 1% for 4 seconds
Grabbing another coin increases the damage by 1% and resets the countdown
Caps out at 20% damage
Increases coin pickup range 
Shops have lower prices
Hitting enemies will sometimes drop extra coins"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
            Item.expertOnly = true;
            Item.expert = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            TooltipLine DamageTooltip = new TooltipLine(Mod, "Damage", Language.GetTextValue("Mods.AAMod.Common.DesireTalismanInfo") + modPlayer.GreedyDamage + "%");
            tooltips.Add(DamageTooltip);

            base.ModifyTooltips(tooltips);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            modPlayer.GreedTalisman = true;
            player.goldRing = true;
            player.coins = true;
            player.discountAvailable = true;
        }
    }
}