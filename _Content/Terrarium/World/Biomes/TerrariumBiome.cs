using AAModClassic._Content.Terrarium.World.Biomes.Waters;
using AAModClassic.Achievements;
using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Biomes
{
    public class TerrariumBiome : ModBiome
    {
        public override string MapBackground => "AAModClassic/_Content/Terrarium/World/Biomes/Backgrounds/TerrariumMap";

        public override string BackgroundPath => "AAModClassic/_Content/Terrarium/World/Biomes/Backgrounds/TerrariumMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.terraTiles >= 1 || AAWorld.keepTiles >= 1;
            if (active && player.whoAmI == Main.myPlayer)
                TerrariumDiscovered.Condition.Complete();
            return AAWorld.terraTiles >= 1;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Terrarium"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<TerrariumBG>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<TerraWaterStyle>();
    }

    public class TerrariumBG : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Terrarium/World/Biomes/Backgrounds/TerrariumBG");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Terrarium/World/Biomes/Backgrounds/TerrariumBG");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Terrarium/World/Biomes/Backgrounds/TerrariumBG");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Terrarium/World/Biomes/Backgrounds/TerrariumBG");
        }
    }
}
