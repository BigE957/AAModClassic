using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic._Content.Terrarium.World.Biomes.Waters;
using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Biomes
{
    public class LostKeepBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player) => AAWorld.keepTiles >= 1;

        public override int Music => MusicManagementSystem.MusicSlots["LostKeep"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<TerrariumBG>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<TerraWaterStyle>();
    }
}
