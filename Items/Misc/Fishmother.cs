using AAModClassic.NPCs.Bosses.Broodmother;
using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.Localization;

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
            return NPCExtensions.BeenKilled<Broodmother>();
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.QuestFish.Fishmother");
            catchLocation = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.QuestFish.FishmotherLocation");
        }
    }
}