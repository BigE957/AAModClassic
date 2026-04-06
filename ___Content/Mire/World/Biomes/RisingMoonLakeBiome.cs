using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.World.Biomes
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
            AAWorld.downedAllAncients ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/SleepingDragon") :
            (NPC.downedMoonlord && !Main.dayTime) ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Shrines") : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && !Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }
}
