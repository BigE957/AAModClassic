using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Biomes
{
    public class StarsBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneStars = AAWorld.Radium + AAWorld.EquinoxAltar >= 20;
        }

        public override int Music => AAWorld.EquinoxAltar > 0 ? MusicManagementSystem.MusicSlots["Equinox_Altar"] : MusicManagementSystem.MusicSlots["Stars"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

}
