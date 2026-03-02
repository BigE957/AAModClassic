using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class FogWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
            Player player = Main.LocalPlayer;
            return Main.bgStyle == Mod.GetSurfaceBgStyleSlot("MireSurfaceBgStyle") && Main.dayTime && !AAWorld.downedYamata && player.position.Y < Main.worldSurface * 16.0 && !player.buffImmune[ModContent.BuffType<Buffs.Clueless>()];
        }
        
		public override int ChooseWaterfallStyle()
		{
			return Mod.GetWaterfallStyleSlot("FogWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return Mod.Find<ModDust>("FogWaterSplash").Type;
		}

		public override int GetDropletGore()
		{
			return Mod.GetGoreSlot("Water/FogDroplet");
		}

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