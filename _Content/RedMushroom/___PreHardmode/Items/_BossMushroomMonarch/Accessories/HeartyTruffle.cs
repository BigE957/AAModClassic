using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.Accessories
{
    public class HeartyTruffle : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

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
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
                player.statLifeMax2 += 50;
        }

    }
}