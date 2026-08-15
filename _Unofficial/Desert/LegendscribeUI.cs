using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA;
using AAModClassic._CrossMod;
using AAModClassic.Globals;
using AAModClassic.UI.Core;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static AAModClassic._Unofficial.Desert.UIParallaxBackground;

namespace AAModClassic._Unofficial.Desert
{
    public class LegendscribeQuestUI : UIState
    {
        /// <summary>
        /// The current subquest to display
        /// </summary>
        private static string CurrentQuestID = "";

        private static bool InEarlygameState = true;

        internal static Questline CurrentQuestline => AADowned.downedForsakenAnubis ? QuestSystem.Questlines["LegendscribeLateGame"] : QuestSystem.Questlines["LegendscribeEarlyGame"];
        internal static Quest CurrentQuest => CurrentQuestID == "" ? null : CurrentQuestline.Quests[CurrentQuestID];
        internal static UIPanel Area;

        //Node List Side
        internal static UIPanel QuestListArea;

        internal static UIHorizontalList QuestList;
        internal static HorizontalUIScrollbar QuestListScrollBar;
        internal static UIParallaxBackground QuestListBackground;

        internal static Dictionary<string, int> DepthValues;
        internal static Dictionary<string, QuestNode> Nodes;
        internal static List<(string NodeKey1, string NodeKey2, FrameableUIImage line)> Lines;

        //Details Side
        internal static UIPanel QuestDetailsArea;

        internal static UITextPanel<string> QuestTitle;
        internal static UIPanel QuestDescriptionArea;
        internal static UIText QuestDescriptionText;
        internal static UIList QuestDescriptionList;
        internal static UIScrollbar QuestDescriptionScrollBar;

        internal static UITextPanel<string> QuestTasksHeader;
        internal static UIPanel QuestTasksArea;
        internal static UIText QuestTasksText;
        internal static UIList QuestTasksList;
        internal static UIScrollbar QuestTasksScrollBar;

        /*
        internal static UITextPanel<string> QuestRewardsHeader;
        internal static UIPanel QuestRewardsArea;
        internal static List<UIPanel> QuestRewards;
        */

        internal static UIPanel MouseTextPanel;
        internal static UIText MouseText;

