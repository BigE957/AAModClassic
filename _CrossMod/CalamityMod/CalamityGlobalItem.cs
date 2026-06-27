using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace AAModClassic._CrossMod.CalamityMod
{
    public class CalamityGlobalItem : GlobalItem
    {
        public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
        {
            if (!CalamityMod.IsEnabled)
                return true;

            if (item.ModItem == null)
                return true;

            if (item.ModItem.Mod.Name != CalamityMod.Calamity.Name)
                return true;

            if (item.ModItem.Name != "LoreAwakening")
                return true;

            if (line.Name == "CalamityMod:HoldShiftTooltip")
            {
                string replacementText = Language.GetTextValue("Mods.AAModClassic.CrossMod.CalamityMod.AwakeningReplacement");
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, replacementText, new Vector2(line.X, line.Y), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
                return false;
            }

            return true;
        }
    }
}
