using AAModClassic._Content.Acropolis.__Hardmode.NPCs._BossAthena;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs._BossAthenaA;
using AAModClassic._Content.Bunny.__Hardmode.NPCs._BossRajah;
using AAModClassic._Content.Chaos.___PreHardmode.NPCs.__BossGripsOfChaos;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossShen;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossSisters.Ashe;
using AAModClassic._Content.Desert.___PreHardmode.NPCs.__BossDesertDjinn;
using AAModClassic._Content.Desert.__Hardmode._BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord._BossAnubisA;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus;
using AAModClassic._Content.Hoard.__Hardmode.NPCs._BossGreed;
using AAModClassic._Content.Hoard._PostMoonlord.NPCs._BossGreedA;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs.__BossMushroomMonarch;
using AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent;
using AAModClassic._Content.Stars._PostMoonlord.NPCs._BossEquinox;
using AAModClassic._Content.Void.___PreHardmode.NPCs._BossSagittarius;
using AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero;
using AAModClassic._Unofficial.Desert;
using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Globals;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unofficial
{
    public class QuestSystem : ModSystem
    {
        public static Dictionary<string, Questline> Questlines = [];

        public override void OnModLoad()
        {
            On_Main.UpdateTime_StartDay += RefreshQuests;
        }

        public static void RefreshQuests(On_Main.orig_UpdateTime_StartDay orig, ref bool stopEvents)
        {
            // At the start of each day, reset completed quests
            foreach (Questline questline in Questlines.Values)
                foreach (string ID in questline.UnlockedQuests)
                {
                    Quest quest = questline.Quests[ID];
                    if (quest.IsRepeatable && quest.IsTurnedIn)
                    {
                        quest.IsTurnedIn = false;
                        quest.RefreshQuest();
                    }
                }
            orig(ref stopEvents);
        }

        public static void InitializeAllQuestlines()
        {
            #region Legendscribe Earlygame Questline
            string legendscribeQuestID = "LegendscribeEarlyGame";
            AddQuestline(legendscribeQuestID, "Legendscribe");

            Questlines[legendscribeQuestID].AddQuest(
                "MushroomMonarch",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<MushroomMonarch>(), false)
                ],
                ["GripsOfChaos"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<MushroomMonarch>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "FeudalFungus",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<FeudalFungus>(), false)
                ],
                ["GripsOfChaos"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<FeudalFungus>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "GripsOfChaos",
                [
                    new FlagObjective(() => AAWorld.downedGrips, false)
                ],
                ["Broodmother", "Hydra"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<GripOfChaosInferno>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Broodmother",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Broodmother>(), false)
                ],
                ["SubzeroSerpent", "DesertDjinn"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Broodmother>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Hydra",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<HydraBody>(), false)
                ],
                ["SubzeroSerpent", "DesertDjinn"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<HydraHead1>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "DesertDjinn",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<DesertDjinn>(), false)
                ],
                ["Sagittarius"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<DesertDjinn>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "SubzeroSerpent",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<SubzeroSerpent_Head>(), false)
                ],
                ["Sagittarius"],
                ModContent.Request<Texture2D>("AAModClassic/_Content/Snow/___PreHardmode/NPCs/__BossSubzeroSerpent/BossTextures/Default/SubzeroSerpent_Head_Boss"),
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Sagittarius",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Sag>(), false)
                ],
                ["Anubis"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Sag>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Anubis",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Anubis>(), false)
                ],
                ["Athena", "Greed"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AnubisUnreleased>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Athena",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Athena>(), false)
                ],
                ["Rajah"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Athena>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Greed",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Greed>(), false)
                ],
                ["Rajah"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Greed>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Rajah",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Rajah>(), false)
                ],
                [/*"ForsakenAnubis"*/],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Rajah>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].UnlockedQuests.Add("MushroomMonarch");
            Questlines[legendscribeQuestID].UnlockedQuests.Add("FeudalFungus");

            foreach (Quest quest in Questlines[legendscribeQuestID].Quests.Values)
            {
                foreach (string id in quest.QuestUnlocks)
                    Questlines[legendscribeQuestID].Quests[id].QuestRequirements.Add(quest.ID);
            }

            LegendscribeQuestUISystem.questUI.OnInitialize();
            LegendscribeQuestUISystem.questUI.OnActivate();
            #endregion

            #region Legendscribe Lategame Questline
            legendscribeQuestID = "LegendscribeLateGame";
            AddQuestline(legendscribeQuestID, "Legendscribe");

            Questlines[legendscribeQuestID].AddQuest(
                "ForsakenAnubis",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<ForsakenAnubis>(), false)
                ],
                ["GreedA", "AthenaA"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<ForsakenAnubis>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "AthenaA",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<AthenaA>(), false)
                ],
                ["Equinox"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AthenaA>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "GreedA",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<GreedA>(), false)
                ],
                ["Equinox"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<GreedA>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Equinox",
                [
                    new FlagObjective(() => AAWorld.downedEquinox, false)
                ],
                ["Sisters"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<DaybringerHead>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Sisters",
                [
                    new FlagObjective(() => AAWorld.downedSisters, false)
                ],
                ["Akuma", "Yamata", "Zero"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Ashe>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Akuma",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Akuma>(), false)
                ],
                ["Shen", "SoulOfCthulhu"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Akuma>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Yamata",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<YamataBody>(), false)
                ],
                ["Shen", "SoulOfCthulhu"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<YamataHead>()]], 
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Zero",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Zero>(), false)
                ],
                ["InfinityZero", "SoulOfCthulhu"],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Zero>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "Shen",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<Shen>(), false)
                ],
                [],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Shen>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "InfinityZero",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<InfinityZero>(), false)
                ],
                [],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<InfinityZero>()]],
                hasCompleteText: true
            );

            Questlines[legendscribeQuestID].AddQuest(
                "SoulOfCthulhu",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<SoulOfCthulhu>(), false)
                ],
                [],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<SoulOfCthulhu>()]],
                hasCompleteText: true
            );

            //Questlines[legendscribeQuestID].UnlockedQuests.Add("ForsakenAnubis");

            foreach (Quest quest in Questlines[legendscribeQuestID].Quests.Values)
            {
                foreach (string id in quest.QuestUnlocks)
                    Questlines[legendscribeQuestID].Quests[id].QuestRequirements.Add(quest.ID);
            }
            #endregion
        }

        public override void PostSetupContent()
        {
            InitializeAllQuestlines();
        }

        public override void PostWorldLoad()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && !Questlines["LegendscribeLateGame"].Quests.ContainsKey("InfinityZero"))
            {
                Questlines["LegendscribeLateGame"].AddQuest(
                "InfinityZero",
                [
                    new FlagObjective(() => NPCExtensions.BeenKilled<InfinityZero>(), false)
                ],
                [],
                TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<InfinityZero>()]],
                hasCompleteText: true
            );

                Questlines["LegendscribeLateGame"].AddQuest(
                    "SoulOfCthulhu",
                    [
                        new FlagObjective(() => NPCExtensions.BeenKilled<SoulOfCthulhu>(), false)
                    ],
                    [],
                    TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<SoulOfCthulhu>()]],
                    hasCompleteText: true
                );
            }
            else if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) && Questlines["LegendscribeLateGame"].Quests.ContainsKey("InfinityZero"))
            {
                Questlines["LegendscribeLateGame"].Quests.Remove("InfinityZero");
                Questlines["LegendscribeLateGame"].Quests.Remove("SoulOfCthulhu");
            }

            LegendscribeQuestUISystem.questUI.OnInitialize();
            LegendscribeQuestUISystem.questUI.OnActivate();
        }

        public override void PostUpdateEverything()
        {
            foreach (Questline questline in Questlines.Values.Where(ql => ql.Started))
                foreach (Quest quest in questline.Quests.Values.Where(q => q.Active))
                    foreach (QuestObjective task in quest.Objectives.Where(t => !t.IsComplete || t.TrackPastCompletion))
                    {
                        switch (task)
                        {
                            case ItemObjective itemObj:
                                int itemCount = 0;
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    foreach (int type in itemObj.ItemTypes)
                                    {
                                        itemCount += Main.LocalPlayer.CountItem(type, itemObj.MaxProgress);
                                        if (itemCount >= itemObj.MaxProgress)
                                            break;
                                    }
                                }
                                else
                                    foreach (Player p in Main.ActivePlayers)
                                    {
                                        foreach (int type in itemObj.ItemTypes)
                                        {
                                            itemCount += p.CountItem(type, itemObj.MaxProgress);
                                            if (itemCount >= itemObj.MaxProgress)
                                                break;
                                        }
                                        if (itemCount >= itemObj.MaxProgress)
                                            break;
                                    }

                                itemCount = Math.Min(itemCount, itemObj.MaxProgress);
                                if (itemObj.TrackPastCompletion)
                                {
                                    if (itemObj.Progress != itemCount)
                                        itemObj.AddProgress(itemCount, true);
                                }
                                else if (itemCount > itemObj.Progress)
                                    itemObj.AddProgress(itemCount, true);
                                break;
                            case FlagObjective flag:
                                if (flag.CompleteCondition.Invoke())
                                    task.AddProgress(1, true);
                                else if (flag.TrackPastCompletion)
                                    task.AddProgress(0, true);
                                break;
                            case PlayerFlagObjective pFlagObj:
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    if (pFlagObj.CompleteCondition.Invoke(Main.LocalPlayer))
                                        task.AddProgress(1, true);
                                    else if (pFlagObj.TrackPastCompletion)
                                        task.AddProgress(0, true);
                                }
                                else
                                {
                                    bool cleared = false;

                                    foreach (Player p in Main.ActivePlayers)
                                    {
                                        if (pFlagObj.CompleteCondition.Invoke(p))
                                            cleared = true;
                                    }

                                    if (cleared)
                                        task.AddProgress(1, true);
                                    else if (pFlagObj.TrackPastCompletion)
                                        task.AddProgress(0, true);
                                }
                                break;
                        }
                    }
        }

        public override void ClearWorld()
        {
            foreach (Questline questline in Questlines.Values)
            {
                foreach (Quest quest in questline.Quests.Values)
                    quest.ClearQuest();
                questline.UnlockedQuests.Clear();
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            foreach (Questline questline in Questlines.Values)
                foreach (Quest quest in questline.Quests.Values)
                {
                    writer.Write(quest.IsTurnedIn);
                    writer.Write(quest.EverTurnedIn);
                    foreach (QuestObjective obj in quest.Objectives)
                        writer.Write(obj.Progress);
                    writer.Write(quest.Active);
                }
        }

        public override void NetReceive(BinaryReader reader)
        {
            foreach (Questline questline in Questlines.Values)
                foreach (Quest quest in questline.Quests.Values)
                {
                    quest.IsTurnedIn = reader.ReadBoolean();
                    quest.EverTurnedIn = reader.ReadBoolean();
                    foreach (QuestObjective obj in quest.Objectives)
                        obj.AddProgress(reader.ReadInt32(), true, true);

                    quest.Active = reader.ReadBoolean();
                }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            base.LoadWorldData(tag);
            foreach (Questline questline in Questlines.Values)
                foreach (Quest quest in questline.Quests.Values)
                {
                    string questkey = questline.ID + quest.ID;
                    foreach (QuestObjective obj in quest.Objectives)
                    {
                        string intKey = questkey + "ProgressionInt" + obj.Index.ToString();

                        if (tag.ContainsKey(intKey))
                        {
                            obj.AddProgress(tag.GetInt(intKey), true, true);
                        }
                    }
                    string startkey = questkey + "Started";
                    if (tag.ContainsKey(startkey))
                    {
                        quest.Active = tag.GetBool(startkey);
                    }
                    string endkey = questkey + "Ended";
                    if (tag.ContainsKey(endkey))
                    {
                        quest.IsTurnedIn = tag.GetBool(endkey);
                    }
                    string compKey = questkey + "CompletedBefore";
                    if (tag.ContainsKey(compKey))
                    {
                        quest.EverTurnedIn = tag.GetBool(compKey);
                    }
                }

            //Quest Unlocks
            foreach (Questline questline in Questlines.Values)
                foreach (Quest quest in questline.Quests.Values)
                {
                    bool canUnlock = true;
                    foreach (string gate in quest.QuestRequirements)
                    {
                        if (!questline.Quests[gate].EverTurnedIn)
                            canUnlock = false;
                    }
                    if (canUnlock)
                        questline.UnlockedQuests.Add(quest.ID);
                }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);
            foreach (Questline questline in Questlines.Values)
                foreach (Quest quest in questline.Quests.Values)
                {
                    string questkey = questline.ID + quest.ID;
                    foreach (QuestObjective obj in quest.Objectives)
                    {
                        string intKey = questkey + "ProgressionInt" + obj.Index.ToString();
                        if (!tag.ContainsKey(intKey))
                        {
                            tag.Add(intKey, obj.Progress);
                        }
                        else
                        {
                            tag[intKey] = obj.Progress;
                        }
                    }
                    string startkey = questkey + "Started";
                    if (!tag.ContainsKey(startkey))
                    {
                        tag.Add(startkey, quest.Active);
                    }
                    string endkey = questkey + "Ended";
                    if (!tag.ContainsKey(endkey))
                    {
                        tag.Add(endkey, quest.IsTurnedIn);
                    }
                    string compKey = questkey + "CompletedBefore";
                    if (!tag.ContainsKey(compKey))
                    {
                        tag.Add(compKey, quest.EverTurnedIn);
                    }
                }
        }

        public static void AddQuestline(string id, string vendor)
        {
            Questlines.Add(id, new(id, vendor));
        }

        public static Quest GetQuest(string questline, string id) => Questlines[questline].Quests[id];
    }

    public class Questline(string id, string vendor)
    {
        public readonly string ID = id;

        public readonly Dictionary<string, Quest> Quests = [];

        public readonly List<string> UnlockedQuests = [];

        public readonly string Vendor = vendor;

        public bool Started = false;

        public void AddQuest(string id, List<QuestObjective> objectives, string[] gates, Asset<Texture2D> icon, bool autoStart = false, List<Item> rewards = null, List<Item> repeatRewards = null, bool hasCompleteText = false)
        {
            string path = "Mods.AAModClassic.UI.Quests." + ID + "." + id + ".";
            Quests.Add(id, new Quest(ID, id, Language.GetText(path + "Name"), Language.GetText(path + (hasCompleteText ? "Description.Incomplete" : "Description")), Language.GetText(path + "Tasks"), objectives, gates, icon, autoStart, rewards, repeatRewards, Language.GetText(path + "ExtraRewards"), hasCompleteText ? Language.GetText(path + "Description.Complete") : null));
        }
    }

    public class Quest
    {
        #region Text
        /// <summary>
        /// The name of the quest
        /// </summary>
        public LocalizedText Name;
        /// <summary>
        /// The description of the quest
        /// </summary>
        public LocalizedText DescriptionIncomplete;
        /// <summary>
        /// The description of the quest
        /// </summary>
        public LocalizedText DescriptionComplete;
        /// <summary>
        /// The tasks section
        /// </summary>
        public LocalizedText Tasks;
        /// <summary>
        /// Text for extra rewards such as recipe unlocks.
        /// </summary>
        public LocalizedText ExtraRewardDesc;
        #endregion

        #region Identification
        public readonly string QuestLine;

        public readonly string ID;

        public Asset<Texture2D> Icon;
        #endregion

        #region Completion Values
        /// <summary>
        /// Is this quest currently active?
        /// </summary>
        public bool Active;

        /// <summary>
        /// The tasks that must be completed to finish the quest
        /// </summary>
        public readonly List<QuestObjective> Objectives;

        public bool AutoCompletes => Objectives.Count == 0;

        /// <summary>
        /// Has this quest ever been completed
        /// </summary>
        public bool EverTurnedIn = false;

        /// <summary>
        /// Is this quest currently marked as completed
        /// </summary>
        public bool IsTurnedIn = false;

        /// <summary>
        /// Has this quest progressed to completion?
        /// </summary>
        /// <returns></returns>
        public bool IsComplete
        {
            get
            {
                if (!Active)
                    return false;

                if (Objectives == null || Objectives.Count == 0)
                    return true;

                foreach (QuestObjective task in Objectives)
                {
                    if (!task.IsComplete)
                        return false;
                }
                return true;
            }
        }
        #endregion

        #region Rewards
        /// <summary>
        /// The items gained from this quest
        /// </summary>
        public List<Item> Rewards;
        /// <summary>
        /// The items gained from this quest after the first time
        /// </summary>
        public List<Item> RepeatRewards;

        /// </summary>
        /// All the quests which completing this quest unlocks.
        /// </summary>
        public string[] QuestUnlocks;
        /// <summary>
        /// All the quests which completing will unlock this quest.
        /// </summary>
        public List<string> QuestRequirements = [];

        public bool IsRepeatable => Rewards != null;
        #endregion

        internal Quest(string questline, string id, LocalizedText name, LocalizedText description, LocalizedText tasks, List<QuestObjective> objectives, string[] gates, Asset<Texture2D> icon, bool autoStart = false, List<Item> rewards = null, List<Item> repeatRewards = null, LocalizedText extraRewards = null, LocalizedText descriptionComplete = null)
        {
            Name = name;
            DescriptionIncomplete = description;
            DescriptionComplete = descriptionComplete ?? description;
            Tasks = tasks;
            ExtraRewardDesc = extraRewards;
            QuestLine = questline;
            ID = id;
            Icon = icon;
            Active = autoStart;
            Objectives = objectives;
            Rewards = rewards;
            RepeatRewards = repeatRewards;
            QuestUnlocks = gates;

            for (int i = 0; i < Objectives.Count; i++)
            {
                Objectives[i].Questline = questline;
                Objectives[i].QuestID = ID;
                Objectives[i].Index = i;
            }
        }

        public void ClearQuest()
        {
            Active = false;
            EverTurnedIn = false;
            IsTurnedIn = false;

            foreach (QuestObjective task in Objectives)
                task.ResetProgress();
        }

        public void StartQuest()
        {
            Active = true;
            IsTurnedIn = false;

            foreach (QuestObjective task in Objectives)
                task.ResetProgress();
        }

        public void RefreshQuest()
        {
            IsTurnedIn = false;

            foreach (QuestObjective task in Objectives)
                task.ResetProgress();
        }
    }

    public enum ObjectiveProgressDisplay
    {
        None,
        Ratio,
        Percentage
    }

    public abstract class QuestObjective
    {
        internal string Questline;

        internal string QuestID;

        internal int Index;

        private int progress = 0;

        internal int MaxProgress;

        internal bool TrackPastCompletion;

        internal bool SyncProgress;

        internal ObjectiveProgressDisplay ProgressDisplay;

        public int Progress => progress;

        public bool IsComplete => Progress >= MaxProgress;

        public float CompletionProgress => Progress / (float)MaxProgress;

        public string CompletionPercentage => "(" + ((int)(CompletionProgress * 100)) + "%)";

        public string CompletionRatio => "(" + Progress + "/" + MaxProgress + ")";

        public void AddProgress(int amount = 1, bool setValue = false, bool fromPacket = false)
        {
            bool wasComplete = IsComplete;

            if (setValue)
            {
                if (progress == amount)
                    return;

                progress = amount;
            }
            else
                progress += amount;

            if (!wasComplete && IsComplete)
                CombatText.NewText(Main.LocalPlayer.getRect(), Color.Cyan, Language.GetTextValue("Mods.AAModClassic.Common.QuestComplete"), true);

            if (SyncProgress && Main.netMode == NetmodeID.MultiplayerClient && !fromPacket)
                AANet.SendNetMessage<QuestProgressionPacket>(Questline, QuestID, Index, progress);

        }

        internal void ResetProgress()
        {
            progress = 0;
        }
    }

    #region Objective Types
    public class KillObjective : QuestObjective
    {
        internal readonly int NPCType;

        public KillObjective(int npcToKill, int amountToKill, ObjectiveProgressDisplay progressDisplay = ObjectiveProgressDisplay.Ratio)
        {
            NPCType = npcToKill;
            MaxProgress = amountToKill;
            TrackPastCompletion = false;
            SyncProgress = true;
            ProgressDisplay = progressDisplay;
        }
    }

    public class FlagObjective : QuestObjective
    {
        internal Func<bool> CompleteCondition;

        public FlagObjective(Func<bool> condition, bool mustAlwaysBeTrue, ObjectiveProgressDisplay progressDisplay = ObjectiveProgressDisplay.None)
        {
            CompleteCondition = condition;
            MaxProgress = 1;
            TrackPastCompletion = mustAlwaysBeTrue;
            SyncProgress = false;
            ProgressDisplay = progressDisplay;
        }
    }

    public class PlayerFlagObjective : QuestObjective
    {
        internal Func<Player, bool> CompleteCondition;

        public PlayerFlagObjective(Func<Player, bool> condition, bool mustAlwaysBeTrue, ObjectiveProgressDisplay progressDisplay = ObjectiveProgressDisplay.None)
        {
            CompleteCondition = condition;
            MaxProgress = 1;
            TrackPastCompletion = mustAlwaysBeTrue;
            SyncProgress = false;
            ProgressDisplay = progressDisplay;
        }
    }

    public class ItemObjective : QuestObjective
    {
        internal int[] ItemTypes;

        public ItemObjective(int itemType, int amountNeeded, bool mustAlwaysBeTrue = true, ObjectiveProgressDisplay progressDisplay = ObjectiveProgressDisplay.Ratio)
        {
            ItemTypes = [itemType];
            MaxProgress = amountNeeded;
            TrackPastCompletion = mustAlwaysBeTrue;
            SyncProgress = false;
            ProgressDisplay = progressDisplay;
        }

        public ItemObjective(int[] itemTypes, int amountNeeded, bool mustAlwaysBeTrue = true, ObjectiveProgressDisplay progressDisplay = ObjectiveProgressDisplay.Ratio)
        {
            ItemTypes = itemTypes;
            MaxProgress = amountNeeded;
            TrackPastCompletion = mustAlwaysBeTrue;
            SyncProgress = false;
            ProgressDisplay = progressDisplay;
        }
    }

    public class InvokedObjective : QuestObjective
    {
        public delegate void OnObjectiveProgress(int progressAmount, bool setProgress);

        private void Invoked(int progressAmount, bool setProgress)
        {
            AddProgress(progressAmount, setProgress);
        }

        public InvokedObjective(ref OnObjectiveProgress p, int amountNeeded, ObjectiveProgressDisplay progressDisplay = ObjectiveProgressDisplay.Percentage)
        {
            p += Invoked;
            MaxProgress = amountNeeded;
            TrackPastCompletion = false;
            SyncProgress = true;
            ProgressDisplay = progressDisplay;
        }
    }
    #endregion
}
