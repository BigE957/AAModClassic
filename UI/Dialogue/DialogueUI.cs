using AAModClassic.Dialogues;
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
using Terraria.GameInput;
using Terraria.Graphics.Effects;
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
        internal DialoguePortrait portrait;

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

            portrait = new(ModContent.Request<Texture2D>("AAModClassic/UI/Dialogue/Assets/Portraits/Portrait_Truffle_shimmer"));
            portrait.Top.Percent = PortraitTopPercent;

            ((DialogueUIEffect)dialogue.DisplayEffects).SceneOverlay = portrait.DrawImage;

            Append(panel);

            panel.Append(portrait);
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

            ChangeDialogue();

            (dialogue.DisplayEffects as DialogueUIEffect).ChangePosition = Vector2.Zero;
            (dialogue.DisplayEffects as DialogueUIEffect).ChangeSize = Vector2.Zero;
            dialogue.DialogueTimer = 0;
            portrait.Opacity = 0f;
        }

        public void OnResponsePress(int responseIndex)
        {
            var response = tree.Dialogues[currentIndex].Responses[responseIndex];
            SwitchDialogue(response.Heading == -2 ? currentIndex + 1 : response.Heading);

            for(int i = 0; i < responses.Count; i++)
            {
                int delta = Math.Abs(i - responseIndex);
                responses[i].Hide(delta * -5);
            }

        }

        private bool switchStarted = false;

        internal void SwitchDialogue(int newIndex)
        {
            currentIndex = newIndex;
            switchStarted = true;
            if (currentIndex == -1)
                dialogue.ClosingDialogue = true;
            else
            {
                dialogue.SwitchingPage = true;
                var nextDialogue = tree.Dialogues[currentIndex].DialogueInfo;

                Vector2 futureSize = dialogue.PredictTextSize(nextDialogue, out Vector2 futureOffset);
                Vector2 futurePosition = dialogue.Position + (dialogue.TextSize - futureSize) / 2f;
                Vector2 futurePageTop = dialogue.DisplayEffects.TextOffsetFromStart(futurePosition, futureSize) - futureOffset;
                Vector2 currentPageTop = dialogue.DisplayEffects.TextOffsetFromStart(dialogue.Position, dialogue.TextSize) - dialogue.SizeOffsetFromStart;

                var effect = (DialogueUIEffect)dialogue.DisplayEffects;
                effect.ChangePosition = futurePageTop - currentPageTop;
                effect.ChangeSize = futureSize - dialogue.TextSize;
            }
        }

        private void ChangeDialogue()
        {
            List<UIElement> toRemove = [];
            foreach (var child in panel.Children)
                if (child is ResponseButton)
                    toRemove.Add(child);

            foreach (var child in toRemove)
                panel.RemoveChild(child);

            responses.Clear();

            crawlOver = false;
            switchStarted = false;

            if (currentIndex == -1)
            {
                DialogueUISystem.EndDialogue();
                return;
            }

            var effect = (DialogueUIEffect)dialogue.DisplayEffects;
            bool isPageSwitch = dialogue.SwitchingPage;   // must be read before ResetText clears it

            dialogue.ResetText(tree.Dialogues[currentIndex].DialogueInfo);

            effect.ChangePosition = Vector2.Zero;
            effect.ChangeSize = Vector2.Zero;
            effect.SkipIntroFade = isPageSwitch;

            int count = tree.Dialogues[currentIndex].Responses.Length;

            for (int i = 0; i < count; i++)
            {
                var response = tree.Dialogues[currentIndex].Responses[i];
                var (texture, frame) = DialogueUISystem.ResponseIcons[response.Icon];
                ResponseButton button = new(response, texture, frame, Color.White, Color.White, 1.5f);
                int myIndex = i;
                button.OnLeftClick += (_, _) => OnResponsePress(myIndex);

                float xAlign = (i + 1) / (float)(count + 1);
                button.idealAligns = new(xAlign, 0.5f + (MathF.Sin(xAlign * MathHelper.Pi) / 2f));
                button.HAlign = 0.5f;
                button.VAlign = 0.5f;

                panel.Append(button);

                responses.Add(button);
            }
        }

        bool crawlOver = false;

        private static float PortraitBaseLeft => 24f;
        private static float PortraitBaseTop => -130f;
        private static float PortraitTopPercent => 0.25f;

        private void UpdatePortrait()
        {
            Vector2 size = dialogue.TextSize;
            Vector2 virtualSize = size;

            if (switchStarted && dialogue.SwitchingPage && !dialogue.ClosingDialogue)
            {
                var effect = (DialogueUIEffect)dialogue.DisplayEffects;
                virtualSize += effect.ChangeSize * DialogueUIEffect.SwitchProgress(dialogue.SwitchCounter);
            }

            float widthDiff = size.X - virtualSize.X;
            float heightDiff = (size.Y - virtualSize.Y) * 2f;   // panel height is TextSize.Y * 2

            portrait.Left.Pixels = PortraitBaseLeft + widthDiff / 2f;
            portrait.Top.Pixels = PortraitBaseTop + (0.5f - PortraitTopPercent) * heightDiff;

            if (dialogue.DialogueTimer < 30f && !(dialogue.DisplayEffects as DialogueUIEffect).SkipIntroFade)
            {
                portrait.Opacity = MathUtils.CircOutEasing(dialogue.DialogueTimer / 30f);

                portrait.Top.Pixels += 48 * (1 - portrait.Opacity);
            }
            else if (dialogue.SwitchCounter > 0 && dialogue.ClosingDialogue)
            {
                portrait.Opacity = 1 - MathHelper.Clamp(MathUtils.SineInEasing(dialogue.SwitchCounter / 20f), 0f, 1f);

                portrait.Top.Pixels += 48 * MathHelper.Clamp(MathUtils.SineInEasing(dialogue.SwitchCounter / 30f), 0f, 1f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (switchStarted)
            {
                dialogue.SwitchCounter++;
                if (dialogue.SwitchCounter >= 30)
                    ChangeDialogue();
            }

            panel.Width.Pixels = dialogue.TextSize.X;
            panel.Height.Pixels = dialogue.TextSize.Y * 2;

            Vector2 halfSize = new(panel.Width.Pixels / 2f, panel.Height.Pixels / 2f);
            Vector2 cameraPosition = Main.screenPosition;
            Vector2 screenPos = worldCenter - cameraPosition - halfSize;

            panel.Left.Pixels = screenPos.X;
            panel.Top.Pixels = screenPos.Y;

            UpdatePortrait();

            panel.Recalculate();

            dialogue.Position = panel.GetInnerDimensions().Center() - dialogue.TextSize / 2f;

            if (!crawlOver && !dialogue.Crawling)
            {
                for (int i = 0; i < responses.Count; i++)
                    responses[i].Show(i * -10);

                crawlOver = true;
            }

            base.Update(gameTime);

            if (panel.ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;
        }
    }

    public class ResponseButton : UIElement
    {
        private Response _response;

        private Asset<Texture2D> _texture;
        private Rectangle _frame;
        private float _baseScale;
        private Color _hoverColor;
        private Color _unhoverColor;
        private Color _color;
        private Color _previousColor;
        private float _scale;
        private float _previousScale;

        private byte _hoverTimer = 30;
        private static float visibilityActive => 1f;
        private static float visibilityInactive => 0.8f;
        private bool _hovered = false;

        private bool show = false;
        private int showTimer = 0;
        private bool hide = false;
        private int hideTimer = 0;
        internal Vector2 idealAligns = Vector2.One * 0.5f;


        public ResponseButton(Response response, Asset<Texture2D> texture, Rectangle frame, Color hoverColor, Color unhoverColor, float baseScale = 1f)
        {
            _response = response;

            _color = _unhoverColor = unhoverColor * visibilityInactive;
            _hoverColor = hoverColor * visibilityActive;
            
            _texture = texture;
            _frame = frame;
            _baseScale = baseScale;

            Width.Set(_frame.Width * _baseScale, 0f);
            Height.Set(_frame.Height * _baseScale, 0f);
        }

        public void SetImage(Asset<Texture2D> texture, Rectangle frame, float baseScale = 1f)
        {
            _texture = texture;
            _frame = frame;
            _baseScale = baseScale;

            Width.Set(_frame.Width * _baseScale, 0f);
            Height.Set(_frame.Height * _baseScale, 0f);
        }

        public void SetImageWithoutSettingSize(Asset<Texture2D> texture)
        {
            _texture = texture;
        }

        public void Show(int time)
        {
            show = true;
            showTimer = time;
        }

        public void Hide(int time)
        {
            hide = true;
            hideTimer = time;

            _previousColor = _color;
            _previousScale = _scale;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if(hide)
            {
                if (hideTimer > 18)
                    return;

                hideTimer++;

                if (hideTimer >= 0)
                {
                    float lerp = MathUtils.SineOutEasing(hideTimer / 18f);
                    HAlign = MathHelper.Lerp(idealAligns.X, (0.5f + idealAligns.X) / 2f, lerp);
                    VAlign = MathHelper.Lerp(idealAligns.Y, 0.5f, lerp);

                    _scale = MathHelper.Lerp(_previousScale, 0.1f, lerp);
                    _color = Color.Lerp(_previousColor, Color.Transparent, lerp);
                }
            }
            else if (showTimer == 30)
            {
                HAlign = idealAligns.X;
                VAlign = idealAligns.Y;

                if (_hoverTimer == 0)
                {
                    _previousColor = _color;
                    _previousScale = _scale;
                }

                if (_hovered)
                {
                    float lerp = MathUtils.SineInOutEasing(_hoverTimer / 18f);
                    _color = Color.Lerp(_previousColor, _hoverColor * visibilityActive, lerp);
                    _scale = MathHelper.Lerp(_previousScale, 1f, lerp);

                    Main.instance.MouseText(_response.Title);
                }
                else
                {
                    float lerp = MathUtils.SineInOutEasing(_hoverTimer / 18f);
                    _color = Color.Lerp(_previousColor, _unhoverColor * visibilityInactive, lerp);
                    _scale = MathHelper.Lerp(_previousScale, 0.75f, lerp);

                }

                if (_hoverTimer < 18)
                    _hoverTimer++;
            }
            else if (show)
            {
                showTimer++;

                if (showTimer >= 0)
                {
                    float lerp = MathUtils.SineOutEasing(showTimer / 30f);
                    HAlign = MathHelper.Lerp((0.5f + idealAligns.X) / 2f, idealAligns.X, lerp);
                    VAlign = MathHelper.Lerp(0.5f, idealAligns.Y, lerp);

                    _previousScale = _scale = MathHelper.Lerp(0.1f, 0.75f, lerp);
                    _previousColor = _color = Color.Lerp(Color.Transparent, _unhoverColor * visibilityInactive, lerp);
                }
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Vector2 position = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;

            Color drawColor = _color;
            if(showTimer < 30)
                drawColor *= MathUtils.SineOutEasing(showTimer / 30f);
            spriteBatch.Draw(_texture.Value, position, _frame, drawColor, 0f, _frame.Size() / 2f, _scale * _baseScale, SpriteEffects.None, 0f);
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

    public class DialoguePortrait : UIElement
    {
        private Asset<Texture2D> _texture;
        public float ImageScale = 1f;
        public float Opacity = 1f;
        public float Rotation;
        public bool ScaleToFit;
        public bool AllowResizingDimensions = true;
        public Color Color = Color.White;
        public Vector2 NormalizedOrigin = Vector2.Zero;
        public bool RemoveFloatingPointsFromDrawPosition;
        private Texture2D _nonReloadingTexture;

        public DialoguePortrait(Asset<Texture2D> texture)
        {
            SetImage(texture);
        }

        public DialoguePortrait(Texture2D nonReloadingTexture)
        {
            SetImage(nonReloadingTexture);
        }

        public void SetImage(Asset<Texture2D> texture)
        {
            _texture = texture;
            _nonReloadingTexture = null;
            if (AllowResizingDimensions)
            {
                Width.Set(_texture.Width(), 0f);
                Height.Set(_texture.Height(), 0f);
            }
        }

        public void SetImage(Texture2D nonReloadingTexture)
        {
            _texture = null;
            _nonReloadingTexture = nonReloadingTexture;
            if (AllowResizingDimensions)
            {
                Width.Set(_nonReloadingTexture.Width, 0f);
                Height.Set(_nonReloadingTexture.Height, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) => DrawImage(spriteBatch);

        internal void DrawImage(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Texture2D texture2D = _texture != null ? _texture.Value : _nonReloadingTexture;

            spriteBatch.End(out var snap);
            var shaderSnap = snap;

            Effect effect = Filters.Scene["AAModClassic:VerticalFade"].GetShader().Shader;
            effect.Parameters["fadeStart"].SetValue(0.75f);

            shaderSnap.SortMode = SpriteSortMode.Immediate;
            shaderSnap.CustomEffect = effect;
            spriteBatch.Begin(shaderSnap);

            if (ScaleToFit)
                spriteBatch.Draw(texture2D, dimensions.ToRectangle(), Color * Opacity);
            else
            {
                Vector2 vector = texture2D.Size();
                Vector2 vector2 = dimensions.Position() + vector * (1f - ImageScale) / 2f + vector * NormalizedOrigin;
                if (RemoveFloatingPointsFromDrawPosition)
                    vector2 = vector2.Floor();

                spriteBatch.Draw(texture2D, vector2, null, Color * Opacity, Rotation, vector * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(snap);
        }
    }

    [Autoload(Side = ModSide.Client)]
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

        public override void Unload() => DialogueSceneCapture.Dispose();

        public override void PostSetupContent()
        {
            ResponseIcons.Add("Default", (TextureAssets.Item[ItemID.FallenStar], TextureAssets.Item[ItemID.FallenStar].Frame(1, 8)));
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(0, new LegacyGameInterfaceLayer("AAModClassic: Dialogue", () =>
            {
                UI.Draw(Main.spriteBatch, new());
                return true;
            }, InterfaceScaleType.Game));
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

        public static void CloseDialogue()
        {
            State.SwitchDialogue(-1);
            for (int i = 0; i < State.responses.Count; i++)
                State.responses[i].Hide(i * -5);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
        
        public static void EndDialogue()
        {
            if (Visible)
            {
                Visible = false;

                UI.SetState(null);
            }
        }

        internal static class DialogueSceneCapture
        {
            private static RenderTarget2D target;

            public static RenderTarget2D Capture(SpriteBatch sb, RenderTarget2D source, Action<SpriteBatch> overlay, SpriteBatchSnapshot uiState)
            {
                GraphicsDevice gd = Main.instance.GraphicsDevice;

                if (target == null || target.IsDisposed || target.Width != source.Width || target.Height != source.Height)
                {
                    target?.Dispose();
                    target = new RenderTarget2D(gd, source.Width, source.Height, false, source.Format, DepthFormat.None);
                }

                RenderTargetBinding[] previousTargets = gd.GetRenderTargets();
                Viewport previousViewport = gd.Viewport;

                gd.SetRenderTarget(target);
                gd.Clear(Color.Transparent);

                var copySnap = uiState;
                copySnap.SortMode = SpriteSortMode.Deferred;
                copySnap.BlendState = BlendState.Opaque;
                copySnap.CustomEffect = null;
                copySnap.TransformMatrix = Matrix.Identity;
                sb.Begin(copySnap);
                sb.Draw(source, Vector2.Zero, Color.White);
                sb.End();

                if (overlay != null)
                {
                    sb.Begin(uiState);
                    overlay(sb);
                    sb.End();
                }

                gd.SetRenderTargets(previousTargets);
                gd.Viewport = previousViewport;

                if (previousTargets.Length == 0)
                {
                    var restoreSnap = uiState;
                    restoreSnap.SortMode = SpriteSortMode.Deferred;
                    restoreSnap.BlendState = BlendState.Opaque;
                    restoreSnap.CustomEffect = null;
                    restoreSnap.TransformMatrix = Matrix.Identity;
                    sb.Begin(restoreSnap);
                    sb.Draw(target, Vector2.Zero, Color.White);
                    sb.End();
                }

                return target;
            }

            public static void Dispose()
            {
                Main.QueueMainThreadAction(() =>
                {
                    target?.Dispose();
                    target = null;
                });
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
