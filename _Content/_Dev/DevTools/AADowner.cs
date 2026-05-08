using Terraria;
using Terraria.ID;

namespace AAModClassic._Content._Dev.DevTools
{
    public class AADowner : BaseAAItem
    {
        public override string Texture => "AAModClassic/_Content/_Dev/DevTools/AAUndowner";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AA Downer");
            /* Tooltip.SetDefault(@"Downs all AA bosses.
Non-Consumable"); */
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAWorld.downedAshe = true;
            AAWorld.downedDB = true;
            AAWorld.downedNC = true;
            AAWorld.downedEquinox = true;
            AAWorld.downedGrips = true;
            AAWorld.downedHaruka = true;
            AAWorld.downedSisters = true;
            return true;
        }
    }
}
