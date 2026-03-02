using Terraria;

namespace AAMod.Items.DevTools
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
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAWorld.downedAkuma = false;
            AAWorld.downedAllAncients = false;
            AAWorld.downedAshe = false;
            AAWorld.downedBrood = false;
            AAWorld.downedDB = false;
            AAWorld.downedNC = false;
            AAWorld.downedDjinn = false;
            AAWorld.downedEquinox = false;
            AAWorld.downedFungus = false;
            AAWorld.downedGrips = false;
            AAWorld.downedHaruka = false;
            AAWorld.downedHydra = false;
            AAWorld.downedMonarch = false;
            AAWorld.downedRajah = false;
            AAWorld.downedSag = false;
            AAWorld.downedSAncient = false;
            AAWorld.downedSerpent = false;
            AAWorld.downedShen = false;
            AAWorld.downedSisters = false;
            AAWorld.downedYamata = false;
            AAWorld.downedZero = false;
            AAWorld.downedRajahsRevenge = false;
            AAWorld.downedAthena = false;
            AAWorld.downedAnubis = false;
            AAWorld.downedGreed = false;
            AAWorld.downedAthenaA = false;
            AAWorld.downedAnubisA = false;
            AAWorld.downedGreedA = false;
            return true;
        }
    }
}
