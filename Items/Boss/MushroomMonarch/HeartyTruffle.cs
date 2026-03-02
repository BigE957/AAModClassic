using Terraria;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class HeartyTruffle : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hearty Truffle");
            /* Tooltip.SetDefault(
@"+50 Health
Don't eat it"); */
        }


        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
                player.statLifeMax2 += 50;
        }

    }
}