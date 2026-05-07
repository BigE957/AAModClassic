using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Biomes
{
    public class StarsBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneStars = AAWorld.Radium >= 20;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Stars"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

}
