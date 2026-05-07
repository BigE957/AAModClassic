using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.World.Biomes
{
    public class AcropolisBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneAcropolis = AAWorld.CloudTiles > 1;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Acropolis"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

    }

}
