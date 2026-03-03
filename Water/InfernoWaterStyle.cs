using AAModClassic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Water
{
    public class InfernoWaterStyle : ModWaterStyle
	{
        public static ModWaterStyle Instance { get; private set; }
        public static ModWaterfallStyle WaterfallStyle { get; private set; }
        public static int SplashDust { get; private set; }
        public static int DropletGore { get; private set; }
        public static Asset<Texture2D> RainTexture { get; private set; }

        public override void SetStaticDefaults()
        {
            Instance = this;
            WaterfallStyle = ModContent.Find<ModWaterfallStyle>("AAModClassic/InfernoWaterfallStyle");
            SplashDust = ModContent.DustType<InfernoWaterSplash>();
            DropletGore = ModContent.GoreType<InfernoDroplet>();
        }

        public override void Unload()
        {
            Instance = null;
            WaterfallStyle = null;
            SplashDust = 0;
            DropletGore = 0;
        }

        public override int ChooseWaterfallStyle() => WaterfallStyle.Slot;
        public override int GetSplashDust() => SplashDust;
        public override int GetDropletGore() => DropletGore;
        public override Asset<Texture2D> GetRainTexture() => RainTexture ??= ModContent.Request<Texture2D>("AAModClassic/Waters/InfernoWaterfallStyle");

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.OrangeRed;
		}
	}
}