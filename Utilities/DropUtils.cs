using AAModClassic._CrossMod.CalamityMod;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Utilities
{
    public class MasterRevDropRule : IItemDropRuleCondition
    {
        private static bool CheckRevenge()
        {
            return ModLoader.TryGetMod("CalamityMod", out Mod calamity) && (bool)calamity.Call("GetDifficultyActive", "revengeance");
        }

        bool IItemDropRuleCondition.CanDrop(DropAttemptInfo info) => CheckRevenge() || Main.masterMode;

        bool IItemDropRuleCondition.CanShowItemDropInUI() => CheckRevenge() || Main.masterMode;

        string IProvideItemConditionDescription.GetConditionDescription()
        {
            return ModLoader.HasMod("CalamityMod") && !Main.masterMode
                ? Language.GetTextValue("Bestiary_ItemDropConditions.SimpleCondition", Language.GetTextValue("Mods.CalamityMod.Condition.InRev"))
                : Language.GetTextValue("Bestiary_ItemDropConditions.IsMasterMode");
        }
    }

    public class LoreItemDropCondition<T> : IItemDropRuleCondition, IProvideItemConditionDescription where T : ModNPC
    {
        public bool CanDrop(DropAttemptInfo info) => CalamityMod.IsEnabled && !NPCExtensions.BeenKilled<T>(true);
        public bool CanShowItemDropInUI() => CalamityMod.IsEnabled;
        public string GetConditionDescription() => null;
    }

    public class LoreItemDropCondition(Func<bool> isDowned) : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => CalamityMod.IsEnabled && !isDowned.Invoke();
        public bool CanShowItemDropInUI() => CalamityMod.IsEnabled;
        public string GetConditionDescription() => null;
    }

    #region Calamity's Per Player Drop Rule
    public class PerPlayerDropRule(int itemID, int denominator, int minQuantity = 1, int maxQuantity = 1, int numerator = 1, int protectFrames = PerPlayerDropRule.DefaultDropProtectionTime) : CommonDrop(itemID, denominator, minQuantity, maxQuantity, numerator)
    {
        // Default instanced drops are protected for 15 minutes, because they are used for boss bags.
        // You can customize this duration as you see fit. Calamity defaults it to 5 minutes.
        private const int DefaultDropProtectionTime = 18000; // 5 minutes
        private readonly int protectionTime = protectFrames;

        // Overriding CanDrop is unnecessary. This drop rule has no condition.
        // If you want to use a condition with PerPlayerDropRule, use DropHelper.If

        public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default;
            if (denominator == 1 || info.rng.Next(chanceDenominator) < chanceNumerator)
            {
                int stack = minQuantity == maxQuantity ? minQuantity : info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1);
                TryDropInternal(info, itemId, stack);
                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }

        // The contents of this method are more or less copied from CommonCode.DropItemLocalPerClientAndSetNPCMoneyTo0
        private void TryDropInternal(DropAttemptInfo info, int itemId, int stack)
        {
            if (itemId <= 0 || itemId >= ItemLoader.ItemCount)
                return;

            // If server-side, then the item must be spawned for each client individually.
            if (Main.dedServ)
            {
                NPC npc = info.npc;
                int idx = Item.NewItem(npc.GetSource_Loot(), npc.Center, itemId, stack, true, -1);
                if (idx < Main.maxItems)
                {
                    Main.timeItemSlotCannotBeReusedFor[idx] = protectionTime;
                    foreach (Player player in Main.ActivePlayers)
                        NetMessage.SendData(MessageID.InstancedItem, player.whoAmI, -1, null, idx);
                    Main.item[idx].active = false;
                }
            }

            // Otherwise just drop the item.
            else
                CommonCode.DropItem(info, itemId, stack);
        }
    }
    #endregion

    public static class DropUtils
    {
        /// <summary>
        /// Adds a Lore Item drop to the NPC.
        /// </summary>
        /// <param name="loot">The ILoot interface for the loot table.</param>
        /// <param name="itemID">The item ID to drop.</param>
        /// <returns>A LeadingConditionRule which you can attach more PerPlayer or other rules to as you want.</returns>
        public static LeadingConditionRule AddLoreItemDrop<T>(this ILoot loot, int itemID) where T : ModNPC
        {
            LeadingConditionRule lcr = new(new LoreItemDropCondition<T>());
            lcr.OnSuccess(new PerPlayerDropRule(itemID, 1));
            loot.Add(lcr);
            return lcr;
        }
    }
}
