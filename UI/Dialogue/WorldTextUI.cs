using AAModClassic.Dialogues;
using AAModClassic.Globals;
using AAModClassic.UI.Dialogue.DisplayEffects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace AAModClassic.UI.Dialogue
{
    internal class WorldTextUI : UIState
    {
        internal static readonly Dictionary<int, (string name, TextDisplay ui, DialogueTextData data, Entity entity, int upTime)> Dialogues = [];
        internal static readonly List<int> DialoguesToRemove = [];

        public override void Update(GameTime gameTime)
        {
            foreach (int index in DialoguesToRemove)
            {
                RemoveChild(Dialogues[index].ui);
                Dialogues.Remove(index);
            }
            DialoguesToRemove.Clear();

            foreach (var pair in Dialogues)
            {
                int slot = pair.Key;
                var dialog = pair.Value;

                TextDisplay ui = dialog.ui;
                DialogueTextData data = dialog.data;

                if (ui.DisplayEffects.FadeWhenTooFar)
                {
                    float distFromSource = Vector2.Distance(Main.LocalPlayer.Center, ui.Position);
                    // If the player is too far, cancel the dialogue
                    if (distFromSource > ui.DisplayEffects.FadeBuffer + ui.DisplayEffects.FadeDistance)
                    {
                        DialoguesToRemove.Add(slot);
                        continue;
                    }
                }

                if (dialog.entity != null)
                {
                    if (dialog.entity.active)
                        dialog.ui.Position = dialog.entity.Center;
                    else if (ui.DisplayEffects.DespawnWithAttachedNPC)
                        dialog.ui.ClosingDialogue = true;
                    else
                    {
                        dialog.ui.Position = dialog.entity.Center;
                        dialog.entity = null;
                    }
                }

                if (dialog.upTime != -1)
                {
                    if (ui.Uptime >= dialog.upTime)
                    {
                        if (ui.ProgressDialogue)
                            ui.SwitchingPage = true;
                        else
                            ui.ClosingDialogue = true;
                    }
                }

                if (ui.DialoguePage.Event != null)
                {
                    if (ui.DialoguePage.Event.IsOver)
                    {
                        if (!ui.ProgressDialogue)
                            DialoguesToRemove.Add(slot);
                        else
                        {
                            if (++data.Page >= data.PageCount)
                                DialoguesToRemove.Add(slot);
                            else
                            {
                                ui.DialoguePage = data.Pages[data.Page];
                                ui.SwitchingPage = false;
                                ui.SwitchCounter = 0;
                                Activate();
                            }
                        }
                        return;
                    }
                }
                if (ui.Switching)
                {
                    if (ui.SwitchCounter >= ui.DisplayEffects.TimeToDisappear)
                    {
                        if (ui.ClosingDialogue || !ui.ProgressDialogue)
                            DialoguesToRemove.Add(slot);
                        else
                        {
                            if (++data.Page >= data.PageCount)
                                DialoguesToRemove.Add(slot);
                            else
                            {
                                ui.DialoguePage = data.Pages[data.Page];
                                ui.SwitchingPage = false;
                                ui.SwitchCounter = 0;
                                Activate();
                            }
                        }
                        continue;
                    }
                    ui.SwitchCounter++;
                }
            }

            base.Update(gameTime);
        }
    }

    public class WorldTextSystem : ModSystem
    {
        internal static WorldTextUI State;

        internal static UserInterface UI;

        public enum DisplayEffectID
        {
            Invalid = -1,
            None,
            AlwaysOnScreen,
            BossText,
            Built,
            WhisperingPearls,
            NearbyBossText
        }

        public static DisplayEffectID GetID(object obj)
        {
            if (obj is not DisplayEffect)
                return DisplayEffectID.Invalid;

            return obj switch
            {
                AlwaysOnScreen => DisplayEffectID.AlwaysOnScreen,
                BossText => DisplayEffectID.BossText,
                BuiltEffect => DisplayEffectID.Built,
                WhisperingPearlEffects => DisplayEffectID.WhisperingPearls,
                NearbyBossText => DisplayEffectID.NearbyBossText,
                _ => DisplayEffectID.None
            };
        }

        public static DisplayEffect GetEffect(DisplayEffectID id)
        {
            return id switch
            {
                DisplayEffectID.AlwaysOnScreen => new AlwaysOnScreen(),
                DisplayEffectID.BossText => new BossText(),
                DisplayEffectID.Built => new BuiltEffect(),
                DisplayEffectID.WhisperingPearls => new WhisperingPearlEffects(),
                DisplayEffectID.NearbyBossText => new NearbyBossText(),
                _ => new DisplayEffect()
            };
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                UI = new();
                State = new();
                State.Activate();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (UI?.CurrentState != null)
            {
                PlayerInput.SetZoom_World();
                UI.Update(gameTime);
                PlayerInput.SetZoom_Unscaled();
            }
        }

        public static Color GetColorFromHex(string hex)
        {
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml('#' + hex);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);
            return new Color(r, g, b);
        }

        /// <summary>
        /// Returns the slot of the first dialogue instance with the coorisponding name
        /// </summary>
        public static int GetSlot(string name)
        {
            foreach (var pair in WorldTextUI.Dialogues)
                if (pair.Value.name == name)
                    return pair.Key;
            return -1;
        }

        /// <summary>
        /// Manually progresses dialogue
        /// </summary>
        public static void ProgressDialogue(int slot, int newUptime = -2)
        {
            if (WorldTextUI.Dialogues.TryGetValue(slot, out var val))
            {
                TextDisplay display = val.ui;
                if (display.SwitchingPage)
                    return;

                // If the text crawl hasnt finished, finish it instantly
                if (display.textIndex < display.Text.Length - 1)
                    display.textIndex = display.Text.Length - 1;
                // If the text crawl has finished, progress to the next page or finish if we're out of pages
                else
                {
                    display.SwitchingPage = true;
                    if (newUptime != -2)
                    {
                        display.Uptime = newUptime;
                        val.upTime = newUptime;
                    }
                }
            }
        }

        /// <summary>
        /// Ends the dialogue if it exists in the world
        /// </summary>
        /// <param name="name">The name of the dialogue's localization key</param>
        public static void EndDialogue(int slot)
        {
            if (WorldTextUI.Dialogues.TryGetValue(slot, out var val))
                val.ui.ClosingDialogue = true;
        }

        public static void RemoveDialogue(int slot)
        {
            WorldTextUI.DialoguesToRemove.Add(slot);
        }

        public static bool ContainsDialogueKey(string key) => WorldTextUI.Dialogues.Any(d => d.Value.name == key);

        /// <summary>
        /// Creates a dialogue instance in the world
        /// </summary>
        /// <param name="name">The name of the dialogue's localization key</param>
        /// <param name="startPosition">The position of the text in the world</param>
        public static int StartDialogue(string name, Vector2 startPosition, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1, int toClient = -1, int ignoreClient = -1)
        {
            if (Main.dedServ)
            {
                AANet.SendNetMessage<StartDialogueDisplayPacket>(name, progressDialogue, startPosition, startIndex, Uptime, GetID(effects), wrapWidth, toClient, ignoreClient);
                return -1;
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                return StartDialogueOnClient(name, startPosition, startIndex, Uptime, progressDialogue, effects, wrapWidth);
            }

            return -1;
        }

        public static int StartDialogueOnClient(string name, Vector2 startPosition, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1)
        {
            if (Main.dedServ)
                return -1;

            UI ??= new();
            State ??= new();
            effects ??= new DisplayEffect();

            if (!DialogueLoader.TryGetTextData(name, out var textData))
            {
                AAMod.instance.Logger.Error($"Unable to find Dialogue Data for given name: '{name}'");
                return -1;
            }

            if (startIndex >= textData.PageCount)
                startIndex = textData.PageCount - 1;

            textData.Page = startIndex;

            TextDisplay display = new(textData.Pages[startIndex], effects, wrapWidth: wrapWidth)
            {
                Position = startPosition,
                ProgressDialogue = progressDialogue,
            };

            int slot;
            for (slot = 0; slot <= WorldTextUI.Dialogues.Count; slot++)
                if (!WorldTextUI.Dialogues.ContainsKey(slot))
                    break;

            WorldTextUI.Dialogues.Add(slot, (name, display, textData, null, Uptime));

            State.Append(display);
            display.Activate();

            if (UI.CurrentState != State)
                UI?.SetState(State);

            return slot;
        }

        /// <summary>
        /// Creates a dialogue instance in the world
        /// </summary>
        /// <param name="name">The name of the dialogue's localization key</param>
        /// <param name="entity">The entity this dialogue will appear with</param>
        /// <param name="Uptime">The entity this dialogue will appear with</param>
        public static int StartDialogue(string name, Entity entity, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1, int toClient = -1, int ignoreClient = -1)
        {
            if (Main.dedServ)
            {
                AANet.SendNetMessage<StartDialogueDisplayPacket>(name, progressDialogue, entity is NPC ? EntityType.NPC : entity is Player ? EntityType.Player : EntityType.Projectile, entity is Projectile p ? p.identity : entity.whoAmI, startIndex, Uptime, GetID(effects), wrapWidth, toClient, ignoreClient);
                return -1;
            }
            else if (Main.netMode == NetmodeID.SinglePlayer)
            {
                return StartDialogueOnClient(name, entity, startIndex, Uptime, progressDialogue, effects, wrapWidth);
            }

            return -1;
        }

        public static int StartDialogueOnClient(string name, Entity entity, int startIndex = 0, int Uptime = -1, bool progressDialogue = true, DisplayEffect effects = null, float wrapWidth = -1)
        {
            if (Main.dedServ)
                return -1;

            UI ??= new();
            State ??= new();
            effects ??= new DisplayEffect();

            if (!DialogueLoader.TryGetTextData(name, out var textData))
            {
                AAMod.instance.Logger.Error($"Unable to find Dialogue Data for given name: '{name}'");
                return -1;
            }

            if (startIndex >= textData.PageCount)
                startIndex = textData.PageCount - 1;

            TextDisplay display = new(textData[startIndex], effects, wrapWidth: wrapWidth)
            {
                Position = entity.Center,
                ProgressDialogue = progressDialogue,
            };

            int slot;
            for (slot = 0; slot <= WorldTextUI.Dialogues.Count; slot++)
                if (!WorldTextUI.Dialogues.ContainsKey(slot))
                    break;

            WorldTextUI.Dialogues.Add(slot, (name, display, textData, entity, Uptime));

            State.Append(display);
            display.Activate();

            if (UI.CurrentState != State)
                UI?.SetState(State);

            return slot;
        }
        public enum EntityType : byte
        {
            NPC,
            Player,
            Projectile
        }

        internal sealed class StartDialogueDisplayPacket : AAPacket
        {
            protected override void Write(BinaryWriter writer, object[] args)
            {
                if (args[2] is Vector2 vector)
                {
                    writer.Write((string)args[0]); //name
                    writer.WriteFlags((bool)args[1], false); //progressDialogue, hasEntity
                    writer.WritePackedVector2(vector); //position
                    writer.Write((int)args[3]); //index
                    writer.Write((int)args[4]); //uptime
                    writer.Write((byte)((DisplayEffectID)args[5])); //effect
                    writer.Write((float)args[6]); //wrapWidth
                }
                else
                {
                    writer.Write((string)args[0]); //name
                    writer.WriteFlags((bool)args[1], true); //progressDialogue, hasEntity
                    writer.Write((byte)((EntityType)args[2])); //type
                    writer.Write((int)args[3]); //entity
                    writer.Write((int)args[4]); //index
                    writer.Write((int)args[5]); //uptime
                    writer.Write((byte)((DisplayEffectID)args[6])); //effect
                    writer.Write((float)args[7]); //wrapWidth
                }
            }

            public override void HandlePacket(BinaryReader packet, int sender)
            {
                // Only receive info as clients

                string name = packet.ReadString();
                packet.ReadFlags(out bool progressDialogue, out bool hasEntity);

                int entity = -1;
                Vector2 pos = Vector2.Zero;
                EntityType type = EntityType.NPC;
                if (hasEntity)
                {
                    type = (EntityType)packet.ReadByte();
                    entity = packet.ReadInt32();
                }
                else
                    pos = packet.ReadPackedVector2();

                int index = packet.ReadInt32();
                int uptime = packet.ReadInt32();
                byte effect = packet.ReadByte();
                float wrapWidth = packet.ReadSingle();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    return;

                DisplayEffect de = GetEffect((DisplayEffectID)effect);

                if (hasEntity)
                {
                    Entity e = type switch
                    {
                        EntityType.NPC => Main.npc[entity],
                        EntityType.Player => Main.player[entity],
                        EntityType.Projectile => Main.projectile.FirstOrDefault(p => p.identity == entity),
                        _ => null
                    };

                    StartDialogueOnClient(name, e, index, uptime, progressDialogue, de, wrapWidth);
                }
                else
                    StartDialogueOnClient(name, pos, index, uptime, progressDialogue, de, wrapWidth);

            }
        }

        /// <summary>
        /// Resets all of the dialogue's variables
        /// </summary>
        public static void EndAllDialogue()
        {
            WorldTextUI.Dialogues.Clear();
            State.RemoveAllChildren();
            UI?.SetState(null);
        }
    }
}
