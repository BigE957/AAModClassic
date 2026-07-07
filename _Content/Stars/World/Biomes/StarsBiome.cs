using AAModClassic.Achievements;
using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars.World.Biomes
{
    public class StarsBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.EquinoxAltar >= 20;
            if (active && player.whoAmI == Main.myPlayer)
                EquinoxAltarDiscovered.Condition.Complete();
            return player.GetModPlayer<ZAAPlayer>().ZoneStars = AAWorld.Radium + AAWorld.EquinoxAltar >= 20;
        }

        public override int Music => AAWorld.EquinoxAltar > 0 ? MusicManagementSystem.MusicSlots["Equinox_Altar"] : MusicManagementSystem.MusicSlots["Stars"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

}
