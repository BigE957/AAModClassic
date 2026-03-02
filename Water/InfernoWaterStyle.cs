using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Water
{
    public class InfernoWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
        {
            Player player = Main.LocalPlayer;
            if (Main.bgStyle == Mod.GetSurfaceBgStyleSlot("InfernoSurfaceBgStyle") || Main.bgStyle == Mod.GetSurfaceBgStyleSlot("InfernoDesertBgStyle") || (player.ZoneSnow && player.GetModPlayer<AAPlayer>().ZoneInferno))
            {
                return true;
            }
            return false;
		}

		public override int ChooseWaterfallStyle()
		{
			return Mod.GetWaterfallStyleSlot("InfernoWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return Mod.Find<ModDust>("InfernoWaterSplash").Type;
		}

		public override int GetDropletGore()
		{
			return Mod.GetGoreSlot("Water/InfernoDroplet");
		}

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