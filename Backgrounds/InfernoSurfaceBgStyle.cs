using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Backgrounds
{
    class InfernoSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
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

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return base.ChooseCloseTexture(ref scale, ref parallax, ref a, ref b);
        }

        public override int ChooseMiddleTexture()
        {
            return base.ChooseMiddleTexture();
        }
    }

    public class InfernoUgBgStyle : ModUndergroundBackgroundStyle
    {
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
