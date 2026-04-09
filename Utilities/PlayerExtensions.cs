using Terraria;

namespace AAModClassic.Utilities
{
    public static class PlayerExtensions
    {
        public static bool ZoneTowerAny(this Player player) => player.ZoneTowerNebula || player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerStardust;

        public static bool ZoneSurface(this Player player) => !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight;
    }
}
