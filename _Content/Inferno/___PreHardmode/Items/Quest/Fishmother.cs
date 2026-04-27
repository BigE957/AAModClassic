using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Quest
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
            Item.maxStack = Item.CommonMaxStack;
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