using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Backgrounds
{
    class MushroomSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMush;
        }

        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomBG3");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomBG2");
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomBG1");
        }
    }

    public class MushroomUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMush;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomUG2");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomUG1");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomUG4");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MushroomUG3");
        }
    }
}
