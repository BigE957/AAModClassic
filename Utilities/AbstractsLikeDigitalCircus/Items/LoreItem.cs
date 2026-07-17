using AAModClassic._CrossMod.CalamityMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public abstract class LoreItem : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Lore.Calamity";

        public override bool IsLoadingEnabled(Mod mod) => CalamityMod.IsEnabled;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = tooltips.Last();
            if (!Main.keyState.PressingShift())
            {
                if (line != null)
                    line.Text = CalamityMod.IsEnabled ? Language.GetTextValue("Mods.CalamityMod.Items.Lore.ShortTooltip") : Language.GetTextValue("Mods.AAModClassic.CrossMod.CalamityMod.LoreItemTooltip");
                return;
            }
            //tooltips.RemoveRange(1, tooltips.Count - 1);
            //tooltips.RemoveAt(2);
            tooltips.Add(new(AAMod.instance, "LoreTab", Language.GetTextValue("Mods.AAModClassic." + LocalizationCategory + "." + Name + ".Lore")));
        }

        public override bool CanUseItem(Player player) => false;

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = (ContentSamples.CreativeHelper.ItemGroup)12000; //This is the value calamity lore items get given
        }
    }
}
