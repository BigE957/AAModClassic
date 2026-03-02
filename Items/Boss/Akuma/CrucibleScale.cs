using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace AAMod.Items.Boss.Akuma
{
    public class CrucibleScale : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crucible Scale");
            // Tooltip.SetDefault("The fury of the draconian sun eminates from this scale");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 4));
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 9;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.OrangeRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}