using AAModClassic._CrossMod;
using AAModClassic.Dialogues;
using AAModClassic.UI.Core;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI.Dialogue
{
    public class DialogueUI : UIState
    {
        // Elements
        internal UIPanel panel;
        internal TextDisplay dialogue;
        internal List<ResponseButton> responses = [];
        internal UIPanel responsePanel;
        internal TextDisplay responseText;

        // Data
        internal string treeName;
        internal int currentIndex = 0;
        internal Vector2 worldCenter = Vector2.Zero;
        private DialogueTree tree;

        public override void OnInitialize()
        {
            var width = 640;

            dialogue = new(new(), new DialogueUIEffect(), true, width);

            var height = 300;
            Vector2 origin = Main.ScreenSize.ToVector2() / 2;
            var drawPos = new Vector2(origin.X - width / 2, origin.Y - height / 2);
            var bgRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, width, height);

            panel = new UIPanel();
            panel.Left.Pixels = bgRect.X;
            panel.Top.Pixels = bgRect.Y;
            panel.Width.Pixels = bgRect.Width;
            panel.Height.Pixels = bgRect.Height;
            panel.BackgroundColor = Color.Transparent;
            panel.BorderColor = Color.Transparent;

            Append(panel);

            panel.Append(dialogue);
        }

        public override void OnActivate()
        {
            if (treeName == null)
                return;

            if (!DialogueLoader.TryGetDialogueTree(treeName, out tree))
            {
                AAMod.instance.Logger.Error($"Unable to find Dialogue Tree for given name: '{treeName}'");
                return;
            }

            if (currentIndex >= tree.Count)
                currentIndex = tree.Count - 1;

            UpdateDialogue();
        }

        public void OnResponsePress(int index)
        {
            var response = tree.Dialogues[currentIndex].Responses[index];

            currentIndex = response.Heading == -2 ? currentIndex + 1 : response.Heading;

            UpdateDialogue();
        }

        private void UpdateDialogue()
        {
            List<UIElement> toRemove = [];
            foreach (var child in panel.Children)
                if (child is ResponseButton)
                    toRemove.Add(child);

            foreach (var child in toRemove)
                panel.RemoveChild(child);

            responses.Clear();

            if(currentIndex == -1)
            {
                DialogueUISystem.EndDialogue();
                return;
            }

            dialogue.ResetText(tree.Dialogues[currentIndex].DialogueInfo);

            int count = tree.Dialogues[currentIndex].Responses.Length;

            for (int i = 0; i < count; i++)
            {
                var response = tree.Dialogues[currentIndex].Responses[i];
                var (texture, frame) = DialogueUISystem.ResponseIcons[response.Icon];
                ResponseButton button = new(response, texture, frame, Color.White, Color.White);
                int myIndex = i;
                button.OnLeftClick += (_, _) => OnResponsePress(myIndex);

                button.HAlign = (i + 1) / (float)(count + 1);
                button.VAlign = 0.5f + (MathF.Sin(button.HAlign * MathHelper.Pi) / 2f);

                panel.Append(button);
            }
        }

        public override void Update(GameTime gameTime)
        {
            panel.Width.Pixels = dialogue.TextSize.X;
            panel.Height.Pixels = dialogue.TextSize.Y * 2;

            Vector2 halfSize = new(panel.Width.Pixels / 2f, panel.Height.Pixels / 2f);
            Vector2 screenPos = worldCenter - Main.LocalPlayer.velocity - Main.screenPosition - halfSize;
            panel.Left.Pixels = screenPos.X;
            panel.Top.Pixels = screenPos.Y;
            
            panel.Recalculate();

            dialogue.Position = panel.GetInnerDimensions().Center() - dialogue.TextSize / 2f;

            base.Update(gameTime);
        }
    }

    public class ResponseButton : UIElement
    {
        private Asset<Texture2D> _texture;
        private Color _hoverColor;
        private Color _unhoverColor;
        private Color _color;
        private Color _previousColor;
        private float _scale;
        private float _previousScale;
        private byte _hoverTimer = 0;
        private Rectangle _frame;
        private static float _visibilityActive => 1f;
        private static float _visibilityInactive => 0.6f;
        private bool _hovered;
        private Response _response;

        public ResponseButton(Response response, Asset<Texture2D> texture, Rectangle frame, Color hoverColor, Color unhoverColor)
        {
            _response = response;

            _color = _unhoverColor = unhoverColor * _visibilityInactive;
            _hoverColor = hoverColor * _visibilityActive;
            
            _texture = texture;
            _frame = frame;

            Width.Set(_frame.Width, 0f);
            Height.Set(_frame.Height, 0f);
        }

        public void SetImage(Asset<Texture2D> texture, Rectangle frame)
        {
            _texture = texture;
            _frame = frame;

            Width.Set(_frame.Width, 0f);
            Height.Set(_frame.Height, 0f);
        }

        public void SetImageWithoutSettingSize(Asset<Texture2D> texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_hoverTimer == 0)
            {
                _previousColor = _color;
                _previousScale = _scale;
            }

            if (_hovered)
            {
                float lerp = MathUtils.SineInOutEasing(_hoverTimer / 30f);
                _color = Color.Lerp(_previousColor, _hoverColor * _visibilityActive, lerp);
                _scale = MathHelper.Lerp(_previousScale, 1f, lerp);

                Main.instance.MouseText(_response.Title);
            }
            else
            {
                float lerp = MathUtils.SineInOutEasing(_hoverTimer / 30f);
                _color = Color.Lerp(_previousColor, _unhoverColor * _visibilityInactive, lerp);
                _scale = MathHelper.Lerp(_previousScale, 0.75f, lerp);

            }

            if (_hoverTimer < 30)
                _hoverTimer++;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Vector2 position = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;

            spriteBatch.Draw(_texture.Value, position, _frame, _color, 0f, _frame.Size() / 2f, _scale, SpriteEffects.None, 0f);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            SoundEngine.PlaySound(SoundID.MenuTick);
            _hovered = true;
            _hoverTimer = 0;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
            _hovered = false;
            _hoverTimer = 0;
        }
    }

    public class DialogueUISystem : ModSystem
    {
        internal static DialogueUI State;

        internal static UserInterface UI;

        internal static Dictionary<string, (Asset<Texture2D> texture, Rectangle frame)> ResponseIcons = [];

        internal static bool Visible = false;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                UI = new();
                State = new();
                State.Activate();
            }
        }

        public override void PostSetupContent()
        {
            ResponseIcons.Add("Default", (TextureAssets.Item[ItemID.FallenStar], TextureAssets.Item[ItemID.FallenStar].Frame(1, 8)));
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int preInventory = layers.FindIndex(layer => layer.Name == "Vanilla: Interface Logic 2");
            if (preInventory != -1)
            {
                layers.Insert(preInventory, new LegacyGameInterfaceLayer("AAModClassic: Dialogue", () =>
                {
                    UI.Draw(Main.spriteBatch, new());
                    return true;
                }, InterfaceScaleType.Game));
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (UI?.CurrentState != null)
                UI?.Update(gameTime);
        }
    
        public static void StartDialogue(string name, int startIndex, Vector2 position)
        {
            State.worldCenter = position;
            State.treeName = name;
            State.currentIndex = startIndex;

            if (!Visible)
            {
                Visible = true;

                UI.SetState(State);

                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
        }

        public static void EndDialogue()
        {
            if (Visible)
            {
                Visible = false;

                UI.SetState(null);

                SoundEngine.PlaySound(SoundID.MenuClose);
            }
        }
    }

    [JsonConverter(typeof(SpeakerConverter))]
    public class Speaker(string name, string portrait = null)
    {
        public string Name { get; init; } = name;
        public string Portrait { get; set; } = portrait;
    }
    public class SpeakerConverter : JsonConverter<Speaker>
    {
        public override Speaker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                return new Speaker(reader.GetString());

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
                {
                    var root = doc.RootElement;

                    string name = root.TryGetProperty("Name", out var n) ? n.GetString() : string.Empty;
                    string portrait = root.TryGetProperty("Portrait", out var p) ? p.GetString() : null;

                    return new Speaker(name, portrait);
                }
            }

            throw new JsonException($"Unexpected token type {reader.TokenType} when parsing Speaker.");
        }

        public override void Write(Utf8JsonWriter writer, Speaker value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Name", value.Name);
            writer.WriteString("Portrait", value.Portrait);
            writer.WriteEndObject();
        }
    }

    public class DialogueTree
    {
        public string DefaultColor { get; init; }
        public Speaker DefaultSpeaker { get; init; }
        public int DefaultScale { get; init; }

        public Alignment AlignType { get; init; }
        public int TextDelay { get; init; }
        public PunctuationData BasePunctuationDelay { get; init; }
        public int PunctuationDelayCap { get; init; }
        public Dictionary<string, PunctuationData> PunctuationDelays { get; init; }

        public Dialogue[] Dialogues { get; set; }

        public int Revision { get; init; }

        public int Count => Dialogues.Length;

        [JsonConstructor]
        public DialogueTree(Dialogue[] Dialogues, string DefaultColor = null, Speaker DefaultSpeaker = null, int DefaultScale = 1, Alignment AlignType = 0, int TextDelay = 3, PunctuationData BasePunctuationDelay = null, int PunctuationDelayCap = 60, Dictionary<string, PunctuationData> PunctuationDelays = null, int Revision = 0)
        {
            this.Dialogues = Dialogues;
            this.DefaultColor = DefaultColor;
            this.DefaultSpeaker = DefaultSpeaker;
            this.DefaultScale = DefaultScale;
            this.AlignType = AlignType;
            this.TextDelay = TextDelay;
            this.BasePunctuationDelay = BasePunctuationDelay ?? new();
            this.PunctuationDelayCap = PunctuationDelayCap;
            this.PunctuationDelays = PunctuationDelays ?? [];
            this.Revision = Revision;

            foreach (Dialogue d in Dialogues)
            {
                DialoguePage p = d.DialogueInfo;
                if (p == null)
                    continue;

                p.BaseColor ??= this.DefaultColor;
                p.Speaker ??= this.DefaultSpeaker;
                if (p.TextScale == -1)
                    p.TextScale = this.DefaultScale;
                if (p.TextDelay == -1)
                    p.TextDelay = this.TextDelay;
                if (p.InPunctuationDelay == -1)
                    p.InPunctuationDelay = this.TextDelay;
                if (p.AlignType == Alignment.None)
                    p.AlignType = this.AlignType;
                p.BasePunctuationDelay ??= this.BasePunctuationDelay;
                if (p.PunctuationDelayCap == -1)
                    p.PunctuationDelayCap = this.PunctuationDelayCap;
                p.PunctuationDelays ??= this.PunctuationDelays;
            }
        }
    }

    public class Dialogue
    {
        public DialoguePage DialogueInfo { get; set; }
        public Response[] Responses { get; set; } = [];

        /// <summary>
        /// Make something happen once you close the dialogue
        /// </summary>
        public Action endEvent = null;

        public string PortraitKey = null;
    }

    public class Response
    {
        /// <summary>
        /// The title that appears for this response
        /// </summary>
        public string Title { get; set; } = "...";

        /// <summary>
        /// The name of the Icon used for this response
        /// </summary>
        public string Icon { get; set; } = "Default";

        /// <summary>
        /// The index within the DialogueTree that selecting this Response leads to.
        /// Setting this to -2 will have it lead to the index ahead of it.
        /// Setting this to -1 will have this response close the dialogue.
        /// </summary>
        public int Heading { get; set; } = -2;

        /// <summary>
        /// The key of the tree that selecting this response option will have the dialogue switch to
        /// The heading value will still determine what dialogue index is used, even in the new tree
        /// </summary>
        public string SwapTree { get; set; } = null;

        /// <summary>
        /// Dictates if this response can show up or not
        /// </summary>
        public bool Unlocked { get; set; } = true;

        /// <summary>
        /// When should the response unlock?
        /// </summary>
        public Func<bool> UnlockCondition = null;

        /// <summary>
        /// Make something happen once you select this response
        /// </summary>
        public Action selectEvent = null;

        /// <summary>
        /// Unlocks or locks the dialogue option
        /// </summary>
        /// <param name="status">Whether to unlock or lock the option</param>
        /// <returns></returns>
        public bool SetLockStatus(bool status = true)
        {
            Unlocked = status;
            return Unlocked;
        }
    }
}
