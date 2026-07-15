using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._CrossMod;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Quest
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
            return !ContentReplacementSystem.NeedToReplaceContent && NPCExtensions.BeenKilled<HydraBody>();
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            description = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.QuestFish.TriHeadedKoi");
            catchLocation = Language.GetTextValue("Mods.AAModClassic.NPCs.TownNPCs.QuestFish.TriHeadedKoiLocation");
        }
    }
}