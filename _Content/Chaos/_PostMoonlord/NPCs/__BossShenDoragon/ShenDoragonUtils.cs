using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public class ShenDoragonUtils
    {
        public enum ChaosType
        {
            Inferno = 0,
            Mire = 1, 
            Discord = 2
        }

        public static bool AddShenCrossmodDialogue(string key, LocalizedText text, Func<bool> condition) => CrossModDialogue.TryAdd(key, new(text, condition));

        public static string GetCrossModDialogue()
        {
            List<LocalizedText> crossModText = [];
            foreach (var (text, condition) in CrossModDialogue.Values)
            {
                if (condition.Invoke())
                    crossModText.Add(text);
            }

            if (crossModText.Count > 0)
                return crossModText[Main.rand.Next(crossModText.Count)].Value;

            return Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.50.NoMod");
        }

        public static readonly Dictionary<string, (LocalizedText text, Func<bool> condition)> CrossModDialogue = [];
    }
}
