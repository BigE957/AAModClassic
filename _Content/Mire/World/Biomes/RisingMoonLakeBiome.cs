using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.World.Biomes
{
    public class RisingMoonLakeBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player) => AAWorld.lakeTiles >= 1;

        public override int Music =>
            (AAWorld.downedAllAncients && !AAWorld.downedShen) ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            (NPC.downedMoonlord && !Main.dayTime) ? MusicManagementSystem.MusicSlots["Mire_Lake"] : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && !Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }
}
