using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials
{
    public class GlowingMushium : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
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
