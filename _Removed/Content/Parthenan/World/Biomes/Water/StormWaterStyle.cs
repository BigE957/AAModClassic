using AAModClassic._Content.Mire.World.Biomes.Water;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.World.Biomes.Water
{
    public class StormWaterStyle : ModWaterStyle
	{
		/*
        public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == Mod.GetSurfaceBgStyleSlot("StormBgStyle");
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
            WaterfallStyle = ModContent.Find<ModWaterfallStyle>("AAModClassic/StormWaterfallStyle");
            SplashDust = ModContent.DustType<StormWaterSplash>();
            DropletGore = ModContent.GoreType<StormDroplet>();
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
        public override Asset<Texture2D> GetRainTexture() => RainTexture ??= ModContent.Request<Texture2D>("AAModClassic/_Removed/Content/Parthenan/World/Biomes/Water/StormRain");

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = .7f;
            g = 0f;
            b = 1f;
        }

        public override Color BiomeHairColor()
        {
            return Color.Violet;
        }
    }
}