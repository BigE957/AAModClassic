using AAModClassic.NPCs.Bosses.Akuma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using static Terraria.UI.UIElement;
using static Terraria.WorldGen;

namespace AAModClassic.UI.WorldGen
{
    public enum AAWorldType
    {
        Release,
        Beta,
        Mixed
    }

    public class WorldTypeSystem : ModSystem
    {
        private static AAWorldType _optionAAWorldType = AAWorldType.Release;
        public static AAWorldType WorldType => _optionAAWorldType;
        private static ModGroupOptionButton<AAWorldType>[] _aaWorldTypeButtons;

        public override void Load()
        {
            On_Main.UpdateUIStates += ModifyWorldGenMenu;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("AAWorldType", (int)WorldType);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            //Assume a world is a release world type, then get the tag if it exists for what it actually is.
            _optionAAWorldType = AAWorldType.Release;
            if (tag.TryGet<int>("AAWorldType", out int option))
                _optionAAWorldType = (AAWorldType)option;
        }

        private static bool perviousStateWorldCreation = false;

        private void ModifyWorldGenMenu(On_Main.orig_UpdateUIStates orig, GameTime gameTime)
        {
            orig(gameTime);

            int globalTime = (int)(Main.GlobalTimeWrappedHourly * 60);

            //AAMod.instance.Logger.Info("HEY");

            if (Main.MenuUI.CurrentState is UIWorldCreation worldCreation)
            {
                if (perviousStateWorldCreation)
                    return;

                perviousStateWorldCreation = true;
                _optionAAWorldType = AAWorldType.Release;

                UIElement baseElement = worldCreation.Children.First();
                UIElement[] baseChildren = baseElement.Children.ToArray();
                UIPanel worldGenPanel = (UIPanel)baseChildren[0];
                UITextPanel<LocalizedText> backButton = (UITextPanel<LocalizedText>)baseChildren[1];
                UITextPanel<LocalizedText> createButton = (UITextPanel<LocalizedText>)baseChildren[2];
                UIElement infoRack = worldGenPanel.Children.First().Children.First();
                UIText optionDesc = (UIText)infoRack.Children.First(e => e is UISlicedImage).Children.First();

                int num = 18;
                float defaultPanelHeight = 280f + (float)num;
                float defaultButtonTop = -45;
                worldGenPanel.Height = worldGenPanel.MaxHeight = StyleDimension.FromPixels(defaultPanelHeight + 48);
                backButton.Top = StyleDimension.FromPixels(defaultButtonTop + 48);
                createButton.Top = StyleDimension.FromPixels(defaultButtonTop + 48);

                UIHorizontalSeparator element = new UIHorizontalSeparator
                {
                    Width = StyleDimension.FromPercent(1f),
                    Top = StyleDimension.FromPixels(worldGenPanel.Height.Pixels - 20f - 48f),
                    Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
                };

                infoRack.Append(element);

                AddAAWorldOptions(worldCreation, optionDesc, infoRack, worldGenPanel.Height.Pixels - 20f - 48f - 40, ClickAAWorldTypeOption, "AAWorldType", 1f);
            }
            else
                perviousStateWorldCreation = false;
        }

        private void AddAAWorldOptions(UIWorldCreation worldCreation, UIText desc, UIElement container, float accumualtedHeight, MouseEvent clickEvent, string tagGroup, float usableWidthPercent)
        {
            AAWorldType[] array = new AAWorldType[3] {
                AAWorldType.Release,
                AAWorldType.Mixed,
                AAWorldType.Beta,
            };

            LocalizedText[] array2 = new LocalizedText[3] {
                Language.GetText("Mods.AAModClassic.UI.Release"),
                Language.GetText("Mods.AAModClassic.UI.Mixed"),
                Language.GetText("Mods.AAModClassic.UI.Beta")
            };

            LocalizedText[] array3 = new LocalizedText[3] {
                Language.GetText("Mods.AAModClassic.UI.AAWorldDescriptionRelease"),
                Language.GetText("Mods.AAModClassic.UI.AAWorldDescriptionMixed"),
                Language.GetText("Mods.AAModClassic.UI.AAWorldDescriptionBeta")
            };

            Color[] array4 = new Color[3] {
                Color.DeepSkyBlue,
                Color.Violet,
                Color.OrangeRed
            };

            string[] array5 = new string[3] {
                "AAModClassic/NPCs/Bosses/Yamata/YamataHead_Head_Boss",
                "AAModClassic/Items/Vanity/Mask/ShenAMask",
                "AAModClassic/Items/Vanity/Mask/AkumaMask"
            };

            ModGroupOptionButton<AAWorldType>[] array6 = new ModGroupOptionButton<AAWorldType>[array.Length];
            for (int i = 0; i < array6.Length; i++)
            {
                Vector2 iconOffset = i switch
                {
                    2 => new Vector2(4, 3),
                    1 => new Vector2(4, 3),
                    _ => new Vector2(4, 2)
                };

                ModGroupOptionButton<AAWorldType> groupOptionButton = new ModGroupOptionButton<AAWorldType>(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f, iconOffset);
                groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(-4 * (array6.Length - 1), 1f / (float)array6.Length * usableWidthPercent);
                groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
                groupOptionButton.HAlign = (float)i / (float)(array6.Length - 1);
                groupOptionButton.Top.Set(accumualtedHeight, 0f);
                groupOptionButton.OnLeftMouseDown += clickEvent;
                groupOptionButton.OnMouseOver += (_, _) => desc.SetText(groupOptionButton.Description);
                groupOptionButton.OnMouseOut += worldCreation.ClearOptionDescription;
                groupOptionButton.SetSnapPoint(tagGroup, i);
                groupOptionButton.SetCurrentOption(AAWorldType.Release);
                container.Append(groupOptionButton);
                array6[i] = groupOptionButton;
            }

            _aaWorldTypeButtons = array6;
        }

