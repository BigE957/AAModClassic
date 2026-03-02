using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic.Items.Currency
{
    public class AncientCoin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Coin");
            // Tooltip.SetDefault("A red and blue coin with an A engraved into it");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.LightPurple;
        }
    }

    public class ACoin : CustomCurrencySingleCoin
    {
        public static Color color = Color.LightBlue;

        public ACoin(int coinItemID) : base(coinItemID, 999L)
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
                Language.GetTextValue("Mods.AAMod.Common.PlayerBuyPrice"),
                price,
                price == 1 ? Language.GetTextValue("Mods.AAMod.Common.AncientCoin") : Language.GetTextValue("Mods.AAMod.Common.AncientCoins")
            });
        }
    }
}