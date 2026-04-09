using AAModClassic.Backgrounds;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.NPCs.Bosses.Akuma;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;
using AAModClassic.NPCs.Bosses.Zero;
using AAModClassic.NPCs.Bosses.Zero.Protocol;
using AAModClassic.Water;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.World
{
    public class InfernoBiomeZone : ModBiome
    {
        public override string MapBackground => "AAModClassic/Map/InfernoMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = AAWorld.infernoTiles > 100 || BaseAI.GetNPC(player.Center, ModContent.NPCType<Akuma>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<AkumaA>(), 5000) != -1;
            return player.GetModPlayer<AAPlayer>().ZoneInferno = active;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool rllyActive = (isActive && player.Center.Y <= Main.worldSurface * 16) || player.GetModPlayer<AAPlayer>().SunAltar;
            player.ManageSpecialBiomeVisuals("AAModClassic:InfernoSky", rllyActive);
            player.ManageSpecialBiomeVisuals("HeatDistortion", rllyActive && Main.UseHeatDistortion);
        }

        public override int Music => 
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicManagementSystem.MusicSlots["Inferno_Underground"] :
            AAWorld.downedAkuma && AAWorld.downedYamata ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] :
            !Main.dayTime ? MusicManagementSystem.MusicSlots["Inferno_Night"] :
            MusicManagementSystem.MusicSlots["Inferno_Surface"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle 
        { 
            get 
            {
                if((Main.LocalPlayer.ZoneDesert && Main.LocalPlayer.ZoneSnow) && ModLoader.TryGetMod("SpiritReforged", out var spirit))
                {
                    //Rectangle saltFlatsArea = (Rectangle)spirit.Call("GetSaltFlatsArea");
                    //bool playerInSaltFlats = saltFlatsArea.Contains(Main.LocalPlayer.Center.ToTileCoordinates());
                    //Main.NewText(saltFlatsArea);
                    //if (playerInSaltFlats)
                        return null;
                }

                return Main.LocalPlayer.ZoneDesert ? ModContent.GetInstance<InfernoDesertBgStyle>() : !Main.LocalPlayer.ZoneSnow ? ModContent.GetInstance<InfernoSurfaceBgStyle>() : null;
            }
        }

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<InfernoUgBgStyle>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<InfernoWaterStyle>();
    }

    public class VoidBiomeZone : ModBiome
    {
        public override string MapBackground => "AAModClassic/Map/VoidMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = (AAWorld.voidTiles > 20 && player.ZoneSkyHeight) || (AAWorld.voidTiles > 100 && !player.ZoneSkyHeight) || BaseAI.GetNPC(player.Center, ModContent.NPCType<Zero>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<ZeroProtocol>(), 5000) != -1;
            return player.GetModPlayer<AAPlayer>().ZoneVoid = active;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:VoidSky", isActive || player.GetModPlayer<AAPlayer>().VoidUnit);
        }

        public override int Music => 
            AAWorld.downedZero ? MusicManagementSystem.MusicSlots["Void_PreIZ"] : 
            NPC.downedMoonlord ? MusicManagementSystem.MusicSlots["Void_PostML"] : 
            MusicManagementSystem.MusicSlots["Void"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<VoidSurfaceBgStyle>();

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<VoidUGBG>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<VoidWaterStyle>();
    }

    public class MushroomBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneMush = AAWorld.mushTiles > 100;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Mushroom_Surface"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<MushroomSurfaceBgStyle>();

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<MushroomUgBgStyle>();
    }

    public class TerrariumBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            player.GetModPlayer<AAPlayer>().Terrarium = AAWorld.terraTiles >= 1 || AAWorld.keepTiles >= 1;
            return AAWorld.terraTiles >= 1;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Terrarium"];

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<TerrariumBG>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<TerraWaterStyle>();
    }

    public class LostKeepBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            player.GetModPlayer<AAPlayer>().Terrarium = AAWorld.terraTiles >= 1 || AAWorld.keepTiles >= 1;
            return AAWorld.keepTiles >= 1;
        }

        public override int Music => MusicManagementSystem.MusicSlots["LostKeep"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<TerrariumBG>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<TerraWaterStyle>();
    }

    public class RisingSunPagodaBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda = (AAWorld.keepTiles == 0 && AAWorld.pagodaTiles >= 1);
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAModClassic:InfernoSky", isActive && player.Center.Y <= Main.worldSurface * 16);
            player.ManageSpecialBiomeVisuals("HeatDistortion", isActive && Main.UseHeatDistortion);
        }

        public override int Music => 
            AAWorld.downedAllAncients ? MusicManagementSystem.MusicSlots["Chaos_PreShen"] : 
            (NPC.downedMoonlord && Main.dayTime) ? MusicManagementSystem.MusicSlots["Inferno_Pagoda"] : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }

    public class RadiumBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneStars = AAWorld.Radium >= 20;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Stars"];
        
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

    public class HoardBiomeZone : ModBiome
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

    public class AcropolisBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneAcropolis = AAWorld.CloudTiles > 1;
        }

        public override int Music => MusicManagementSystem.MusicSlots["Acropolis"];

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

    }

    /*
    if (Ancients.ZoneShip)
    {
        priority = MusicPriority.Event;
        music = MusicManagementSystem.MusicSlots["Ship"];

        return;
    }
    */
}
