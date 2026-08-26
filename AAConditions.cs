
global using static AAModClassic.AAConditions;
#region tooooooo many usings
using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.__BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs.__BossGreedA;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic._Content.Void.___PreHardmode.NPCs.__BossSagittarius;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._CrossMod.CalamityMod;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRetriever;
using AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
#endregion
namespace AAModClassic
{
    public class AAConditions
    {
        #region general conditions
        // biomes
        public static Condition InAnyInferno = new Condition("Mods.AAModClassic.Common.Conditions.InAnyInferno", () => Main.LocalPlayer.InModBiome<InfernoBiome>());
        public static Condition InAnyMire = new Condition("Mods.AAModClassic.Common.Conditions.InAnyMire", () => Main.LocalPlayer.InModBiome<MireBiome>());
        public static Condition InAnyVoid = new Condition("Mods.AAModClassic.Common.Conditions.InAnyVoid", () => Main.LocalPlayer.InModBiome<VoidBiome>());
        public static Condition InAnyRedMushroom = new Condition("Mods.AAModClassic.Common.Conditions.InAnyRedMushroom", () => Main.LocalPlayer.InModBiome<RedMushroomBiome>());

        #region downeds
        public static Condition downedMushroomMonarch = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.MushroomMonarch", () => NPCExtensions.BeenKilled<MushroomMonarch>());
        public static Condition downedFeudalFungus = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.FeudalFungus", () => NPCExtensions.BeenKilled<FeudalFungus>());
        public static Condition downedGripsOfChaos = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.GripsOfChaos", () => AAWorld.downedGrips);

        public static Condition downedTruffleToad = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.TruffleToad", () => NPCExtensions.BeenKilled<TruffleToad>());
        public static Condition downedBroodmother = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Broodmother", () => NPCExtensions.BeenKilled<Broodmother>());
        public static Condition downedHydra = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Hydra", () => NPCExtensions.BeenKilled<HydraBody>());

        public static Condition downedSubzeroSerpent = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.SubzeroSerpent", () => NPCExtensions.BeenKilled<SubzeroSerpentHead>());
        public static Condition downedDesertDjinn = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.DesertDjinn", () => NPCExtensions.BeenKilled<DesertDjinn>());
        public static Condition downedSagittarius = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Sagittarius", () => NPCExtensions.BeenKilled<Sagittarius>());

        public static Condition downedTechnoTruffle = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.TechnoTruffle", () => NPCExtensions.BeenKilled<TechnoTruffle>());
        public static Condition downedRetriever = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Retriever", () => NPCExtensions.BeenKilled<Retriever>());
        public static Condition downedOrthrusX = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.OrthrusX", () => NPCExtensions.BeenKilled<OrthrusXBody>());

        public static Condition downedRaiderUltima = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.RaiderUltima", () => NPCExtensions.BeenKilled<RaiderUltima>());
        public static Condition downedAnubis = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Anubis", () => NPCExtensions.BeenKilled<Anubis>());
        public static Condition downedBiomiteCore = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.BiomiteCore", () => NPCExtensions.BeenKilled<BiomiteCore>());

        public static Condition downedAthena = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Athena", () => NPCExtensions.BeenKilled<Athena>());
        public static Condition downedGreed = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Greed", () => NPCExtensions.BeenKilled<GreedHead>());
        public static Condition downedRajahRabbit = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.RajahRabbit", () => NPCExtensions.BeenKilled<RajahRabbit>());

        public static Condition downedForsakenAnubis = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.ForsakenAnubis", () => NPCExtensions.BeenKilled<AnubisA>());
        public static Condition downedAthenaA = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.AthenaA", () => NPCExtensions.BeenKilled<AthenaA>());
        public static Condition downedGreedA = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.GreedA", () => NPCExtensions.BeenKilled<GreedAHead>());

        public static Condition downedEquinoxWorms = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.EquinoxWorms", () => AAWorld.downedEquinox);
        public static Condition downedSistersOfDiscord = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.SistersOfDiscord", () => AAWorld.downedSisters);
        public static Condition downedAkuma = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Akuma", () => AAWorld.downedAkuma);

        public static Condition downedAkumaA = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.AkumaA", () => AAWorld.downedAkuma);
        public static Condition downedYamata = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Yamata", () => AAWorld.downedYamata);
        public static Condition downedYamataA = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.YamataA", () => AAWorld.downedYamata);

        public static Condition downedZero = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Zero", () => AAWorld.downedZero);
        public static Condition downedZeroP = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.ZeroP", () => AAWorld.downedZero);
        public static Condition downedRajahRabbitR = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.RajahRabbitR", () => NPCExtensions.BeenKilled<RajahRabbitA>());

        public static Condition downedShen = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Shen", () => AAWorld.downedShen);
        public static Condition downedShenA = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.ShenA", () => AAWorld.downedShen);
        public static Condition downedInfinityZero = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.InfinityZero", () => NPCExtensions.BeenKilled<InfinityZero>());

        public static Condition downedSoulOfCthulhu = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.SoulOfCthulhu", () => NPCExtensions.BeenKilled<SoulOfCthulhu>());
        public static Condition downedCthulhu = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.Cthulhu", () => NPCExtensions.BeenKilled<SoulOfCthulhu>());

        public static Condition DownedAnyLateAncient = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.AnyLateAncient", () => AAWorld.downedAncient);
        public static Condition DownedAnySuperancient = new Condition("Mods.AAModClassic.Common.Conditions.BossDowned.AnySuperancient", () => AAWorld.downedSAncient);
        #endregion

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
        #endregion
    }
}
