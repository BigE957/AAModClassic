using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Items.Currency
{
    public class GoblinSoul : BaseAAItem
    {
        public static Asset<Texture2D> GoblinSoulAnimatedTex;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goblin Soul");
            // Tooltip.SetDefault("The soul of a goblin");
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            GoblinSoulAnimatedTex = ModContent.Request<Texture2D>("AAModClassic/Items/Currency/GoblinSoulA");
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

            Texture2D itemTex = GoblinSoulAnimatedTex.Value;

            Rectangle iframe = BaseDrawing.GetFrame(cframe, itemTex.Width, itemTex.Height / 4, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, itemTex, 0, Item.position, Item.width, Item.height, scale, rotation, Item.direction, 4, iframe, lightColor, true);
            return false;
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