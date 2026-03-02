using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    class InfernoSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneInferno && !Main.LocalPlayer.ZoneSnow && !Main.LocalPlayer.ZoneDesert;
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

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/InfernoBG");
        }
    }

    public class InfernoUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneInferno;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/InfernoUnderground1");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/InfernoUnderground");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/InfernoCavern1");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/InfernoCavern");
        }
    }

    class InfernoDesertBgStyle : ModSurfaceBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneInferno && Main.LocalPlayer.ZoneDesert;
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

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/InfernoDesertBG");
        }

    }
}
