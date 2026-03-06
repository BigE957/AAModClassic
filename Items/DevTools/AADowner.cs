using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.DevTools
{
    public class AADowner : BaseAAItem
    {
        public override string Texture => "AAModClassic/Items/DevTools/AAUndowner";
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
            AAWorld.downedAkuma = true;
            AAWorld.downedAllAncients = true;
            AAWorld.downedAshe = true;
            AAWorld.downedBrood = true;
            AAWorld.downedDB = true;
            AAWorld.downedNC = true;
            AAWorld.downedDjinn = true;
            AAWorld.downedEquinox = true;
            AAWorld.downedFungus = true;
            AAWorld.downedGrips = true;
            AAWorld.downedHaruka = true;
            AAWorld.downedHydra = true;
            AAWorld.downedMonarch = true;
            AAWorld.downedRajah = true;
            AAWorld.downedSag = true;
            AAWorld.downedSAncient = true;
            AAWorld.downedSerpent = true;
            AAWorld.downedShen = true;
            AAWorld.downedSisters = true;
            AAWorld.downedYamata = true;
            AAWorld.downedZero = true;
            AAWorld.downedRajahsRevenge = true;
            AAWorld.downedAthena = true;
            AAWorld.downedAnubis = true;
            AAWorld.downedGreed = true;
            return true;
        }
    }
}
