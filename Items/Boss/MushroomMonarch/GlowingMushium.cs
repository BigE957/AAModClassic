
using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class GlowingMushium : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.rare = 1;
            Item.value = Item.sellPrice(0, 0, 3, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowing Mushium");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.White, lightColor, lightColor, Color.White);
        }
    }
}
