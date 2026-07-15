using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Currency
{
    public class GoblinSoul : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Currency";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goblin Soul");
            // Tooltip.SetDefault("The soul of a goblin");
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(8, 4));
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            Item.ResearchUnlockCount = 50;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.ForestGreen.ToVector3() * 0.55f * Main.essScale);
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ItemRarityID.Orange;
        }
    }
    public class GSouls : CustomCurrencySingleCoin
    {
        public static Color color = Color.ForestGreen;

        public GSouls(int coinItemID) : base(coinItemID, 999L)
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
                price == 1 ? Language.GetTextValue("Mods.AAModClassic.Common.GoblinSoul") : Language.GetTextValue("Mods.AAModClassic.Common.GoblinSouls")
            });
        }
    }
}