using AAModClassic._CrossMod;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;

namespace AAModClassic.Utilities
{
    public static class PlayerExtensions
    {
        public static bool ZoneTowerAny(this Player player) => player.ZoneTowerNebula || player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerStardust;

        public static bool ZoneSurface(this Player player) => !player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight;

        public static bool ZoneAnyInferno(this Player player) => player.GetModPlayer<AAPlayer>().ZoneInferno || ContentReplacementSystem.InNewInferno(player);

        public static bool ZoneAnyMire(this Player player) => player.GetModPlayer<AAPlayer>().ZoneMire || ContentReplacementSystem.InNewMire(player);

        public static TransformationPlayer Transformation(this Player player) => player.GetModPlayer<TransformationPlayer>();

    }
}
