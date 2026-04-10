using AAModClassic.___Content.Mire._PreHardmode.NPCs._BossHydra;
using AAModClassic.Utilities;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic.Items.Misc
{
    public class TriHeadedKoi : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tri-headed Koi");
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
            return NPCExtensions.BeenKilled<HydraBody>();
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.QuestFish.TriHeadedKoi");
            catchLocation = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.QuestFish.TriHeadedKoiLocation");
        }
    }
}