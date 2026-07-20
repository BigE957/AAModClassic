using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._CrossMod.CalamityMod;
using AAModClassic.UI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace AAModClassic.Utilities
{
    public class ItemDropRuleConditionUtils
    {
        public class Unofficial : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            public string GetConditionDescription() => null;
        }
        public class NotUnofficial : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            public bool CanShowItemDropInUI() => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            public string GetConditionDescription() => null;
        }

        public class UnofficialNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !Main.expertMode && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            public bool CanShowItemDropInUI() => !Main.expertMode && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial);
            public string GetConditionDescription() => Language.GetTextValue("Bestiary_ItemDropConditions.NotExpert");
        }

        public class Unreleased : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
            public string GetConditionDescription() => null;
        }

        public class NotUnreleased : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
            public bool CanShowItemDropInUI() => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
            public string GetConditionDescription() => null;
        }

        public class NotUnreleasedAndIsUnofficial : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
            public bool CanShowItemDropInUI() => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased);
            public string GetConditionDescription() => null;
        }

        public class Removed : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public string GetConditionDescription() => null;
        }

        public class NotRemoved : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public bool CanShowItemDropInUI() => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public string GetConditionDescription() => null;
        }

        public class RevOrMaster : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => Main.masterMode || CalamityMod.IsRevengance;
            public bool CanShowItemDropInUI() => Main.masterMode || CalamityMod.IsRevengance;
            public string GetConditionDescription() => CalamityMod.IsEnabled ? Language.GetTextValue("Mods.CalamityMod.Condition.RevOrMM") : Language.GetTextValue("Mods.AAModClassic.Common.Conditions.IsMaster");
        }

        public class PostLateAncientsAndRemovedWorld : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => AAWorld.downedAllAncients && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.PostLateAncientsAndRemovedWorld");
        }

        public class PostLateAncientsAndRemovedWorldAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => AAWorld.downedAllAncients && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && !Main.expertMode;
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && !Main.expertMode;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.PostLateAncientsAndRemovedWorld");
        }

        public class PostLateAncientsAndRemovedWorldAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => AAWorld.downedAllAncients && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && Main.expertMode;
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && Main.expertMode;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.PostLateAncientsAndRemovedWorld");
        }

        public class OneMechDefated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.OneMechDefeated");
        }
        public class SkeletronDefated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.SkeletronDefeated");
        }

        public class GolemDefated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedGolemBoss;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.GolemDefeated");
        }

        public class GoblinsDefated : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => NPC.downedGoblins;
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.GoblinsDefeated");
        }
    }
}
