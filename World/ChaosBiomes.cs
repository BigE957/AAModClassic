using AAModClassic.Base.BaseMod.Base;
using AAModClassic.NPCs.Bosses.Akuma;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;
using AAModClassic.NPCs.Bosses.Yamata;
using AAModClassic.NPCs.Bosses.Yamata.Awakened;
using AAModClassic.NPCs.Bosses.Zero;
using AAModClassic.NPCs.Bosses.Zero.Protocol;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.World
{
    public class MireBiomeZone : ModBiome
    {
        public override string MapBackground => "AAModClassic/Map/MireMap";

        public override bool IsBiomeActive(Player player)
        {
            bool active = (AAWorld.mireTiles > 100) || BaseAI.GetNPC(player.Center, ModContent.NPCType<Yamata>(), 5000) != -1 || BaseAI.GetNPC(player.Center, ModContent.NPCType<YamataA>(), 5000) != -1;
            return player.GetModPlayer<AAPlayer>().ZoneMire = active;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAMod:MireSky", isActive && player.Center.Y <= Main.worldSurface * 16);
        }

        public override int Music =>
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/MireUnderground") :
            Main.dayTime ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/DM") :
            MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/MireSurface");

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }

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
            player.ManageSpecialBiomeVisuals("AAMod:InfernoSky", isActive && player.Center.Y <= Main.worldSurface * 16);
            player.ManageSpecialBiomeVisuals("HeatDistortion", isActive && Main.UseHeatDistortion);
        }

        public override int Music => 
            Main.LocalPlayer.ZoneRockLayerHeight ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/InfernoUnderground") :
            !Main.dayTime ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/IN") :
            MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/InfernoSurface");

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
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
            player.ManageSpecialBiomeVisuals("AAMod:VoidSky", isActive);
        }

        public override int Music => 
            AAWorld.downedZero ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/SleepingGiant") : 
            NPC.downedMoonlord ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/VoidButNowItsSpooky") : 
            MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Void");

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

    }

    public class MushroomBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneMush = AAWorld.mushTiles > 100;
        }

        public override int Music => MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Shroom");

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }

    public class TerrariumBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().Terrarium = AAWorld.terraTiles >= 1;
        }

        public override int Music => MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Terrarium");

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }

    public class RisingMoonLakeBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake = AAWorld.lakeTiles >= 1;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAMod:MireSky", isActive && player.Center.Y <= Main.worldSurface * 16);
        }

        public override int Music => 
            AAWorld.downedAllAncients ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/SleepingDragon") : 
            (NPC.downedMoonlord && !Main.dayTime) ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Shrines") : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && !Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }

    public class RisingSunPagodaBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda = AAWorld.pagodaTiles >= 1;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("AAMod:InfernoSky", isActive && player.Center.Y <= Main.worldSurface * 16);
            player.ManageSpecialBiomeVisuals("HeatDistortion", isActive && Main.UseHeatDistortion);
        }

        public override int Music => 
            AAWorld.downedAllAncients ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/SleepingDragon") : 
            (NPC.downedMoonlord && Main.dayTime) ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/AkumaShrine") : -1;

        public override SceneEffectPriority Priority => AAWorld.downedAllAncients ? SceneEffectPriority.Event : (NPC.downedMoonlord && Main.dayTime) ? SceneEffectPriority.Environment : SceneEffectPriority.None;
    }

    public class RadiumBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneStars = AAWorld.Radium >= 20;
        }

        public override int Music => MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Stars");
        
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }

    public class HoardBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneHoard = AAWorld.HoardTiles > 1 && !player.GetModPlayer<AAPlayer>().ZoneStars;
        }

        public override int Music => AAWorld.downedZero ? MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Hoard") : -1;

        public override SceneEffectPriority Priority => AAWorld.downedZero ? SceneEffectPriority.Event : SceneEffectPriority.None;
    }

    public class AcropolisBiomeZone : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<AAPlayer>().ZoneAcropolis = AAWorld.CloudTiles > 1;
        }

        public override int Music => MusicLoader.GetMusicSlot(AAMod.instance, "Sounds/Music/Acropolis");

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

    }

    /*
    if (Ancients.ZoneShip)
    {
        priority = MusicPriority.Event;
        music = MusicLoader.GetMusicSlot("Sounds/Music/Ship");

        return;
    }
    */
}
