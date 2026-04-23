using AAModClassic.Water;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Biomes.Water
{
    public class FogWaterStyle : ModWaterStyle
	{
        /*
		public override bool ChooseWaterStyle()
		{
            Player player = Main.LocalPlayer;
            return Main.bgStyle == Mod.GetSurfaceBgStyleSlot("MireSurfaceBgStyle") && ];
        }
		*/

        public static ModWaterStyle Instance { get; private set; }
        public static ModWaterfallStyle WaterfallStyle { get; private set; }
        public static int SplashDust { get; private set; }
        public static int DropletGore { get; private set; }
        public static Asset<Texture2D> RainTexture { get; private set; }

        public override void SetStaticDefaults()
        {
            Instance = this;
            WaterfallStyle = ModContent.Find<ModWaterfallStyle>("AAModClassic/FogWaterfallStyle");
            SplashDust = ModContent.DustType<FogWaterSplash>();
            DropletGore = ModContent.GoreType<FogDroplet>();
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
        public override Asset<Texture2D> GetRainTexture() => RainTexture ??= ModContent.Request<Texture2D>("AAModClassic/Water/FogWaterfallStyle");


        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.DarkBlue;
		}
	}
}