        public override void OnInitialize()
        {
            if (!QuestSystem.Questlines.ContainsKey("LegendscribeEarlyGame"))
                return;

            RemoveAllChildren();

            Color outerBorder;
            Color outerBack;

            Color innerBorder;
            Color innerBack;

            Color labelBack;

            if (CurrentQuestline.ID == "LegendscribeEarlyGame")
            {
                outerBorder = Color.Black;
                outerBack = Color.DarkGoldenrod;
                innerBorder = Color.Cyan;
                innerBack = Color.Black;
                labelBack = Color.Gold;

                if(ContentReplacementSystem.NeedToReplaceContent)
                    CurrentQuestID = "GripsOfChaos";
                else
                    CurrentQuestID = "MushroomMonarch";

                InEarlygameState = true;
            }
            else
            {
                outerBorder = Color.Black;
                outerBack = Color.DarkOliveGreen;
                innerBorder = Color.Lime;
                innerBack = Color.Black;
                labelBack = Color.YellowGreen;

                CurrentQuestID = "ForsakenAnubis";

                InEarlygameState = false;
            }


            Area = new()
            {
                HAlign = 0.5f,
                VAlign = 0.33f,
                BackgroundColor = outerBack,
                BorderColor = outerBorder
            };
            Area.Width.Pixels = 880;
            Area.Height.Pixels = 630;

            QuestListArea = new()
            {
                BackgroundColor = innerBack,
                BorderColor = innerBorder,
                VAlign = 0.99f,
                HAlign = 0.5f
            };
            QuestListArea.Width.Pixels = 840;
            QuestListArea.Height.Pixels = 295;
            Area.Append(QuestListArea);

            #region Quest Node Map
            QuestListScrollBar = new()
            {
                VAlign = 1f,
                HAlign = 0.5f,
            };
            QuestListScrollBar.MaxWidth.Pixels = QuestListScrollBar.Width.Pixels = 824;
            QuestListScrollBar.Height.Pixels = 20;
            Area.Append(QuestListScrollBar);

            QuestList = [];
            QuestList.Width.Pixels = 840;
            QuestList.Height.Pixels = 295;

            Main.instance.LoadBackground(0);
            Asset<Texture2D> tex = CurrentQuestline.ID == "LegendscribeEarlyGame" ? TextureAssets.Background[0] : TextureAssets.BlackTile;
            Main.instance.LoadBackground(22);
            Asset<Texture2D> farBack = TextureAssets.Background[22];
            Main.instance.LoadBackground(208);
            Asset<Texture2D> mediumBack = TextureAssets.Background[208];
            Main.instance.LoadBackground(218);
            Asset<Texture2D> nearBack = TextureAssets.Background[218];

            Color forsakenTint = Color.Lerp(Color.White, new(0.2f, 0.5f, 0.2f), 0.5f);

            Color baseColor = CurrentQuestline.ID == "LegendscribeEarlyGame" ? Color.White : forsakenTint;

            QuestListBackground = new(tex, 
            [
                new(farBack, 0.2f, new(0,-30), baseColor * 0.666f), 
                new(farBack, 0.25f, new(56, 10), baseColor), 
                new(mediumBack, 0.4f, new(0, 40), baseColor), 
                new(nearBack, 1f, new(0, 110), baseColor)
            ], 0.5f);
            QuestListBackground.MaxWidth.Pixels = QuestListBackground.Width.Pixels = 3000;
            QuestListBackground.Height.Pixels = QuestList.Height.Pixels;
            QuestList.Add(QuestListBackground);

            QuestList.SetScrollbar(QuestListScrollBar);
            QuestListArea.Append(QuestList);

            DepthValues = [];
            Nodes = [];
            Lines = [];
            #endregion

            QuestDetailsArea = new()
            {
                BackgroundColor = innerBack,
                BorderColor = innerBorder,
                VAlign = 0.01f,
                HAlign = 0.5f
            };
            QuestDetailsArea.Width.Pixels = 840;
            QuestDetailsArea.Height.Pixels = 295;

            #region Quest Title
            QuestTitle = new(CurrentQuest == null ? "Hi" : CurrentQuest.Name.Value, 0.66f, true)
            {
                HAlign = 0.5f,
                BackgroundColor = labelBack
            };

            QuestTitle.Left.Pixels = -222;

            QuestTitle.Width.Pixels = 300;
            QuestTitle.Height.Pixels = 40;
            QuestDetailsArea.Append(QuestTitle);
            #endregion

            #region Quest Description
            float descWidth = 320;
            var wrapped = FontAssets.MouseText.Value.CreateWrappedText(CurrentQuest == null ? "Mushroom Monarch" : CurrentQuest.DescriptionIncomplete.Format(Main.LocalPlayer.name), descWidth - 40);

            QuestDescriptionText = new(wrapped);
            QuestDescriptionText.Width.Pixels = descWidth - 20;
            QuestDescriptionText.Height.Pixels = FontAssets.MouseText.Value.MeasureString(QuestDescriptionText.Text).Y;

            QuestDescriptionArea = new()
            {
                HAlign = 0.05f,
                BackgroundColor = outerBack,
                BorderColor = outerBorder,
            };
            QuestDescriptionArea.Width.Pixels = descWidth + 10;
            QuestDescriptionArea.Height.Pixels = 190;
            QuestDescriptionArea.Top.Pixels = 52;

            QuestDescriptionList = new();
            QuestDescriptionList.PaddingTop = QuestDescriptionList.PaddingRight = 0f;
            QuestDescriptionList.Width.Pixels = descWidth - 10;
            QuestDescriptionList.Height.Pixels = QuestDescriptionArea.Height.Pixels;

            QuestDescriptionScrollBar = new()
            {
                HAlign = 1f,
                VAlign = 0.5f
            };
            QuestDescriptionScrollBar.Left.Pixels = 12;
            QuestDescriptionScrollBar.Width.Pixels = 20;
            QuestDescriptionScrollBar.Height.Pixels = 180;

            QuestDescriptionList.SetScrollbar(QuestDescriptionScrollBar);
            QuestDescriptionList.Add(QuestDescriptionText);
            QuestDescriptionArea.Append(QuestDescriptionScrollBar);
            QuestDescriptionArea.Append(QuestDescriptionList);

            QuestDetailsArea.Append(QuestDescriptionArea);
            #endregion

            #region Quest Tasks Header
            QuestTasksHeader = new("Tasks", 0.66f, true)
            {
                HAlign = 0.95f,
                BackgroundColor = labelBack,
                MarginRight = 10
            };

            QuestTasksHeader.Width.Pixels = 300;
            QuestTasksHeader.Height.Pixels = 40;
            QuestTasksHeader.Top.Pixels = 20;
            QuestDetailsArea.Append(QuestTasksHeader);
            #endregion

            #region Quest Tasks
            wrapped = FontAssets.MouseText.Value.CreateWrappedText(CurrentQuest == null ? "Greetings" : CurrentQuest.Tasks.Value, 320 - 40);
            List<int> insertIndexes = [];
            for (var i = 0; i < wrapped.Length; i++)
                if (i != wrapped.Length - 1 && wrapped[i] == '\n' && wrapped[i + 1] != '-')
                    insertIndexes.Add(i + 1 + insertIndexes.Count * 3);

            foreach (var index in insertIndexes)
                wrapped = wrapped.Insert(index, "   ");

            QuestTasksText = new(wrapped);
            QuestTasksText.TextOriginX = 0f;
            QuestTasksText.Width.Pixels = descWidth - 40;
            QuestTasksText.Height.Pixels = 100;// FontAssets.MouseText.Value.MeasureString(QuestTasksText.Text).Y;

            QuestTasksArea = new()
            {
                HAlign = 0.95f,
                BackgroundColor = outerBack,
                BorderColor = outerBorder,
            };
            QuestTasksArea.Width.Pixels = descWidth;
            QuestTasksArea.Height.Pixels = 60;
            QuestTasksArea.Top.Pixels = 5 + QuestTasksHeader.Top.Pixels + QuestTasksHeader.Height.Pixels;

            QuestTasksList = new();
            QuestTasksList.Width.Pixels = descWidth - 40;
            QuestTasksList.Height.Pixels = QuestTasksArea.Height.Pixels;

            QuestTasksScrollBar = new()
            {
                HAlign = 1f,
                VAlign = 0.5f
            };
            QuestTasksScrollBar.Left.Pixels = 12;
            QuestTasksScrollBar.Width.Pixels = 20;
            QuestTasksScrollBar.Height.Pixels = 100;

            QuestTasksList.SetScrollbar(QuestTasksScrollBar);
            QuestTasksList.Add(QuestTasksText);
            QuestTasksArea.Append(QuestTasksScrollBar);
            QuestTasksArea.Append(QuestTasksList);

            QuestDetailsArea.Append(QuestTasksArea);
            #endregion

            /*
            #region Quest Rewards Header
            QuestRewardsHeader = new("Rewards", 0.66f, true)
            {
                HAlign = 0.95f,
                BackgroundColor = labelBack,
                MarginRight = 10
            };

            QuestRewardsHeader.Width.Pixels = 300;
            QuestRewardsHeader.Height.Pixels = 40;
            QuestRewardsHeader.Top.Pixels = 10 + QuestTasksArea.Top.Pixels + QuestTasksArea.Height.Pixels;
            QuestDetailsArea.Append(QuestRewardsHeader);
            #endregion

            #region Quest Rewards
            QuestRewardsArea = new()
            {
                HAlign = 0.95f,
                BackgroundColor = outerBack,
                BorderColor = outerBorder,
            };
            QuestRewardsArea.Width.Pixels = descWidth;
            QuestRewardsArea.Height.Pixels = 60;
            QuestRewardsArea.Top.Pixels = 5 + QuestRewardsHeader.Top.Pixels + QuestRewardsHeader.Height.Pixels;
            QuestRewards = [];

            QuestDetailsArea.Append(QuestRewardsArea);
            #endregion
            */
            
            Area.Append(QuestDetailsArea);

            Append(Area);

            #region Fake Mouse Text
            MouseTextPanel = new()
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };

            MouseText = new("")
            {
                TextColor = Color.Transparent,
                ShadowColor = Color.Transparent
            };

            MouseTextPanel.Append(MouseText);
            Append(MouseTextPanel);
            #endregion
        }

