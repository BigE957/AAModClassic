using AAModClassic._Unreleased;
using AAModClassic.UI.WorldGen;
using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.DevTools
{
    public class AAUndowner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AA Undowner");
            /* Tooltip.SetDefault(@"Undowns all AA bosses.
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
            AAWorld.downedAshe = false;
            AAWorld.downedDB = false;
            AAWorld.downedNC = false;
            AAWorld.downedEquinox = false;
            AAWorld.downedGrips = false;
            AAWorld.downedHaruka = false;
            AAWorld.downedSisters = false;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
            {
                AAWorld_Unreleased.downedIZ = false;
                AAWorld_Unreleased.downedSoC = false;
            }
            return true;
        }
    }
}
