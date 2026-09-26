using AAModClassic.Dialogues;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Audio;
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
        internal Vector2 WorldCenter = Vector2.Zero;

        public override void OnInitialize()
        {
            dialogue = new(new(), new UIFadeIn(), screenLocked: true);

            var width = 640;
            var height = 300;
            Vector2 origin = Main.ScreenSize.ToVector2() / 2;
            var drawPos = new Vector2(origin.X - width / 2, origin.Y - height / 2);
            var bgRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, width, height);

            panel = new UIPanel();
            panel.Left.Pixels = bgRect.X;
            panel.Top.Pixels = bgRect.Y;
            panel.Width.Pixels = bgRect.Width;
            panel.Height.Pixels = bgRect.Height;

            Append(panel);

            panel.Append(dialogue);
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 halfSize = new(panel.Width.Pixels / 2f, panel.Height.Pixels / 2f);
            Vector2 screenPos = WorldCenter - Main.LocalPlayer.velocity - Main.screenPosition - halfSize;
            panel.Left.Pixels = screenPos.X;
            panel.Top.Pixels = screenPos.Y;
            panel.Width.Pixels = dialogue.TextSize.X;
            panel.Height.Pixels = dialogue.TextSize.Y;
            panel.Recalculate();

            dialogue.Position = panel.GetInnerDimensions().Position();

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
        private float _visibilityActive = 1f;
        private float _visibilityInactive = 0.4f;
        private bool _hovered;

        public ResponseButton(Asset<Texture2D> texture, Color hoverColor, Color unhoverColor)
        {
            _color = _unhoverColor = unhoverColor * _visibilityInactive;
            _hoverColor = hoverColor * _visibilityActive;
            _texture = texture;

            Width.Set(_texture.Width(), 0f);
            Height.Set(_texture.Height(), 0f);
        }

        public void SetImage(Asset<Texture2D> texture)
        {
            _texture = texture;
            Width.Set(_texture.Width(), 0f);
            Height.Set(_texture.Height(), 0f);
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
                _scale = MathHelper.Lerp(_previousScale, 1.5f, lerp);
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

            spriteBatch.Draw(_texture.Value, position, null, _color, 0f, _texture.Size() / 2f, _scale, SpriteEffects.None, 0f);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            SoundEngine.PlaySound(SoundID.MenuTick);
            _hovered = true;
            _hoverTimer = 0;
        }

        public void SetVisibility(float whenActive, float whenInactive)
        {
            _visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
            _visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
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
            if (!DialogueLoader.TryGetDialogue(name, out var textData))
            {
                AAMod.instance.Logger.Error($"Unable to find Dialogue Data for given name: '{name}'");
                return;
            }

            if (startIndex >= textData.PageCount)
                startIndex = textData.PageCount - 1;

            textData.Page = startIndex;

            State.WorldCenter = position;

            State.dialogue.ResetText(textData.Pages[startIndex]);

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
}
