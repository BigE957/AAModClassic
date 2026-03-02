using Terraria;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Materials
{
    public class Prism : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prism");
            // Tooltip.SetDefault("Shines with the colors of all the gems");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
			Item.maxStack = 99;
            Item.rare = 3;
            Item.value = 1000;
        }
    }
}
