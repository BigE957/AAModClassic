using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.GreedCavern._Hardmode.Items.Greed.Calamity
{
    public class GreedLore : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Greed");
            /* Tooltip.SetDefault(@"What is this..? Another worm like...him?
Impossible, they were all purged except for the Devourer because he escaped into a--
...hmm...what if he wasn't the only one with that ability..?"); */
        }

        public override void UpdateInventory(Player player)
        {
            if (ModLoader.GetMod("CalamityMod") == null)
            {
                Item.TurnToAir();
            }
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (ModLoader.GetMod("CalamityMod") == null)
            {
                Item.active = false;
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Lime;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            return false;
        }
    }
}