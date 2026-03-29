using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Items.Currency
{
    public class MonsterSoul : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Monster Soul");
            // Tooltip.SetDefault("The soul of a terrifying Monster");
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.DarkGray.ToVector3() * 0.55f * Main.essScale);
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = 1000;
            Item.rare = ItemRarityID.Lime;
        }

        int counter = 0;
        int cframe = 0;

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (counter++ > 7)
            {
                cframe++;
                counter = 0;
                if (cframe > 7)
                {
                    cframe = 0;
                }
            }

            Texture2D itemTex = ModContent.Request<Texture2D>("AAModClassic/Items/Currency/MonsterSoulA").Value;

            Rectangle iframe = BaseDrawing.GetFrame(cframe, itemTex.Width, itemTex.Height / 8, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, itemTex, 0, Item.position, Item.width, Item.height, scale, rotation, Item.direction, 8, iframe, lightColor, true);
            return false;
        }
    }
    public class MSouls : CustomCurrencySingleCoin
    {
        public static Color color = Color.DarkGray;

        public MSouls(int coinItemID) : base(coinItemID, 999L)
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
                price == 1 ? Language.GetTextValue("Mods.AAModClassic.Common.MonsterSoul") : Language.GetTextValue("Mods.AAModClassic.Common.MonsterSouls")
            });
        }
    }
}
