using Ionic.Zlib;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public static class CrateLootHelper
    {
        public static void RegisterBiomeCrateDrops(this ILoot loot, bool hardmode, IItemDropRule[] topLoot = null, IItemDropRule[] bottomLoot = null)
        {
            #region The Mess
            //TODO: clean all of this up. make it use tileid instead of numbers
            IItemDropRule coin = ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 4, 5, 12);
            IItemDropRule[] ores =
            [
                ItemDropRule.NotScalingWithLuck(12, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(699, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(11, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(700, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(14, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(701, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(13, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(702, 1, 20, 35)
            ];
            IItemDropRule[] hardmodeOres =
            [
                ItemDropRule.NotScalingWithLuck(364, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(1104, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(365, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(1105, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(366, 1, 20, 35),
                ItemDropRule.NotScalingWithLuck(1106, 1, 20, 35)
            ];
            IItemDropRule[] bars =
            [
                ItemDropRule.NotScalingWithLuck(22, 1, 6, 16),
                ItemDropRule.NotScalingWithLuck(704, 1, 6, 16),
                ItemDropRule.NotScalingWithLuck(21, 1, 6, 16),
                ItemDropRule.NotScalingWithLuck(705, 1, 6, 16),
                ItemDropRule.NotScalingWithLuck(19, 1, 6, 16),
                ItemDropRule.NotScalingWithLuck(706, 1, 6, 16)
            ];
            IItemDropRule[] hardmodeBars =
            [
                ItemDropRule.NotScalingWithLuck(381, 1, 5, 16),
                ItemDropRule.NotScalingWithLuck(1184, 1, 5, 16),
                ItemDropRule.NotScalingWithLuck(382, 1, 5, 16),
                ItemDropRule.NotScalingWithLuck(1191, 1, 5, 16),
                ItemDropRule.NotScalingWithLuck(391, 1, 5, 16),
                ItemDropRule.NotScalingWithLuck(1198, 1, 5, 16)
            ];
            IItemDropRule[] potions =
            [
                ItemDropRule.NotScalingWithLuck(288, 1, 2, 4),
                ItemDropRule.NotScalingWithLuck(296, 1, 2, 4),
                ItemDropRule.NotScalingWithLuck(304, 1, 2, 4),
                ItemDropRule.NotScalingWithLuck(305, 1, 2, 4),
                ItemDropRule.NotScalingWithLuck(2322, 1, 2, 4),
                ItemDropRule.NotScalingWithLuck(2323, 1, 2, 4)
            ];
            IItemDropRule[] extraPotions =
            [
                ItemDropRule.NotScalingWithLuck(188, 1, 5, 17),
                ItemDropRule.NotScalingWithLuck(189, 1, 5, 17)
            ];
            IItemDropRule[] extraBait =
            [
                ItemDropRule.NotScalingWithLuck(2676, 3, 2, 6),
                ItemDropRule.NotScalingWithLuck(2675, 1, 2, 6)
            ];
            #endregion

            //TODO: cal early hm progression rework support
            IItemDropRule hardmodeBiomeCrateOres = ItemDropRule.SequentialRulesNotScalingWithLuck(7, new OneFromRulesRule(2, hardmodeOres), new OneFromRulesRule(1, ores));
            IItemDropRule hardmodeBiomeCrateBars = ItemDropRule.SequentialRulesNotScalingWithLuck(4, new OneFromRulesRule(3, 2, hardmodeBars), new OneFromRulesRule(1, bars));

            IItemDropRule[] lootPreHardmode =
            [
                coin,
                new OneFromRulesRule(7, ores),
                new OneFromRulesRule(4, bars),
                new OneFromRulesRule(3, potions)
            ];
            IItemDropRule[] lootHardmode =
            [
                coin,
                hardmodeBiomeCrateOres,
                hardmodeBiomeCrateBars,
                new OneFromRulesRule(3, potions)
            ];

            IItemDropRule[] middleLoot = hardmode == false ? lootPreHardmode : lootHardmode;
            IItemDropRule[] lootFinal = MergeArrays(topLoot, middleLoot, bottomLoot);

            loot.Add(ItemDropRule.AlwaysAtleastOneSuccess(lootFinal));
            loot.Add(new OneFromRulesRule(2, extraPotions));
            loot.Add(ItemDropRule.SequentialRulesNotScalingWithLuck(2, extraBait));
        }

        //TODO: make a math helper file and put this there
        /// <summary>
        /// Merges all input arrays, and returns them as one array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays">All of the arrays you wish to add together..</param>
        /// <returns>A single array containing all input arrys, in the order they were input.</returns>
        public static T[] MergeArrays<T>(params T[][] arrays)
        {
            int totalLength = 0;
            foreach (T[] array in arrays)
            {
                if (array == null) 
                    continue;
                totalLength += array.Length;
            }
            T[] finalArray = new T[totalLength];
            
            int currentLength = 0;
            int startingPoint = 0;
            foreach (T[] array in arrays)
            {
                if (array == null)
                    continue;
                currentLength += array.Length;
                for (int i = startingPoint; i < currentLength; i++)
                    finalArray[i] = array[i - startingPoint];
                startingPoint += array.Length;
            }

            return finalArray;
        }
    }
}
