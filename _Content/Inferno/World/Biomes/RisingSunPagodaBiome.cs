using AAModClassic.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public class RisingSunPagodaBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda = (AAWorld.keepTiles == 0 && AAWorld.pagodaTiles >= 1);
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:InfernoSky", isActive && player.Center.Y <= Main.worldSurface * 16);
            player.ManageSpecialBiomeVisuals("HeatDistortion", isActive && Main.UseHeatDistortion);
        }

        public override int Music =>
            AAWorld.downedAllAncients ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            (NPC.downedMoonlord && Main.dayTime) ? MusicManagementSystem.MusicSlots["Inferno_Pagoda"] : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }
}