using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Biomes
{
    public class RisingMoonLakeBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake = AAWorld.lakeTiles >= 1;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:MireSky", isActive && player.Center.Y <= Main.worldSurface * 16);
        }

        public override int Music =>
            AAWorld.downedAllAncients ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            (NPC.downedMoonlord && !Main.dayTime) ? MusicManagementSystem.MusicSlots["Mire_Lake"] : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && !Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }
}
