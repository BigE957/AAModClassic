using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Materials.Mushrooms
{
    public class RainbowMushroom : BaseAAItem
    {
        public override Color GlowmaskDrawColor => Main.DiscoColor;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rainbow Mushroom");
            // Tooltip.SetDefault(@"You're not really sure if it's colorful naturally or because you're high.");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
            Item.expert = true; Item.expertOnly = true;
            Item.value = Item.sellPrice(0, 0, 0, 0);
        }
    }
}
