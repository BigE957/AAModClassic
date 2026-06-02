using AAModClassic.Achievements;
using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.World.Biomes
{
    public class AcropolisBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.CloudTiles > 1;
            if (active && player.whoAmI == Main.myPlayer)
                AcropolisDiscovered.Condition.Complete();
            return player.GetModPlayer<AAPlayer>().ZoneAcropolis = active;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Acropolis"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }
}
