using Terraria;
using Terraria.ID;

namespace AAModClassic.Items.DevTools
{
    public class Undowner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Undowner");
            /* Tooltip.SetDefault(@"Undowns all bosses.
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
            NPC.downedAncientCultist = false;
            NPC.downedBoss1 = false;
            NPC.downedBoss2 = false;
            NPC.downedBoss3 = false;
            NPC.downedChristmasIceQueen = false;
            NPC.downedChristmasSantank = false;
            NPC.downedChristmasTree = false;
            NPC.downedClown = false;
            NPC.downedFishron = false;
            NPC.downedFrost = false;
            NPC.downedGoblins = false;
            NPC.downedGolemBoss = false;
            NPC.downedHalloweenKing = false;
            NPC.downedHalloweenTree = false;
            NPC.downedMartians = false;
            NPC.downedMechBoss1 = false;
            NPC.downedMechBoss2 = false;
            NPC.downedMechBoss3 = false;
            NPC.downedMechBossAny = false;
            NPC.downedMoonlord = false;
            NPC.downedPirates = false;
            NPC.downedPlantBoss = false;
            NPC.downedQueenBee = false;
            NPC.downedSlimeKing = false;
            NPC.downedTowerNebula = false;
            NPC.downedTowerSolar = false;
            NPC.downedTowerStardust = false;
            NPC.downedTowerVortex = false;
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
            AAWorld.downedToad = false;
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
