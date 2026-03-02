using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Backgrounds
{
    public class GreedBG : ModUndergroundBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneHoard;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/GreedBG");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/GreedBG");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/GreedBG");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/GreedBG");
        }
    }
}
