using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.UI;

namespace AAModClassic.UI.Core
{
    public class ColoredFlavorTextBestiaryInfoElement : IBestiaryInfoElement, ICategorizedBestiaryInfoElement
    {
        private string _key;
        private Color _color;

        public UIBestiaryEntryInfoPage.BestiaryInfoCategory ElementCategory => UIBestiaryEntryInfoPage.BestiaryInfoCategory.FlavorText;

        public ColoredFlavorTextBestiaryInfoElement(string languageKey, Color color)
        {
            _key = languageKey;
            _color = color;
        }

        public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
        {
            if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
                return null;

            if (_key == "Mods.AAModClassic.Bestiary.Oblivion")
            {
                UIElement probablyInfoPage = Main.BestiaryUI.Children.ToArray()[0].Children.ToArray()[1].Children.ToArray()[1].Children.ToArray()[1];
                if (probablyInfoPage is UIBestiaryEntryInfoPage infoPage)
                {
                    UIList list = (UIList)infoPage.Children.ToArray()[0];
                    list._items[2].RemoveChild(list._items[2].Children.ToArray()[1]);
                    list._items[2].Append(GetOblivionStars());
                }
            }
            else if (_key == "Mods.AAModClassic.Bestiary.ZeroProtocol" && !Main.rand.NextBool(500)) //my spr is feeling very eatr
            {
                UIElement probablyInfoPage = Main.BestiaryUI.Children.ToArray()[0].Children.ToArray()[1].Children.ToArray()[1].Children.ToArray()[1];
                if (probablyInfoPage is UIBestiaryEntryInfoPage infoPage)
                {
                    infoPage.FillInfoForEntry(null, default(ExtraBestiaryInfoPageInformation));
                    var potentialEntries = Main.BestiaryDB.Entries.Where(e => e.UIInfoProvider.GetEntryUICollectionInfo().UnlockState >= BestiaryEntryUnlockState.CanShowStats_2).ToArray();
                    infoPage.FillInfoForEntry(potentialEntries[Main.rand.Next(potentialEntries.Length)], new ExtraBestiaryInfoPageInformation { BestiaryProgressReport = Main.BestiaryUI.GetUnlockProgress() });
                    return null;
                }
            }

            UIPanel obj = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
            {
                Width = new StyleDimension(-11f, 1f),
                Height = new StyleDimension(109f, 0f),
                BackgroundColor = new Color(43, 56, 101),
                BorderColor = Color.Transparent,
                Left = new StyleDimension(3f, 0f),
                PaddingLeft = 4f,
                PaddingRight = 4f
            };

            UIText uIText = new UIText(Language.GetText(_key), 0.8f)
            {
                HAlign = 0f,
                VAlign = 0f,
                Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
                IsWrapped = true,
                TextColor = _color,
                ShadowColor = _color.MultiplyRGB(Color.White * 0.333f)
            };

            AddDynamicResize(obj, uIText);
            obj.Append(uIText);
            return obj;
        }

        private UIElement GetOblivionStars()
        {
            int num = 14;
            int num2 = 14;
            int num3 = -4;
            int num4 = num + num3;
            int num5 = 18;
            int num6 = 18;
            int value = 18;// _filledStarsCount.Value;
            float num7 = 1f;
            int num8 = num4 * Math.Min(num6, num5) - num3;
            double num9 = (double)num4 * Math.Ceiling((double)num5 / (double)num6) - (double)num3;
            UIElement uIElement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", (AssetRequestMode)1), null, 5, 21)
            {
                Width = new StyleDimension((float)num8 + num7 * 2f, 0f),
                Height = new StyleDimension((float)num9 + num7 * 2f, 0f),
                BackgroundColor = Color.Gray * 0f,
                BorderColor = Color.Transparent,
                Left = new StyleDimension(10f, 0f),
                Top = new StyleDimension(6f, 0f),
                VAlign = 0f
            };
            uIElement.SetPadding(0f);
            for (int num10 = num5 - 1; num10 >= 0; num10--)
            {
                string text = "Images/UI/Bestiary/Icon_Rank_Light";
                if (num10 >= value)
                {
                    text = "Images/UI/Bestiary/Icon_Rank_Dim";
                }
                UIImage element = new UIImage(Main.Assets.Request<Texture2D>(text, (AssetRequestMode)1))
                {
                    Left = new StyleDimension((float)(num4 * (num10 % num6)) - (float)num8 * 0.5f + (float)num * 0.5f, 0f),
                    Top = new StyleDimension((float)(num4 * (num10 / num6)) - (float)num9 * 0.5f + (float)num2 * 0.5f, 0f),
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };
                uIElement.Append(element);
            }
            return uIElement;
        }

        private static void AddDynamicResize(UIElement container, UIText text)
        {
            text.OnInternalTextChange += delegate {
                container.Height = new StyleDimension(text.MinHeight.Pixels, 0f);
            };
        }
    }

}
