using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class TerrariumBG : ModUndergroundBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().Terrarium;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/TerrariumBG");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/TerrariumBG");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/TerrariumBG");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/TerrariumBG");
        }
    }
}
