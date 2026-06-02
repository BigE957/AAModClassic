using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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
}
