using Terraria;
using Terraria.ID;

namespace AAModClassic.___Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.Accessories
{
    public class GlowingTruffle : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Truffle");
            /* Tooltip.SetDefault(
@"+30 Mana
Don't lick it."); */
        }


        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 30;
        }

    }
}