        public override void OnActivate()
        {
            if (!QuestSystem.Questlines.ContainsKey("LegendscribeEarlyGame"))
                return;

            if(InEarlygameState && CurrentQuestline.ID != "LegendscribeEarlyGame")
            {
                OnInitialize();
            }

            CurrentQuestline.Started = true;
            float maxDepth;
            if (CurrentQuestline.ID == "LegendscribeEarlyGame")
            {
                if (ContentReplacementSystem.NeedToReplaceContent)
                    maxDepth = FillDepthValues("GripsOfChaos", 0);
                else
                {
                    maxDepth = FillDepthValues("MushroomMonarch", 0);
                    DepthValues.TryAdd("FeudalFungus", 0);
                }
            }
            else
            {
                maxDepth = FillDepthValues("ForsakenAnubis", 0);
                if (!CurrentQuestline.Quests.ContainsKey(CurrentQuestID))
                    CurrentQuestID = "ForsakenAnubis";
            }

            if (CurrentQuest != null)
            {
                SwitchDisplayedQuest(CurrentQuest.ID);
                var viewPos = DepthValues.Count == 0 ? 0 : DepthValues[CurrentQuest.ID] / (float)DepthValues.Values.Max();
                QuestListScrollBar.ViewPosition = viewPos * QuestListScrollBar.ViewSize * 1.25f;
                QuestDescriptionScrollBar.ViewPosition = 0f;
                QuestTasksScrollBar.ViewPosition = 0f;
                QuestListBackground.RemoveAllChildren();
            }

            Nodes.Clear();
            Lines.Clear();

            var line = ModContent.Request<Texture2D>("AAModClassic/UI/Core/Line").Value;

            var depths = DepthValues.ToList();
            depths.Sort((v1, v2) => v1.Value > v2.Value ? 1 : v1.Value < v2.Value ? -1 : 0);

            for (var i = 0; i < DepthValues.Count; i++)
            {
                var quest = CurrentQuestline.Quests[depths[i].Key];

                var xPosition = 80 + DepthValues[quest.ID] / maxDepth * QuestListBackground.Width.Pixels * 0.9f;
                if (i != 0)
                    xPosition += Main.rand.NextFloat(-40, 40);

                var equalNodes = depths.Where(v => v.Value == DepthValues[quest.ID]).ToDictionary().Keys.ToList();
                var depthNodeCount = equalNodes.Count;
                var equalIndex = equalNodes.IndexOf(depths[i].Key) + 1;
                var openHeight = QuestListArea.Height.Pixels / (1f + depthNodeCount) * 1f;
                var yPosition = openHeight * equalIndex;
                yPosition -= 24;
                yPosition += Main.rand.NextFloat(-openHeight / (1f + depthNodeCount), openHeight / (1f + depthNodeCount));

                Vector2 placementPos = new(xPosition, yPosition);
                QuestNode node = new(CurrentQuestline.ID, quest.ID, placementPos, i);
                node.Width.Pixels = 48;
                node.Height.Pixels = 48;

                foreach (var gateID in quest.QuestUnlocks)
                {
                    FrameableUIImage uiLine = new(line);
                    uiLine.Color = CurrentQuestline.ID == "LegendscribeEarlyGame" ? Color.LightSkyBlue : Color.Gold;
                    uiLine.NormalizedOrigin.X = 0.5f;
                    uiLine.NormalizedOrigin.Y = 0.5f;
                    uiLine.Width.Pixels = 4;
                    uiLine.Height.Pixels = 4;

                    Lines.Add((quest.ID, gateID, uiLine));
                    QuestListBackground.Append(uiLine);
                }

                Nodes.Add(quest.ID, node);
                QuestListBackground.Append(node);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Area.GetDimensions().ToRectangle().Contains(Main.MouseScreen.ToPoint()))
                Main.LocalPlayer.mouseInterface = true;
            MouseText.SetText("");

            base.Update(gameTime);

            foreach(Quest q in CurrentQuestline.Quests.Values)
            {
                if(q.IsComplete && q.Active && !q.EverTurnedIn)
                {
                    q.EverTurnedIn = true;
                    q.IsTurnedIn = true;
                    foreach (string id in q.QuestUnlocks)
                    {
                        bool canUnlock = true;
                        foreach (string gate in CurrentQuestline.Quests[id].QuestRequirements)
                        {
                            if (id == q.ID)
                                continue;
                            if (!CurrentQuestline.Quests[gate].EverTurnedIn)
                                canUnlock = false;
                        }
                        if (canUnlock)
                            CurrentQuestline.UnlockedQuests.Add(id);
                    }
                    continue;
                }

                if (q.Active)
                    continue;
                
                bool unlocked = true;
                foreach(string key in q.QuestRequirements)
                {
                    if(!CurrentQuestline.Quests[key].IsComplete)
                    {
                        unlocked = false;
                        break;
                    }
                }
                if (unlocked)
                    q.StartQuest();
            }

            foreach ((var NodeIndex1, var NodeIndex2, var line) in Lines)
            {
                var start = Nodes[NodeIndex1].DrawPos + new Vector2(24, 24);
                var end = Nodes[NodeIndex2].DrawPos + new Vector2(24, 24);
                var startToEnd = end - start;
                line.Left.Pixels = start.X + startToEnd.X / 2f;
                line.Top.Pixels = start.Y + startToEnd.Y / 2f;
                line.Rotation = startToEnd.ToRotation();
                line.ImageScale = new(startToEnd.Length() / 500f, 4);

                if (CurrentQuestline.Quests[NodeIndex1].EverTurnedIn)
                    line.Color = CurrentQuestline.ID == "LegendscribeEarlyGame" ? Color.LightSkyBlue : Color.LightGreen;
                else
                    line.Color = CurrentQuestline.ID == "LegendscribeEarlyGame" ? Color.Navy : Color.SeaGreen;
            }

            /*
            var RewardsList = CurrentQuest == null ? null : CurrentQuest.EverTurnedIn && CurrentQuest.RepeatRewards != null ? CurrentQuest.RepeatRewards : CurrentQuest.Rewards;

            if (RewardsList != null)
            {
                for (var i = 0; i < RewardsList.Count; i++)
                {
                    if (QuestRewards[i].IsMouseHovering)
                    {
                        if (RewardsList[i].type == ItemID.Book)
                            MouseText.SetText(CurrentQuest.ExtraRewardDesc.Value);
                        else
                        {
                            var item = RewardsList[i];
                            item.SetDefaults(item.type);
                            var text = item.Name;

                            if (item.DamageType.DisplayName.Value != " damage" && item.ammo == AmmoID.None)
                            {
                                var dmg = item.DamageType.DisplayName.Value;
                                dmg = dmg[..^7];
                                var first = dmg[1];
                                if (dmg[0] == ' ')
                                    dmg = dmg[2..];
                                else
                                {
                                    first = dmg[0];
                                    dmg = dmg[1..];
                                }
                                dmg = char.ToUpper(first) + dmg;
                                text += "\n" + dmg + " Weapon";
                            }
                            else if (item.accessory)
                                text += "\nAccessory";
                            else if (item.createTile != -1)
                                text += "\nPlaceable";
                            else if (item.material)
                                text += "\nMaterial";

                            for (var j = 0; j < item.ToolTip.Lines; j++)
                            {
                                var line = item.ToolTip.GetLine(j);
                                if (line != "")
                                {
                                    text += "\n";
                                    text += line;
                                }
                            }
                            MouseText.SetText(text);
                        }
                    }
                }
            }
            */

            #region Mouse Text Updates
            if (MouseText.Text != "")
            {
                MouseTextPanel.Left.Pixels = Main.MouseScreen.X + 10;
                MouseTextPanel.Top.Pixels = Main.MouseScreen.Y + 10;
                // measure string doesnt account for chat tags, we must doe this ourselves
                var text = MouseText.Text;
                while (text.Contains('['))
                {
                    var startIndex = text.IndexOf('[');
                    var endIndex = text.IndexOf(']');
                    text = text.Remove(startIndex, endIndex - startIndex);
                    text = text.Insert(startIndex, "   ");
                }
                var textSize = FontAssets.MouseText.Value.MeasureString(text);
                MouseTextPanel.Width.Pixels = textSize.X + 25;
                MouseTextPanel.Height.Pixels = textSize.Y + 15;

                var right = MouseTextPanel.Left.Pixels + MouseTextPanel.Width.Pixels;
                if (right > Main.screenWidth)
                {
                    MouseTextPanel.Left.Pixels -= right - Main.screenWidth;
                }

                MouseTextPanel.BackgroundColor = Color.CadetBlue with { A = (byte)(255 * 0.8f) };
                MouseTextPanel.BorderColor = Color.Black;
                MouseText.TextColor = Color.White;
                MouseText.ShadowColor = Color.Black;
            }
            else
            {
                MouseTextPanel.BackgroundColor = Color.Transparent;
                MouseTextPanel.BorderColor = Color.Transparent;
                MouseText.TextColor = Color.Transparent;
                MouseText.ShadowColor = Color.Transparent;
            }
            #endregion
        }

