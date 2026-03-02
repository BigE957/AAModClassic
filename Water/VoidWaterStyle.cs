using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class VoidWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == Mod.GetSurfaceBgStyleSlot("VoidSurfaceBgStyle");
		}

		public override int ChooseWaterfallStyle()
		{
			return Mod.GetWaterfallStyleSlot("VoidWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return Mod.Find<ModDust>("VoidWaterSplash").Type;
		}

		public override int GetDropletGore()
		{
			return Mod.GetGoreSlot("Water/VoidDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.Black;
		}
	}
}