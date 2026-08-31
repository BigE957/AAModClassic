using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Content.Void.World.Biomes;
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
        #region general conditions
        // biomes
        public static Condition InAnyInferno = new Condition("Mods.AAModClassic.Common.Conditions.InAnyInferno", () => Main.LocalPlayer.InModBiome<InfernoBiome>());
        public static Condition InAnyMire = new Condition("Mods.AAModClassic.Common.Conditions.InAnyMire", () => Main.LocalPlayer.InModBiome<MireBiome>());
        public static Condition InAnyVoid = new Condition("Mods.AAModClassic.Common.Conditions.InAnyVoid", () => Main.LocalPlayer.InModBiome<VoidBiome>());
        public static Condition InAnyRedMushroom = new Condition("Mods.AAModClassic.Common.Conditions.InAnyRedMushroom", () => Main.LocalPlayer.InModBiome<RedMushroomBiome>());

        // downeds 
        public static Condition DownedAnyLateAncient = new Condition("Mods.AAModClassic.Common.Conditions.DownedAnyLateAncient", () => AADowned.DownedAncient);
        public static Condition DownedAnySuperancient = new Condition("Mods.AAModClassic.Common.Conditions.DownedAnySuperancient", () => AADowned.DownedSAncient);

        public static Condition DownedBroodmother = new Condition("Mods.AAModClassic.Common.Conditions.DownedBroodmother", () => AADowned.downedBroodmother);
        public static Condition DownedHydra = new Condition("Mods.AAModClassic.Common.Conditions.DownedHydra", () => AADowned.downedHydra);

        // bullshit
        public static Condition LovecraftianQuestPurity = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Purity", () => AAWorld.squid1 >= 5);
        public static Condition LovecraftianQuestInferno = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Inferno", () => AAWorld.squid2 >= 5);
        public static Condition LovecraftianQuestMire = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Mire", () => AAWorld.squid3 >= 5);
        public static Condition LovecraftianQuestCorruption = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Corruption", () => AAWorld.squid4 >= 5);
        public static Condition LovecraftianQuestCrimson = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Crimson", () => AAWorld.squid5 >= 5);
        public static Condition LovecraftianQuestHallow = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Hallow", () => AAWorld.squid6 >= 5);
        public static Condition LovecraftianQuestVoid = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Void", () => AAWorld.squid7 >= 5);
        public static Condition LovecraftianQuestAntiMushroom = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.AntiMushroom", () => AAWorld.squid8 >= 5);
        public static Condition LovecraftianQuestRedMushroom = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.RedMushroom", () => AAWorld.squid9 >= 5);
        public static Condition LovecraftianQuestGlowingMushroom = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.GlowingMushroom", () => AAWorld.squid10 >= 5);
        public static Condition LovecraftianQuestJungle = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Jungle", () => AAWorld.squid11 >= 5);
        public static Condition LovecraftianQuestIce = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Ice", () => AAWorld.squid12 >= 1);
        public static Condition LovecraftianQuestAntiIce = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.AntiIce", () => AAWorld.squid12 >= 1);
        public static Condition LovecraftianQuestForest = new Condition("Mods.AAModClassic.Common.Conditions.LovecraftianQuest.Forest", () => AAWorld.squid13 >= 5);
        #endregion

        #region item drop rules
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
            public bool CanDrop(DropAttemptInfo info) => AADowned.DownedAllAncients && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed);
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.PostLateAncientsAndRemovedWorld");
        }

        public class PostLateAncientsAndRemovedWorldAndNotExpert : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => AADowned.DownedAllAncients && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && !Main.expertMode;
            public bool CanShowItemDropInUI() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && !Main.expertMode;
            public string GetConditionDescription() => Language.GetTextValue("Mods.AAModClassic.Common.Conditions.PostLateAncientsAndRemovedWorld");
        }

        public class PostLateAncientsAndRemovedWorldAndExpert : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => AADowned.DownedAllAncients && WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Removed) && Main.expertMode;
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
        #endregion
    }
}