        public static int FillDepthValues(string myID, int depth)
        {
            if (DepthValues.TryGetValue(myID, out var value))
            {
                if (value < depth)
                    DepthValues[myID] = depth;
            }
            else
                DepthValues.Add(myID, depth);

            var count = CurrentQuestline.Quests[myID].QuestUnlocks.Length;
            var depths = new int[count];
            for (var i = 0; i < count; i++)
                depths[i] = FillDepthValues(CurrentQuestline.Quests[myID].QuestUnlocks[i], depth + 1);

            return count == 0 ? depth : depths.Max();
        }

        public static void SwitchDisplayedQuest(string id)
        {
            CurrentQuestID = id;

            QuestTitle.SetText(CurrentQuest.Name.Value);

            float descWidth = 320;
            string desc = (CurrentQuest.IsComplete && CurrentQuest.DescriptionComplete != null ? CurrentQuest.DescriptionComplete : CurrentQuest.DescriptionIncomplete).Format(Main.LocalPlayer.name);
            var wrapped = FontAssets.MouseText.Value.CreateWrappedText(desc, descWidth - 40);
            QuestDescriptionText.SetText(wrapped);
            QuestDescriptionText.Height.Pixels = FontAssets.MouseText.Value.MeasureString(QuestDescriptionText.Text).Y;

            if (QuestDescriptionText.Height.Pixels <= QuestDescriptionList.Height.Pixels)
            {
                if (QuestDescriptionArea.HasChild(QuestDescriptionScrollBar))
                    QuestDescriptionArea.RemoveChild(QuestDescriptionScrollBar);
            }
            else
            {
                if (!QuestDescriptionArea.HasChild(QuestDescriptionScrollBar))
                    QuestDescriptionArea.Append(QuestDescriptionScrollBar);
            }

            UpdateTasksText();

            if (QuestTasksText.Height.Pixels <= QuestTasksList.Height.Pixels)
            {
                if (QuestTasksArea.HasChild(QuestTasksScrollBar))
                    QuestTasksArea.RemoveChild(QuestTasksScrollBar);
            }
            else
            {
                if (!QuestTasksArea.HasChild(QuestTasksScrollBar))
                    QuestTasksArea.Append(QuestTasksScrollBar);
            }

            //UpdateRewardsArea();
        }

        /*
        private static void UpdateRewardsArea(bool forceToRepeatable = false)
        {
            QuestRewards.Clear();
            QuestRewardsArea.RemoveAllChildren();

            var RewardsList = (CurrentQuest.EverTurnedIn || forceToRepeatable) && CurrentQuest.RepeatRewards != null ? CurrentQuest.RepeatRewards : CurrentQuest.Rewards;

            if (RewardsList != null)
            {
                for (var j = 0; j < RewardsList.Count; j++)
                {
                    var i = RewardsList[j];
                    var rewardArea = new UIPanel();
                    rewardArea.BackgroundColor = Color.Transparent;
                    rewardArea.BorderColor = Color.Transparent;
                    rewardArea.VAlign = 0.5f;
                    rewardArea.Top.Pixels += j % 2 == 0 ? -12 : 12;
                    rewardArea.HAlign = 1 / (float)(1 + RewardsList.Count) * (j + 1);

                    var tex = TextureAssets.Item[i.type];
                    UIImage item = new(tex);
                    item.Width.Pixels = tex.Width();
                    item.Height.Pixels = tex.Height();
                    if (item.Height.Pixels > 20)
                    {
                        var scale = 20 / item.Height.Pixels;
                        item.ImageScale = scale;
                    }
                    item.Top.Pixels -= item.Height.Pixels / 2f;
                    item.Left.Pixels -= item.Width.Pixels / 2f - 8;
                    UIText amt = new("x" + i.stack);
                    amt.VAlign = 0.5f;
                    amt.Left.Pixels = item.Width.Pixels / 2f + 12;

                    rewardArea.Width.Pixels = item.Width.Pixels + FontAssets.MouseText.Value.MeasureString(amt.Text).X + 20;
                    rewardArea.Height.Pixels = 30;
                    rewardArea.Append(item);
                    rewardArea.Append(amt);

                    QuestRewards.Add(rewardArea);
                    QuestRewardsArea.Append(rewardArea);
                }
            }
        }
        */

