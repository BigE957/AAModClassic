using AAModClassic;
using Terraria.ID;

namespace AAModClassic.Items.Misc
{
    public class GlitchFish : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glitch Fish");
        }
        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 26;
            Item.height = 26;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest; 
        }

        public override bool IsQuestFish()
        {
            return true;
        }

        public override bool IsAnglerQuestAvailable()
        {
            return true;
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = Lang.questFish("GlitchFish");
            catchLocation = Lang.questFish("GlitchFishLocation");
        }
    }
}