using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Currency
{
    public class AncientCoin : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Currency";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Coin");
            // Tooltip.SetDefault("A red and blue coin with an A engraved into it");
            Item.ResearchUnlockCount = 50;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightPurple;
        }
    }

    public class ACoin(int coinItemID) : CustomCurrencySingleCoin(coinItemID, 999L)
    {
        public static Color color = Color.LightBlue;

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
                price == 1 ? Language.GetTextValue("Mods.AAModClassic.Common.AncientCoin") : Language.GetTextValue("Mods.AAModClassic.Common.AncientCoins")
            });
        }
    }
}