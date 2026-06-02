using AAModClassic.Achievements;
using AAModClassic.Music;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.World.Biomes
{
    public class RedMushroomBiome : ModBiome
    {
        public override string BestiaryIcon => "AAModClassic/_Content/RedMushroom/World/Biomes/RedMushroomBiome_Icon";

        public override string MapBackground => "AAModClassic/_Content/RedMushroom/World/Biomes/Backgrounds/RedMushroomMap";

        public override string BackgroundPath => "AAModClassic/_Content/RedMushroom/World/Biomes/Backgrounds/RedMushroomMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.mushTiles > 100;
            if (active && player.whoAmI == Main.myPlayer)
                RedMushroomDiscovered.Condition.Complete();
            return player.GetModPlayer<AAPlayer>().ZoneMush = active;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Mushroom_Surface"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<MushroomSurfaceBgStyle>();

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<MushroomUgBgStyle>();
    }

    public class MushroomSurfaceBgStyle : ModSurfaceBackgroundStyle
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

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomBG3");
        }

        public override int ChooseMiddleTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomBG2");
        }

        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomBG1");
        }
    }

    public class MushroomUgBgStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomUG2");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomUG1");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomUG4");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "_Content/RedMushroom/World/Biomes/Backgrounds/MushroomUG3");
        }
    }
}
