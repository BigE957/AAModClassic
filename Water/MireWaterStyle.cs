using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Water
{
    public class MireWaterStyle : ModWaterStyle
	{
        /*
		public override bool ChooseWaterStyle()
        {
            Player player = Main.LocalPlayer;

            if (Main.bgStyle == Mod.GetSurfaceBgStyleSlot("MireSurfaceBgStyle") || Main.bgStyle == Mod.GetSurfaceBgStyleSlot("MireDesertBgStyle") || (player.ZoneSnow && player.GetModPlayer<AAPlayer>().ZoneMire))
            {
                if (!Main.dayTime || AAWorld.downedYamata || player.position.Y > Main.worldSurface * 16.0)
                {
                    return true;
                }
            }
            return false;
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
            WaterfallStyle = ModContent.Find<ModWaterfallStyle>("AAModClassic/MireWaterfallStyle");
            SplashDust = ModContent.DustType<MireWaterSplash>();
            DropletGore = ModContent.GoreType<MireDroplet>();
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
        public override Asset<Texture2D> GetRainTexture() => RainTexture ??= ModContent.Request<Texture2D>("AAModClassic/Waters/MireWaterfallStyle");

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