        private static void UpdateTasksText()
        {
            var quest = CurrentQuest;

            var split = quest.Tasks.Value.Split('\n');
            var combined = "";
            for (var i = 0; i < split.Length; i++)
            {
                if (quest.IsTurnedIn && !quest.IsRepeatable || quest.AutoCompletes || quest.IsComplete || i < quest.Objectives.Count && quest.Objectives[i].IsComplete)
                    split[i] = split[i].Replace('-', '✓');

                if (i < quest.Objectives.Count)
                    switch (quest.Objectives[i].ProgressDisplay)
                    {
                        case ObjectiveProgressDisplay.Ratio:
                            split[i] += " " + quest.Objectives[i].CompletionRatio;
                            break;
                        case ObjectiveProgressDisplay.Percentage:
                            split[i] += " " + quest.Objectives[i].CompletionPercentage;
                            break;
                    }

                combined += split[i] + '\n';
            }

            var wrapped = FontAssets.MouseText.Value.CreateWrappedText(combined, 320 - 40);
            List<int> insertIndexes = [];

            for (var i = 0; i < wrapped.Length; i++)
                if (i != wrapped.Length - 1 && wrapped[i] == '\n' && wrapped[i + 1] != '-' && wrapped[i + 1] != '✓')
                    insertIndexes.Add(i + 1 + insertIndexes.Count * 3);

            foreach (var index in insertIndexes)
                wrapped = wrapped.Insert(index, "   ");

            QuestTasksText.SetText(wrapped);
            QuestTasksText.Height.Pixels = FontAssets.MouseText.Value.MeasureString(QuestTasksText.Text).Y;
        }
    }

    public class QuestNode(string questline, string id, Vector2 pos, int nodeID) : UIElement
    {
        public FrameableUIImage Node;
        public FrameableUIImage Icon;

        public string Questline = questline;
        public string ID = id;
        public Vector2 Position = pos;
        private readonly int NodeID = nodeID;
        public Vector2 DrawPos => new(Left.Pixels, Top.Pixels);

        internal class NodeBubble(Vector2 startOffset, Vector2 startVelocity)
        {
            internal Vector2 offset = startOffset;
            internal Vector2 velocity = startVelocity;
            internal Color color = Main.rand.NextBool() ? Color.Cyan : Main.rand.NextBool() ? Color.Aqua : Main.rand.NextBool() ? Color.White : Color.MediumSpringGreen;
            internal float scale = Main.rand.NextFloat(0.33f, 0.9f);
            internal int counter = 0;
        }
        private List<NodeBubble> bubbles = [];

        private Vector2 birdOffset = pos;
        private Vector2 birdVelocity = Main.rand.NextVector2CircularEdge(4, 4);
        private Vector2 fishAcceleration = Vector2.Zero;

        bool IsSelected => LegendscribeQuestUI.CurrentQuest == null ? false : LegendscribeQuestUI.CurrentQuest.ID == ID;
        int selectedCounter = 0;
        int hoverCounter = 0;
        float Floatiness => MathHelper.Clamp(MathUtils.SineInOutEasing(hoverCounter / 30f), 0f, 1f);
        float ScalePower => MathHelper.Clamp(MathUtils.SineInOutEasing(selectedCounter / 30f), 0f, 1f);

        public override void OnInitialize()
        {
            Texture2D node;
            if(LegendscribeQuestUI.CurrentQuestline.ID == "LegendscribeEarlyGame")
                node = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Desert/__Hardmode/NPCs/__BossAnubis/Runes/AnubisCircle", AssetRequestMode.ImmediateLoad).Value;
            else
                node = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Desert/__Hardmode/NPCs/__BossAnubis/Runes/ForsakenCircle", AssetRequestMode.ImmediateLoad).Value;

            Node = new(node)
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                frame = node.Frame(),
                NormalizedOrigin = Vector2.One * 0.5f
            };
            Node.Width.Pixels = Node.frame.Width;
            Node.Height.Pixels = Node.frame.Height;
            Node.Top.Pixels = 24;
            Node.Left.Pixels = 24;

            Icon = new(QuestSystem.Questlines[Questline].Quests[ID].Icon)
            {
                NormalizedOrigin = Vector2.One * 0.5f
            };
            Icon.Top.Pixels = Icon.Height.Pixels * 1.5f;
            Icon.Left.Pixels = Icon.Width.Pixels * 1.5f;

            Append(Icon);
            Append(Node);

            Left.Pixels = Position.X;
            Top.Pixels = Position.Y;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Icon == null)
            {
                RemoveAllChildren();
                OnInitialize();
            }

            Node.ImageScale = Vector2.One * (0.5f + ScalePower * 0.25f + ((MathF.Sin((Main.GlobalTimeWrappedHourly + NodeID) * 10) / 2f + 0.5f) / 40f));
            Node.Color = QuestSystem.Questlines[Questline].UnlockedQuests.Contains(ID) ? Color.White : Color.Gray;
            Node.Rotation += 0.025f * (NodeID % 2 == 0 ? -1 : 1);

            if (Icon != null)
            {
                Icon.NormalizedOrigin = Vector2.One * 0.5f;
                Icon.Width.Pixels = QuestSystem.Questlines[Questline].Quests[ID].Icon.Width();
                Icon.Height.Pixels = QuestSystem.Questlines[Questline].Quests[ID].Icon.Height();
                Icon.Top.Pixels = 23;
                Icon.Left.Pixels = 23;

                Icon.ImageScale = Vector2.One * (1f + ScalePower * 0.5f);
                Icon.Color = QuestSystem.Questlines[Questline].UnlockedQuests.Contains(ID) ? Color.White : Color.Black;
            }

            Left.Pixels = Position.X + (float)Math.Sin(Main.GlobalTimeWrappedHourly / 3f + Position.Y) * 8f * (1 - Floatiness);
            Top.Pixels = Position.Y + (float)Math.Sin(Main.GlobalTimeWrappedHourly + Position.Y) * 12f * (1 - Floatiness);

            if (IsMouseHovering)
            {
                LegendscribeQuestUI.MouseText.SetText(QuestSystem.Questlines[Questline].Quests[ID].Name.Value);
                if (hoverCounter < 30)
                    hoverCounter++;
            }
            else if (hoverCounter > 0)
                hoverCounter--;

            if (IsSelected)
            {
                if (selectedCounter < 30)
                    selectedCounter++;
            }
            else if (selectedCounter > 0)
                selectedCounter--;

            /*
            bubbles ??= [];

            if (QuestSystem.Questlines[Questline].Quests[ID].Active && (int)(Main.GlobalTimeWrappedHourly * 60) % 5 == 0)
                bubbles.Add(new(new(Main.rand.NextFloat(-40, 30), 48), new(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-1, -4))));
            for (var i = 0; i < bubbles.Count; i++)
            {
                bubbles[i].velocity.X = MathHelper.Lerp(-1f, 1f, MathUtils.SineBumpEasing(bubbles[i].counter / 30f));
                bubbles[i].offset += bubbles[i].velocity;

                if (bubbles[i].counter > 36)
                    bubbles.RemoveAt(i);
                else
                    bubbles[i].counter++;
            }
            */

            var nodePos = Position;
            var fishPos = birdOffset;

            birdOffset += birdVelocity;

            if (QuestSystem.Questlines[Questline].Quests[ID].EverTurnedIn)
            {
                var buffer = 60;
                var areaSize = new Vector2(Parent.Width.Pixels, LegendscribeQuestUI.QuestList.Height.Pixels);
                var area = new Rectangle(buffer / 2, buffer / 2, (int)areaSize.X - buffer * 2, (int)areaSize.Y - buffer * 2);
                if (!area.Contains(fishPos.ToPoint()))
                {
                    float X = 0;
                    float Y = 0;
                    if (fishPos.ToPoint().X > area.Width)
                        X = -1;
                    else if (fishPos.ToPoint().X < area.X)
                        X = 1;

                    if (fishPos.ToPoint().Y > area.Height)
                        Y = -1;
                    else if (fishPos.ToPoint().Y < area.Y)
                        Y = 1;

                    if (X != 0 && Y != 0)
                    {
                        X *= 0.5f;
                        Y *= 0.5f;
                    }

                    fishAcceleration = new Vector2(X, Y) * 0.1f;
                }
                fishAcceleration = fishAcceleration.RotatedBy((float)Math.Sin(Main.GlobalTimeWrappedHourly + NodeID * 12) * 0.03f);
            }
            else
            {
                if (Vector2.DistanceSquared(nodePos, fishPos) > 1024)
                    fishAcceleration = (nodePos - fishPos).SafeNormalize(Vector2.Zero) * 0.15f;
                else
                    fishAcceleration = fishAcceleration.RotatedBy((float)Math.Sin(Main.GlobalTimeWrappedHourly * 24 + NodeID * 12) * 0.2f);
            }

            birdVelocity += fishAcceleration;
            if (birdVelocity.LengthSquared() > 6.25f)
                birdVelocity = birdVelocity.SafeNormalize(Vector2.UnitX) * 2.5f;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (QuestSystem.Questlines[Questline].UnlockedQuests.Contains(ID))
                LegendscribeQuestUI.SwitchDisplayedQuest(ID);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!QuestSystem.Questlines[Questline].UnlockedQuests.Contains(ID))
                return;

            var drawPos = new Vector2(Width.Pixels / 2f, Height.Pixels / 2f) + Parent.GetDimensions().Position();

            Texture2D bird;
            int drawY;
            Rectangle frame;
            SpriteEffects effects;
            bool canRotate = true;
            switch (NodeID)
            {
                case 2:
                case 7:
                case 9:
                case 10:
                    bird = TextureAssets.Npc[NPCID.Grebe2].Value;
                    drawY = ((int)(Main.GlobalTimeWrappedHourly * 8)) % 4;
                    frame = bird.Frame(1, 15, 0, 11 + drawY);
                    effects = SpriteEffects.None;
                    break;
                case 8:
                    bird = TextureAssets.Npc[ModContent.NPCType<HorusHawk>()].Value;
                    drawY = ((int)(Main.GlobalTimeWrappedHourly * 8)) % 4;
                    frame = bird.Frame(1, 4, 0, drawY);
                    effects = SpriteEffects.None;
                    break;
                case 11:
                    bird = TextureAssets.Npc[ModContent.NPCType<RabbitcopterSoldier>()].Value;
                    drawY = ((int)(Main.GlobalTimeWrappedHourly * 8)) % 4;
                    frame = bird.Frame(1, 4, 0, drawY);
                    effects = SpriteEffects.None;
                    canRotate = false;
                    break;
                default:
                    int npcID;
                    switch(NodeID % 3)
                    {
                        case 0:
                            npcID = NPCID.YellowDragonfly;
                            break;
                        case 1:
                            npcID = NPCID.OrangeDragonfly;
                            break;
                        default:
                            npcID = NPCID.BlackDragonfly;
                            break;
                    }
                    bird = TextureAssets.Npc[npcID].Value;
                    drawY = ((int)(Main.GlobalTimeWrappedHourly * 16)) % 4;
                    frame = bird.Frame(1, 4, 0, drawY);
                    effects = SpriteEffects.None;
                    break;
            }

            if (canRotate)
            {
                if (birdVelocity.X > 0)
                    effects = (effects == SpriteEffects.None ? SpriteEffects.FlipVertically : SpriteEffects.None);
            }
            else
            {
                if (birdVelocity.X > 0)
                    effects = (effects == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            }

            spriteBatch.Draw(bird, drawPos + birdOffset, frame, Color.White, (canRotate ? birdVelocity.ToRotation() + MathHelper.Pi : 0), frame.Size() * 0.5f, 1f, effects, 0);
        }
    }

    public class HorizontalUIScrollbar : UIScrollbar
    {
        private float _viewPosition;
        private float _viewSize = 1f;
        private float _maxViewSize = 20f;
        private bool _isDragging;
        private bool _isHoveringOverHandle;
        private float _dragYOffset;
        private Asset<Texture2D> _texture;
        private Asset<Texture2D> _innerTexture;

        public new float ViewPosition
        {
            get
            {
                return _viewPosition;
            }
            set
            {
                _viewPosition = MathHelper.Clamp(value, 0f, _maxViewSize - _viewSize);
            }
        }

        public new bool CanScroll => _maxViewSize != _viewSize;

        public new void GoToBottom()
        {
            ViewPosition = _maxViewSize - _viewSize;
        }

        public HorizontalUIScrollbar()
        {
            Height.Set(20f, 0f);
            Height.Set(20f, 0f);
            _texture = Main.Assets.Request<Texture2D>("Images/UI/Scrollbar");
            _innerTexture = Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner");
            PaddingTop = 5f;
            PaddingBottom = 5f;
        }

        public new void SetView(float viewSize, float maxViewSize)
        {
            viewSize = MathHelper.Clamp(viewSize, 0f, maxViewSize);
            _viewPosition = MathHelper.Clamp(_viewPosition, 0f, maxViewSize - viewSize);
            _viewSize = viewSize;
            _maxViewSize = maxViewSize;
        }

        public new float GetValue() => _viewPosition;

        private Rectangle GetHandleRectangle()
        {
            CalculatedStyle innerDimensions = GetInnerDimensions();
            if (_maxViewSize == 0f && _viewSize == 0f)
            {
                _viewSize = 1f;
                _maxViewSize = 1f;
            }

            return new Rectangle((int)(innerDimensions.X + innerDimensions.Width * (_viewPosition / _maxViewSize)), (int)innerDimensions.Y - 3, (int)(innerDimensions.Width * (_viewSize / _maxViewSize)) + 7, 20);
        }

        internal static void DrawBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle dimensions, Color color)
        {
            spriteBatch.Draw(texture, new Rectangle(dimensions.X - 6, dimensions.Y, 6, dimensions.Height), new Rectangle(0, 0, 6, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y, dimensions.Width, dimensions.Height), new Rectangle(6, 0, 4, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle(dimensions.X + dimensions.Width, dimensions.Y, 6, dimensions.Height), new Rectangle(texture.Width - 6, 0, 6, texture.Height), color);
        }

        internal static void DrawHandleBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle dimensions, Color color)
        {
            float rotation = -MathHelper.PiOver2;

            void DrawRotatedSlice(Rectangle targetRect, Rectangle sourceRect)
            {
                Vector2 origin = new(sourceRect.Width / 2f, sourceRect.Height / 2f);
                Vector2 position = new(targetRect.X + targetRect.Width / 2f, targetRect.Y + targetRect.Height / 2f);
                spriteBatch.Draw(texture, position, sourceRect, color, rotation, origin, new Vector2((float)targetRect.Height / sourceRect.Width, (float)targetRect.Width / sourceRect.Height), SpriteEffects.None, 0f);
            }
            DrawRotatedSlice(new Rectangle(dimensions.X - 2, dimensions.Y - 2, 6, dimensions.Height), new Rectangle(0, 0, texture.Width, 6));
            DrawRotatedSlice(new Rectangle(dimensions.X + 4, dimensions.Y - 2, dimensions.Width - 12, dimensions.Height), new Rectangle(0, 6, texture.Width, 4));
            DrawRotatedSlice(new Rectangle(dimensions.X + dimensions.Width - 8, dimensions.Y - 2, 6, dimensions.Height), new Rectangle(0, texture.Height - 6, texture.Width, 6));
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            CalculatedStyle innerDimensions = GetInnerDimensions();
            if (_isDragging)
            {
                float num = UserInterface.ActiveInstance.MousePosition.X - innerDimensions.X - _dragYOffset;
                _viewPosition = MathHelper.Clamp(num / innerDimensions.Width * _maxViewSize, 0f, _maxViewSize - _viewSize);
            }

            Rectangle handleRectangle = GetHandleRectangle();
            Vector2 mousePosition = UserInterface.ActiveInstance.MousePosition;
            bool isHoveringOverHandle = _isHoveringOverHandle;
            // tML: Added IsMouseHovering to account for obstructing UI elements such as popups
            _isHoveringOverHandle = IsMouseHovering && handleRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));
            if (!isHoveringOverHandle && _isHoveringOverHandle && Main.hasFocus)
                SoundEngine.PlaySound(SoundID.MenuTick);

            DrawBar(spriteBatch, _texture.Value, dimensions.ToRectangle(), Color.White);
            DrawHandleBar(spriteBatch, _innerTexture.Value, handleRectangle, Color.White * ((_isDragging || _isHoveringOverHandle) ? 1f : 0.85f));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsMouseHovering)
                PlayerInput.LockVanillaMouseScroll("AAModClassic/HorizontalUIScrollBar");
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            if (evt.Target == this)
            {
                Rectangle handleRectangle = GetHandleRectangle();
                if (handleRectangle.Contains(new Point((int)evt.MousePosition.X, (int)evt.MousePosition.Y)))
                {
                    _isDragging = true;
                    _dragYOffset = evt.MousePosition.X - (float)handleRectangle.X;
                }
                else
                {
                    CalculatedStyle innerDimensions = GetInnerDimensions();
                    float num = UserInterface.ActiveInstance.MousePosition.X - innerDimensions.X - (float)(handleRectangle.Width >> 1);
                    _viewPosition = MathHelper.Clamp(num / innerDimensions.Width * _maxViewSize, 0f, _maxViewSize - _viewSize);
                }
            }
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            _isDragging = false;
        }
    }

    public class UIHorizontalList : UIElement, IEnumerable<UIElement>, IEnumerable
    {
        public delegate bool ElementSearchMethod(UIElement element);

        private class UIInnerList : UIElement
        {
            public override bool ContainsPoint(Vector2 point) => true;

            protected override void DrawChildren(SpriteBatch spriteBatch)
            {
                Vector2 position = base.Parent.GetDimensions().Position();
                Vector2 dimensions = new Vector2(base.Parent.GetDimensions().Width, base.Parent.GetDimensions().Height);
                foreach (UIElement element in Elements)
                {
                    Vector2 position2 = element.GetDimensions().Position();
                    Vector2 dimensions2 = new Vector2(element.GetDimensions().Width, element.GetDimensions().Height);
                    if (Collision.CheckAABBvAABBCollision(position, dimensions, position2, dimensions2))
                        element.Draw(spriteBatch);
                }
            }

            public override Rectangle GetViewCullingArea() => base.Parent.GetDimensions().ToRectangle();
        }

        public List<UIElement> _items = new List<UIElement>();
        protected HorizontalUIScrollbar _scrollbar; // Using your custom scrollbar
        internal UIElement _innerList = new UIInnerList();
        private float _innerListWidth; // Changed from Height
        public float ListPadding = 5f;
        public Action<List<UIElement>> ManualSortMethod;

        public int Count => _items.Count;

        public UIHorizontalList()
        {
            _innerList.OverflowHidden = false;
            _innerList.Width.Set(0f, 1f);
            _innerList.Height.Set(0f, 1f);
            OverflowHidden = true;
            Append(_innerList);
        }

        public float GetTotalWidth() => _innerListWidth;

        public void Goto(ElementSearchMethod searchMethod, bool center = false)
        {
            var innerDimensionWidth = GetInnerDimensions().Width;
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                if (searchMethod(item))
                {
                    _scrollbar.ViewPosition = item.Left.Pixels; // Changed from Top
                    if (center)
                    {
                        _scrollbar.ViewPosition = item.Left.Pixels - innerDimensionWidth / 2 + item.GetOuterDimensions().Width / 2;
                    }
                    return;
                }
            }
        }

        public virtual void Add(UIElement item)
        {
            _items.Add(item);
            _innerList.Append(item);
            UpdateOrder();
            _innerList.Recalculate();
        }

        public virtual bool Remove(UIElement item)
        {
            _innerList.RemoveChild(item);
            UpdateOrder();
            return _items.Remove(item);
        }

        public virtual void Clear()
        {
            _innerList.RemoveAllChildren();
            _items.Clear();
        }

        public override void Recalculate()
        {
            base.Recalculate();
            UpdateScrollbar();
        }

        public override void ScrollWheel(UIScrollWheelEvent evt)
        {
            base.ScrollWheel(evt);
            if (_scrollbar != null)
                _scrollbar.ViewPosition -= evt.ScrollWheelValue;
        }

        public override void RecalculateChildren()
        {
            base.RecalculateChildren();
            float num = 0f;
            for (int i = 0; i < _items.Count; i++)
            {
                float num2 = ((_items.Count == 1) ? 0f : ListPadding);
                _items[i].Left.Set(num, 0f); // Position items horizontally
                _items[i].Top.Set(0f, 0f);  // Keep them at the top of the inner list
                _items[i].Recalculate();
                num += _items[i].GetOuterDimensions().Width + num2;
            }
            _innerListWidth = num;
        }

        private void UpdateScrollbar()
        {
            if (_scrollbar != null)
            {
                float width = GetInnerDimensions().Width;
                _scrollbar.SetView(width, _innerListWidth);
            }
        }

        public void SetScrollbar(HorizontalUIScrollbar scrollbar)
        {
            _scrollbar = scrollbar;
            UpdateScrollbar();
        }

        public void UpdateOrder()
        {
            if (ManualSortMethod != null)
                ManualSortMethod(_items);
            else
                _items.Sort(SortMethod);

            UpdateScrollbar();
        }

        public int SortMethod(UIElement item1, UIElement item2) => item1.CompareTo(item2);

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (_scrollbar != null)
                _innerList.Left.Set(0f - _scrollbar.GetValue(), 0f); // Scroll horizontally

            Recalculate();
        }

        public IEnumerator<UIElement> GetEnumerator() => ((IEnumerable<UIElement>)_items).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<UIElement>)_items).GetEnumerator();
    }

    public class UIParallaxBackground(Asset<Texture2D> background, List<BackgroundLayer> layers, float imageScale) : UIElement
    {
        public struct BackgroundLayer(Asset<Texture2D> tex, float strength, Vector2 off, Color color = default)
        {
            public Asset<Texture2D> Texture = tex;
            public float ParallazStrength = strength;
            public Vector2 Offset = off;
            public Color Color = color == default ? Color.White : color;
        }

        public List<BackgroundLayer> Layers = layers;
        public Asset<Texture2D> Background = background;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background.Value, GetOuterDimensions().ToRectangle(), LegendscribeQuestUI.CurrentQuestline.ID == "LegendscribeEarlyGame" ? Color.White : Color.Black);

            Rectangle parentArea = Parent.Parent.GetInnerDimensions().ToRectangle();
            float width = parentArea.Width;
            float diff = (GetInnerDimensions().Position().X - parentArea.TopLeft().X);

            foreach (var layer in Layers)
            {
                float texWidth = layer.Texture.Width() * imageScale;
                for (float i = -width + (diff * layer.ParallazStrength); i <= (width + texWidth); i += texWidth)
                {
                    spriteBatch.Draw(layer.Texture.Value, parentArea.TopLeft() + new Vector2(i, (parentArea.Height / 2)) + layer.Offset, null, layer.Color, 0, layer.Texture.Size() * 0.5f, imageScale, 0, 0);
                }
            }           
        }
    }

    public class LegendscribeQuestUISystem : ModSystem
    {
        /// <summary>
        /// Is the UI up?
        /// </summary>
        public static bool IsActive = false;
        /// <summary>
        /// Legendscribe' index. Used to check if he's still around so that the UI doesn't stay up if he dies or the player runs away
        /// </summary>
        public static int NPCIndex = -1;

        public static UserInterface userInterface;
        public static LegendscribeQuestUI questUI;

        public override void OnModLoad()
        {
            if (!Main.dedServ)
            {
                userInterface = new();
                questUI = new();
            }
        }

        public override void PostSetupContent()
        {
            if(!Main.dedServ)
                questUI.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (userInterface?.CurrentState != null)
                userInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("Ancients Awakened Classic: Legendscribe Quest", () =>
                {
                    userInterface.Draw(Main.spriteBatch, new GameTime());
                    return true;
                }, InterfaceScaleType.UI));
            }
        }

        public override void PostUpdateEverything()
        {
            // Don't bother doing anything except resetting if not looking at the UI.
            if (!IsActive)
                return;

            var player = Main.LocalPlayer;

            // Check if the player can still be in the UI.
            if (Main.playerInventory || player.chest != -1 || player.sign != -1 || player.talkNPC == -1 || !InRangeOfNPC() || Main.InGuideCraftMenu)
            {
                CloseLegendscribeUI();
                Main.CloseNPCChatOrSign();
                return;
            }

            Main.npcChatText = string.Empty;
        }

        public static void OpenLegendscribeUI(int npcIndex)
        {
            IsActive = true;
            NPCIndex = npcIndex;
            Main.playerInventory = false;
            userInterface?.SetState(questUI);
            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        public static void CloseLegendscribeUI()
        {
            userInterface?.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
            IsActive = false;
            NPCIndex = -1;
        }

        internal static bool InRangeOfNPC()
        {
            // Don't bother trying if no valid NPC has been selected yet.
            if (!Main.npc.IndexInRange(NPCIndex) || !Main.npc[NPCIndex].active)
                return false;

            var validTalkArea = Utils.CenteredRectangle(Main.LocalPlayer.Center, new Vector2(Player.tileRangeX * 3f, Player.tileRangeY * 2f) * 16f);
            return validTalkArea.Intersects(Main.npc[NPCIndex].Hitbox);
        }

    }

    public sealed class QuestStartPacket : AAPacket
    {
        protected override void Write(BinaryWriter w, object[] args)
        {
            Quest q = (Quest)args[0];
            w.Write(q.QuestLine);
            w.Write(q.ID);
        }

        public override void HandlePacket(BinaryReader packet, int sender)
        {
            string questline = packet.ReadString();
            string questID = packet.ReadString();
            Quest subquest = QuestSystem.Questlines[questline].Quests[questID];
            subquest.Active = true;
        }
    }

    public sealed class QuestProgressionPacket : AAPacket
    {
        protected override void Write(BinaryWriter w, object[] args)
        {
            string questline = (string)args[0];
            string questID = (string)args[1];
            int progressionIndex = (int)args[2];
            int progress = (int)args[3];
            w.Write(questline);
            w.Write(questID);
            w.Write7BitEncodedInt(progressionIndex);
            w.Write7BitEncodedInt(progress);
        }

        public override void HandlePacket(BinaryReader packet, int sender)
        {
            string questline = packet.ReadString();
            string questID = packet.ReadString();
            int progressIndex = packet.Read7BitEncodedInt();
            int progress = packet.Read7BitEncodedInt();
            Quest quest = QuestSystem.Questlines[questline].Quests[questID];
            QuestObjective node = quest.Objectives[progressIndex];

            node.AddProgress(progress, true, true);
        }
    }

    public sealed class QuestCompletionPacket : AAPacket
    {
        protected override void Write(BinaryWriter w, object[] args)
        {
            Quest q = (Quest)args[0];
            w.Write(q.QuestLine);
            w.Write(q.ID);
        }

        public override void HandlePacket(BinaryReader packet, int sender)
        {
            string questline = packet.ReadString();
            string questID = packet.ReadString();
            Quest subquest = QuestSystem.Questlines[questline].Quests[questID];

            // Progress overall progression if this quest hasnt been completed before
            if (!subquest.EverTurnedIn)
            {
                foreach (string id in subquest.QuestUnlocks)
                {
                    bool canUnlock = true;
                    foreach (string gate in QuestSystem.Questlines[questline].Quests[id].QuestRequirements)
                    {
                        if (id == subquest.ID)
                            continue;
                        if (!QuestSystem.Questlines[questline].Quests[gate].EverTurnedIn)
                            canUnlock = false;
                    }
                    if (canUnlock)
                        QuestSystem.Questlines[questline].UnlockedQuests.Add(id);
                }
            }

            subquest.IsTurnedIn = true;
            subquest.Active = false;
            subquest.EverTurnedIn = true;
        }
    }
}
