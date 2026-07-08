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

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public class AAConditions
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
    }
}
