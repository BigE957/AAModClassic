using AAModClassic;
using Terraria.ID;

namespace AAModClassic.Items.Misc
{
    public class Fishmother : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fishmother");
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
            return AAWorld.downedBrood;
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = Lang.questFish("Fishmother");
            catchLocation = Lang.questFish("FishmotherLocation");
        }
    }
}