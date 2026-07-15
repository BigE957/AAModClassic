using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.FrostMoon.__Hardmode.Items.Currency
{
    public class ChristmasCheer : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Currency";
        public static Asset<Texture2D> AnimatedTexture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Christmas Cheer");
            // Tooltip.SetDefault("Pure joy and minty fresh goodness");
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            AnimatedTexture = ModContent.Request<Texture2D>(Texture + "_Animated");

            Item.ResearchUnlockCount = 50;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightCyan.ToVector3() * 0.55f * Main.essScale);
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ItemRarityID.Yellow;
        }

        int counter = 0;
        int cframe = 0;

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (counter++ > 7)
            {
                cframe++;
                counter = 0;
                if (cframe > 3)
                {
                    cframe = 0;
                }
            }

            Texture2D itemTex = AnimatedTexture.Value;

            Rectangle iframe = BaseDrawing.GetFrame(cframe, itemTex.Width, itemTex.Height / 4, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, itemTex, 0, Item.position, Item.width, Item.height, scale, rotation, Item.direction, 4, iframe, lightColor, true);
            return false;
        }
    }
    public class CCheer : CustomCurrencySingleCoin
    {
        public static Color color = Color.LightCyan;

        public CCheer(int coinItemID) : base(coinItemID, 999L)
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
                price == 1 ? Language.GetTextValue("Mods.AAModClassic.Common.ChristmasCheer") : Language.GetTextValue("Mods.AAModClassic.Common.ChristmasCheers")
            });
        }
    }
}