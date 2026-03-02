using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.Backgrounds
{
    public class MireSurfaceBgStyle : ModSurfaceBackgroundStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire && !Main.LocalPlayer.ZoneSnow && !Main.LocalPlayer.ZoneDesert;
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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireBG");
        }
        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireFG2");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireFG1");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
		{
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);
            
            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

            mireBGFog.Update(Mod.GetTexture("Backgrounds/FogTex"));
			mireBGFog.Draw(Mod.GetTexture("Backgrounds/FogTex"), true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
		}
    }

    public class MireUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireUnderground1");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireUnderground");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireCavern1");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireCavern");
        }
    }

    class MireDesertBgStyle : ModSurfaceBackgroundStyle
    {
        readonly ScreenFog mireBGFog = new ScreenFog(true);

        public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override SurfaceBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire && Main.LocalPlayer.ZoneDesert;
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
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Backgrounds/MireDesertBG");
        }

        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            Color DefaultFog = new Color(120, 120, 200);
            Color YamataFog = new Color(200, 100, 100);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

            mireBGFog.Update(Mod.GetTexture("Backgrounds/FogTex"));
            mireBGFog.Draw(Mod.GetTexture("Backgrounds/FogTex"), true, YamataA ? YamataFog : DefaultFog);
            return Main.dayTime ? false : true;
        }

    }
}
