using Microsoft.Xna.Framework;
using Steamworks;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace AAModClassic.Utilities
{
    public static class ChatUtils
    {
        public static void Chat(string s, Color color, bool sync = true)
        {
            Chat(s, color.R, color.G, color.B, sync);
        }

        public static void Chat(string s, byte colorR = 255, byte colorG = 255, byte colorB = 255, bool sync = true)
        {
            if (!AAConfigClient.Instance.NoBossDialogue)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(s, colorR, colorG, colorB);
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                    Main.NewText(s, colorR, colorG, colorB);
                else if (sync && Main.netMode == NetmodeID.Server)
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), -1);
            }
        }

        // copied from the internet https://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-with-maximum-performance
        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

        /// <summary>
        /// for valid inputs refer to Reset() in PlayerInput
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetVanillaKeybindGlyph(string input)
        {
            InputMode mode = PlayerInput.CurrentInputMode;
            if (mode == InputMode.Mouse || mode == InputMode.KeyboardUI)
                mode = InputMode.Keyboard;
            else if (mode == InputMode.XBoxGamepadUI)
                mode = InputMode.XBoxGamepad;

            if (!PlayerInput.CurrentProfile.InputModes[mode].KeyStatus.ContainsKey(input))
                return "get a proper input moron";

            return PlayerInput.CurrentProfile.InputModes[mode].KeyStatus[input][0];
        }

        public static string GetFormattedListOfStrings(List<string> nameList, bool useAndSign = false)
        {
            string text = "";
            string and = useAndSign ? "& " : "and ";

            if (nameList.Count == 1)
                return nameList[0];

            for (int i = 0; i < nameList.Count; i++)
            {
                string buffName = nameList[i];

                if (i != nameList.Count - 1)
                {
                    text += buffName;
                    if (i == nameList.Count - 2)
                        text += " ";
                    else
                        text += ", ";
                }
                else if (nameList.Count > 1)
                    text += and + buffName;
                else
                    text += buffName;
            }

            return text;
        }

        public enum IncreaseDecreaseTextType
        {
            IncreasesDecreases = 0,
            IncreasedDecreased = 1,
            IncreaseDecrease = 2,
            MoreLess = 3
        }

        public static string IncreaseOrDecreaseText(int number, IncreaseDecreaseTextType textType = IncreaseDecreaseTextType.IncreasedDecreased, bool reduced = false)
        {
            if (number >= 0)
            {
                if (textType == IncreaseDecreaseTextType.IncreasesDecreases)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Increases");
                else if (textType == IncreaseDecreaseTextType.IncreasedDecreased)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Increased");
                else if (textType == IncreaseDecreaseTextType.IncreaseDecrease)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Increase");
                else if (textType == IncreaseDecreaseTextType.MoreLess)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.More");
            }
            else
            {
                if (reduced)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Reduced");

                if (textType == IncreaseDecreaseTextType.IncreasesDecreases)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Decreases");
                else if (textType == IncreaseDecreaseTextType.IncreasedDecreased)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Decreased");
                else if (textType == IncreaseDecreaseTextType.IncreaseDecrease)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Decrease");
                else if (textType == IncreaseDecreaseTextType.MoreLess)
                    return Language.GetTextValue("Mods.AAModClassic.EquipStats.ClassGlobalStats.StatModifier.Less");
            }

            return "big mistake occurred in getting increase or decrease text type";
        }
        public static string IncreaseOrDecreaseText(float number, IncreaseDecreaseTextType textType = IncreaseDecreaseTextType.IncreasedDecreased, bool reduced = false)
        {
            return IncreaseOrDecreaseText((int)number, textType, reduced);
        }
        public static string IncreaseOrDecreaseText(double number, IncreaseDecreaseTextType textType = IncreaseDecreaseTextType.IncreasedDecreased, bool reduced = false)
        {
            return IncreaseOrDecreaseText((int)number, textType, reduced);
        }

        public static string GetDamageTypeName(DamageClass damageType)
        {
            return Language.GetTextValue($"Mods.AAModClassic.EquipStats.ClassGlobalStats.{damageType.Name}");
        }

        public static double GetDisplayNumber(float input, bool absolute = true)
        {
            float input2 = input + 0.0000001f;
            if (absolute)
                return Math.Abs(Math.Round(input2, 4, MidpointRounding.AwayFromZero));
            return Math.Round(input2, 4, MidpointRounding.AwayFromZero);
        }
    }
}
