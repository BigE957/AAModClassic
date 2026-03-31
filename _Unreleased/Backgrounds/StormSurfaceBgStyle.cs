using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Backgrounds
{
    class StormSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        //public override bool ChooseBgStyle()/* tModPorter Note: _Unreleased. Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        //{
        //    return !Main.gameMenu && (NPC.AnyNPCs(ModContent.NPCType<Infinity>()) || NPC.AnyNPCs(ModContent.NPCType<IZSpawn1>()));
        //}

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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Backgrounds/StormBG");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Backgrounds/StormBG");
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Unreleased/Backgrounds/StormBG");
        }
    }
}
