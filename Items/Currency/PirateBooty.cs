using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace AAModClassic.Items.Currency
{
    public class PirateBooty : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pirate's Booty");
            // Tooltip.SetDefault("An exceedingly well-crafted gold coin");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
        }
    }
    public class PBooty : CustomCurrencySingleCoin
    {
        public static Color color = Color.Goldenrod;

        public PBooty(int coinItemID) : base(coinItemID, 999L)
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
                price == 1 ? Language.GetTextValue("Mods.AAMod.Common.PirateBooty") : Language.GetTextValue("Mods.AAMod.Common.PirateBooties")
            });
        }
    }
}