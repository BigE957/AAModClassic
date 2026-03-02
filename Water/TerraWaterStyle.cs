using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class TerraWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == Mod.GetSurfaceBgStyleSlot("TerraSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return Mod.GetWaterfallStyleSlot("TerraWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return Mod.Find<ModDust>("TerraWaterSplash").Type;
		}

		public override int GetDropletGore()
		{
			return Mod.GetGoreSlot("Water/TerraDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return AAColor.TerraGlow;
		}
	}
}