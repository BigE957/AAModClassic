using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace AAModClassic
{
	public class CustomCurrency : CustomCurrencySingleCoin
	{
		public CustomCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
		{
		}

		public override void GetPriceText(string[] lines, ref int currentLine, long price)
		{
			Color goblinColor = Color.ForestGreen * (Main.mouseTextColor / 255f);
			lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
				{
					goblinColor.R,
					goblinColor.G,
					goblinColor.B,
					Language.GetTextValue("LegacyTooltip.50"),
					price,
                    price == 1 ? Language.GetTextValue("Mods.AAModClassic.Common.GoblinSoul") : Language.GetTextValue("Mods.AAModClassic.Common.GoblinSouls")

            });
		}
	}
}
