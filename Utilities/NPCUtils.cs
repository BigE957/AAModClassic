using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public static class NPCUtils
    {
        /// <summary>
        /// Clones the given NPC's loot except anything input and adds it to the given loot pool.
        /// </summary>
        /// <param name="npcToClone">The ID of the npc whose loot is to be cloned.</param>
        /// <param name="itemIdsToExclude">The items present in the former NPC's lootpool you do not wish to clone.</param>
        /// <param name="leadingCondition">The loading condition rule to apply to all cloned loot.</param>
        /// <param name="loot">The loot pool you wish to add the loot to.</param>
        public static void CloneDropsWithoutInput(int npcToClone, int[] itemIdsToExclude, LeadingConditionRule leadingCondition, ref NPCLoot loot)
        {
            List<IItemDropRule> clonedDropRules = Main.ItemDropsDB.GetRulesForNPCID(npcToClone, false);

            foreach (IItemDropRule rule in clonedDropRules)
            {
                int itemID = 0;

                if (rule is ItemDropWithConditionRule conditionDrop)
                {
                    itemID = conditionDrop.itemId;
                }
                else if (rule is CommonDrop commonDrop)
                {
                    itemID = commonDrop.itemId;
                }

                if (itemIdsToExclude.Contains(itemID))
                {
                    continue;
                }

                leadingCondition.OnSuccess(rule);
            }

            loot.Add(leadingCondition);
        }
    }
}
