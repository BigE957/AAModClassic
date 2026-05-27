using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public class RisingSunPagodaBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player) => player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda = (AAWorld.keepTiles == 0 && AAWorld.pagodaTiles >= 1);

        public override int Music =>
            AAWorld.downedAllAncients ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            (NPC.downedMoonlord && Main.dayTime) ? MusicManagementSystem.MusicSlots["Inferno_Pagoda"] : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }
}