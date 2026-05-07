using AAModClassic.Backgrounds;
using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard.World.Biomes
{
    public class HoardBiome : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            player.GetModPlayer<AAPlayer>().ZoneHoard = (AAWorld.HoardTiles > 1 && !player.GetModPlayer<AAPlayer>().ZoneStars);
            return player.GetModPlayer<AAPlayer>().ZoneHoard;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Hoard"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<GreedBG>();
    }

    public class GreedBG : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Hoard/World/Biomes/Backgrounds/GreedBG");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Hoard/World/Biomes/Backgrounds/GreedBG");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Hoard/World/Biomes/Backgrounds/GreedBG");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/Hoard/World/Biomes/Backgrounds/GreedBG");
        }
    }
}
