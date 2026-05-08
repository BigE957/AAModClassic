using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic._Content.PumpkinMoon.__Hardmode.Items.Currency
{
    public class HalloweenTreat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Halloween Treat");
            // Tooltip.SetDefault("A very tasty treat. Don't eat it though, most likely cursed.");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ItemRarityID.Yellow;
        }
    }
    public class HTreat : CustomCurrencySingleCoin
    {
        public static Color color = Color.Orange;

        public HTreat(int coinItemID) : base(coinItemID, 999L)
        {
        }

        public override void GetPriceText(string[] lines, ref int currentLine, long price)
        {
            Color color2 = color * (Main.mouseTextColor / 255f);
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
            {
                color2.R,
                color2.G,
                color2.B,
                Language.GetTextValue("Mods.AAModClassic.Common.PlayerBuyPrice"),
                price,
                price == 1 ? Language.GetTextValue("Mods.AAModClassic.Common.HalloweenTreat") : Language.GetTextValue("Mods.AAModClassic.Common.HalloweenTreats")
            });
        }
    }
}