        private void ClickAAWorldTypeOption(UIMouseEvent evt, UIElement listeningElement)
        {
            ModGroupOptionButton<AAWorldType> groupOptionButton = (ModGroupOptionButton<AAWorldType>)listeningElement;
            _optionAAWorldType = groupOptionButton.OptionValue;
            ModGroupOptionButton<AAWorldType>[] sizeButtons = _aaWorldTypeButtons;
            for (int i = 0; i < sizeButtons.Length; i++)
            {
                sizeButtons[i].SetCurrentOption(groupOptionButton.OptionValue);
            }
        }
    }

    public class ModGroupOptionButton<T> : UIElement, IGroupOptionButton
    {
        private T _currentOption;
        private readonly Asset<Texture2D> _BasePanelTexture;
        private readonly Asset<Texture2D> _selectedBorderTexture;
        private readonly Asset<Texture2D> _hoveredBorderTexture;
        private readonly Asset<Texture2D> _iconTexture;
        private readonly T _myOption;
        private Color _color;
        private Color _borderColor;
        public float FadeFromBlack = 1f;
        private float _whiteLerp = 0.7f;
        private float _opacity = 0.7f;
        private Vector2 _iconOffset = Vector2.Zero;
        private bool _hovered;
        private bool _soundedHover;
        public bool ShowHighlightWhenSelected = true;
        private bool _UseOverrideColors;
        private Color _overrideUnpickedColor = Color.White;
        private Color _overridePickedColor = Color.White;
        private float _overrideOpacityPicked;
        private float _overrideOpacityUnpicked;
        public readonly LocalizedText Description;
        private UIText _title;

        public T OptionValue => _myOption;

        public bool IsSelected
        {
            get
            {
                if (_currentOption != null)
                    return _currentOption.Equals(_myOption);

                return false;
            }
        }

        public ModGroupOptionButton(T option, LocalizedText title, LocalizedText description, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f, Vector2? iconOffset = null)
        {
            _iconOffset = iconOffset ?? Vector2.Zero;
            _borderColor = Color.White;
            _currentOption = option;
            _myOption = option;
            Description = description;
            Width = StyleDimension.FromPixels(44f);
            Height = StyleDimension.FromPixels(34f);
            _BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
            _selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
            _hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
            if (iconTexturePath != null)
                _iconTexture = ModContent.Request<Texture2D>(iconTexturePath);

            _color = Colors.InventoryDefaultColor;
            if (title != null)
            {
                UIText uIText = new UIText(title, textSize)
                {
                    HAlign = titleAlignmentX,
                    VAlign = 0.5f,
                    Width = StyleDimension.FromPixelsAndPercent(0f - titleWidthReduction, 1f),
                    Top = StyleDimension.FromPixels(0f)
                };

                uIText.TextColor = textColor;
                Append(uIText);
                _title = uIText;
            }
        }

        public void SetText(LocalizedText text, float textSize, Color color)
        {
            if (_title != null)
                _title.Remove();

            UIText uIText = new UIText(text, textSize)
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
                Top = StyleDimension.FromPixels(0f)
            };

            uIText.TextColor = color;
            Append(uIText);
            _title = uIText;
        }

        public void SetCurrentOption(T option)
        {
            _currentOption = option;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (_hovered)
            {
                if (!_soundedHover)
                    SoundEngine.PlaySound(SoundID.MenuTick);

                _soundedHover = true;
            }
            else
            {
                _soundedHover = false;
            }

            CalculatedStyle dimensions = GetDimensions();
            Color color = _color;
            float num = _opacity;
            bool isSelected = IsSelected;
            if (_UseOverrideColors)
            {
                color = (isSelected ? _overridePickedColor : _overrideUnpickedColor);
                num = (isSelected ? _overrideOpacityPicked : _overrideOpacityUnpicked);
            }

            Utils.DrawSplicedPanel(spriteBatch, _BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color, FadeFromBlack) * num);
            if (isSelected && ShowHighlightWhenSelected)
                Utils.DrawSplicedPanel(spriteBatch, _selectedBorderTexture.Value, (int)dimensions.X + 7, (int)dimensions.Y + 7, (int)dimensions.Width - 14, (int)dimensions.Height - 14, 10, 10, 10, 10, Color.Lerp(color, Color.White, _whiteLerp) * num);

            if (_hovered)
                Utils.DrawSplicedPanel(spriteBatch, _hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, _borderColor);

            if (_iconTexture != null)
            {
                Color color2 = Color.White;
                if (!_hovered && !isSelected)
                    color2 = Color.Lerp(color, Color.White, _whiteLerp) * num;

                spriteBatch.Draw(_iconTexture.Value, new Vector2(dimensions.X + 1f, dimensions.Y + 1f) + _iconOffset, color2);
            }
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            base.LeftMouseDown(evt);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            _hovered = true;
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
            _hovered = false;
        }

        public void SetColor(Color color, float opacity)
        {
            _color = color;
            _opacity = opacity;
        }

        public void SetColorsBasedOnSelectionState(Color pickedColor, Color unpickedColor, float opacityPicked, float opacityNotPicked)
        {
            _UseOverrideColors = true;
            _overridePickedColor = pickedColor;
            _overrideUnpickedColor = unpickedColor;
            _overrideOpacityPicked = opacityPicked;
            _overrideOpacityUnpicked = opacityNotPicked;
        }

        public void SetBorderColor(Color color)
        {
            _borderColor = color;
        }
    }

}
