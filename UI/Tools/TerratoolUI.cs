using AAModClassic.UI.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace AAModClassic.UI.Tools
{
    public abstract class TerratoolUI : ToggableUI
    {
        private static bool onTerratoolMenu = false;

        private List<UIColorImageButton> buttonList;
        private List<UIColorImage> buttonImageList;
        private Rectangle buttonFrontFrame;
        private static Vector2 circleCenter;
        public List<int> selectedButtons;

        public abstract UIState State { get; }
        public abstract Asset<Texture2D> ButtonImages { get; }
        public abstract Asset<Texture2D> ButtonOnImage { get; }
        public abstract Asset<Texture2D> ButtonOffImage { get; }
        public abstract int HeldItemType { get; }

        public virtual UserInterface Interface => AAMod.instance.TerratoolInterface;

        public virtual Color HoverColor => new Color(150, 150, 150);

        public virtual Color NoHoverColor => new Color(80, 80, 80);

        public virtual float ButtonPadding => 5f;

        public virtual float CircleRadius => 40f + ButtonPadding;

        public virtual int ButtonAmount => 3;

        public override void OnInitialize()
        {
            buttonList = [];
            buttonImageList = [];
            selectedButtons = [ 0 ];
            buttonFrontFrame = ButtonImages.Frame(1, ButtonAmount);
            circleCenter = Main.MouseScreen - new Vector2(20, 20);

            for (int i = 0; i < ButtonAmount; i++)
            {
                buttonList.Add(new UIColorImageButton(ButtonOffImage, (i == 0) ? Color.White : NoHoverColor));
                buttonImageList.Add(new UIColorImage(ButtonImages, (i == 0) ? Color.White : NoHoverColor, buttonFrontFrame));

                double angle = Math.PI * 2 * i / ButtonAmount;
                double x = circleCenter.X + (CircleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (CircleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);

                buttonFrontFrame.Y += buttonFrontFrame.Height;

                int index = i;
                buttonList[i].OnLeftClick += ButtonClicked;
                buttonList[i].OnMouseOver += ButtonHover;
                buttonList[i].OnMouseOut += ButtonLeave;

                buttonList[i].Append(buttonImageList[i]);
                Append(buttonList[i]);
            }
        }

        public override void OnActivate()
        {
            for (int index = 0; index < ButtonAmount; index++)
            {
                buttonImageList[index].SetFrame(ButtonImages.Frame(1, ButtonAmount, 0, index));
                buttonList[index].Width.Set(ButtonOffImage.Width(), 0);
                buttonList[index].Height.Set(40, 0);
            }
        }

        public override void ToggleUI(UserInterface userInterface, UIState state = null)
        {
            base.ToggleUI(userInterface, state);

            if (Visible)
            {
                PlayerInput.SetZoom_UI();
                UpdatePosition();
                PlayerInput.SetZoom_Unscaled();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Main.LocalPlayer.HeldItem.type != HeldItemType || Main.LocalPlayer.dead || Main.LocalPlayer.ghost || ((Main.LocalPlayer.mouseInterface || Main.LocalPlayer.lastMouseInterface) && !onTerratoolMenu))
            {
                for (int i = 0; i < buttonList.Count; i++)
                {
                    buttonList[i].SetImage(ButtonOffImage);

                    if (!selectedButtons.Contains(i))
                    {
                        buttonList[i].SetColor(NoHoverColor);
                        buttonImageList[i].SetColor(NoHoverColor);
                    }
                }

                base.ToggleUI(Interface, State);
            }

            onTerratoolMenu = false;

            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].IsMouseHovering || buttonImageList[i].IsMouseHovering)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    onTerratoolMenu = true;
                }
            }
        }

        public virtual void ButtonClicked(UIMouseEvent evt, UIElement element)
        {
            int index = -1;
            for(int i = 0; i < ButtonAmount; i++)
                if (element == buttonList[i])
                    index = i;

            if (index < 0)
                return;

            if (selectedButtons.Count == 2)
            {
                selectedButtons.RemoveAt(1);
            }
            selectedButtons.Insert(0, index);

            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].SetColor(NoHoverColor);
                buttonImageList[i].SetColor(NoHoverColor);
            }

            foreach (int value in selectedButtons)
            {
                buttonList[value].SetColor(Color.White);
                buttonImageList[value].SetColor(Color.White);
            }
        }

        public virtual void ButtonHover(UIMouseEvent evt, UIElement element)
        {
            int index = -1;
            for (int i = 0; i < ButtonAmount; i++)
                if (element == buttonList[i])
                    index = i;

            buttonList[index].SetImage(ButtonOnImage);

            if (!selectedButtons.Contains(index))
            {
                buttonList[index].SetColor(HoverColor);
                buttonImageList[index].SetColor(HoverColor);
            }
        }

        public virtual void ButtonLeave(UIMouseEvent evt, UIElement element)
        {
            int index = -1;
            for (int i = 0; i < ButtonAmount; i++)
                if (element == buttonList[i])
                    index = i;

            buttonList[index].SetImage(ButtonOffImage);

            if (!selectedButtons.Contains(index))
            {
                buttonList[index].SetColor(NoHoverColor);
                buttonImageList[index].SetColor(NoHoverColor);
            }
        }

        public virtual void UpdatePosition()
        {
            circleCenter = Main.MouseScreen - new Vector2(20f, 20f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                double angle = Math.PI * 2 * i / ButtonAmount;

                double x = circleCenter.X + (CircleRadius * Math.Cos(angle));
                double y = circleCenter.Y + (CircleRadius * Math.Sin(angle));

                buttonList[i].Left.Set((float)x, 0f);
                buttonList[i].Top.Set((float)y, 0f);
            }

            Recalculate();
        }
    }
}
