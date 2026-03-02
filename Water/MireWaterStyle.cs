using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class MireWaterStyle : ModWaterStyle
	{
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
        
		public override int ChooseWaterfallStyle()
		{
			return Mod.GetWaterfallStyleSlot("MireWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return Mod.Find<ModDust>("MireWaterSplash").Type;
		}

		public override int GetDropletGore()
		{
			return Mod.GetGoreSlot("Water/MireDroplet");
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