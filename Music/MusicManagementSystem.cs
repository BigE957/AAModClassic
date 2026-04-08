using AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.___Content.Mire._PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic.Items.Blocks;
using AAModClassic.Items.Blocks.Boxes;
using AAModClassic.Tiles.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAModClassic.Music
{
    public class MusicManagementSystem : ModSystem
    {
        private static readonly Dictionary<string, int> MusicSlots = [];

        public override void Load()
        {
            MusicSlots.Add("Monarch", MusicLoader.GetMusicSlot(Mod, "Music/Monarch"));
            MusicSlots.Add("Fungus", MusicLoader.GetMusicSlot(Mod, "Music/Fungus"));
            MusicSlots.Add("GripsTheme", MusicLoader.GetMusicSlot(Mod, "Music/GripsTheme"));
            MusicSlots.Add("HydraTheme", MusicLoader.GetMusicSlot(Mod, "Music/HydraTheme"));
            MusicSlots.Add("BroodTheme", MusicLoader.GetMusicSlot(Mod, "Music/BroodTheme"));
            MusicSlots.Add("Shroom", MusicLoader.GetMusicSlot(Mod, "Music/Shroom"));
            MusicSlots.Add("InfernoSurface", MusicLoader.GetMusicSlot(Mod, "Music/InfernoSurface"));
            MusicSlots.Add("IN", MusicLoader.GetMusicSlot(Mod, "Music/IN"));
            MusicSlots.Add("MireSurface", MusicLoader.GetMusicSlot(Mod, "Music/MireSurface"));
            MusicSlots.Add("DM", MusicLoader.GetMusicSlot(Mod, "Music/DM"));
            MusicSlots.Add("InfernoUnderground", MusicLoader.GetMusicSlot(Mod, "Music/InfernoUnderground"));
            MusicSlots.Add("MireUnderground", MusicLoader.GetMusicSlot(Mod, "Music/MireUnderground"));
            MusicSlots.Add("Void", MusicLoader.GetMusicSlot(Mod, "Music/Void"));
            MusicSlots.Add("Djinn", MusicLoader.GetMusicSlot(Mod, "Music/Djinn"));
            MusicSlots.Add("TODE", MusicLoader.GetMusicSlot(Mod, "Music/TODE"));
            MusicSlots.Add("Boss6", MusicLoader.GetMusicSlot(Mod, "Music/Boss6"));
            MusicSlots.Add("Sag", MusicLoader.GetMusicSlot(Mod, "Music/Sag"));
            MusicSlots.Add("Anubis", MusicLoader.GetMusicSlot(Mod, "Music/Anubis"));
            MusicSlots.Add("Acropolis", MusicLoader.GetMusicSlot(Mod, "Music/Acropolis"));
            MusicSlots.Add("Hoard", MusicLoader.GetMusicSlot(Mod, "Music/Hoard"));
            MusicSlots.Add("Greed", MusicLoader.GetMusicSlot(Mod, "Music/Greed"));
            MusicSlots.Add("Athena", MusicLoader.GetMusicSlot(Mod, "Music/Athena"));
            MusicSlots.Add("RajahTheme", MusicLoader.GetMusicSlot(Mod, "Music/RajahTheme"));
            MusicSlots.Add("GreedA", MusicLoader.GetMusicSlot(Mod, "Music/GreedA"));
            MusicSlots.Add("AthenaA", MusicLoader.GetMusicSlot(Mod, "Music/AthenaA"));
            MusicSlots.Add("AnubisA", MusicLoader.GetMusicSlot(Mod, "Music/AnubisA"));
            MusicSlots.Add("Equinox", MusicLoader.GetMusicSlot(Mod, "Music/Equinox"));
            MusicSlots.Add("Stars", MusicLoader.GetMusicSlot(Mod, "Music/Stars"));
            MusicSlots.Add("AH", MusicLoader.GetMusicSlot(Mod, "Music/AH"));
            MusicSlots.Add("VoidButNowItsSpooky", MusicLoader.GetMusicSlot(Mod, "Music/VoidButNowItsSpooky"));
            MusicSlots.Add("Shrines", MusicLoader.GetMusicSlot(Mod, "Music/Shrines"));
            MusicSlots.Add("AkumaShrine", MusicLoader.GetMusicSlot(Mod, "Music/AkumaShrine"));
            MusicSlots.Add("Zero", MusicLoader.GetMusicSlot(Mod, "Music/Zero"));
            MusicSlots.Add("Zero2", MusicLoader.GetMusicSlot(Mod, "Music/Zero2"));
            MusicSlots.Add("Akuma", MusicLoader.GetMusicSlot(Mod, "Music/Akuma"));
            MusicSlots.Add("Akuma2", MusicLoader.GetMusicSlot(Mod, "Music/Akuma2"));
            MusicSlots.Add("Yamata", MusicLoader.GetMusicSlot(Mod, "Music/Yamata"));
            MusicSlots.Add("Yamata2", MusicLoader.GetMusicSlot(Mod, "Music/Yamata2"));
            MusicSlots.Add("Terrarium", MusicLoader.GetMusicSlot(Mod, "Music/Terrarium"));
            MusicSlots.Add("SleepingDragon", MusicLoader.GetMusicSlot(Mod, "Music/SleepingDragon"));
            MusicSlots.Add("SleepingGiant", MusicLoader.GetMusicSlot(Mod, "Music/SleepingGiant"));
            MusicSlots.Add("Shen", MusicLoader.GetMusicSlot(Mod, "Music/Shen"));
            MusicSlots.Add("ShenA", MusicLoader.GetMusicSlot(Mod, "Music/ShenA"));
            MusicSlots.Add("SupremeRajah", MusicLoader.GetMusicSlot(Mod, "Music/SupremeRajah"));

            MusicSlots.Add("Cthulhu", MusicLoader.GetMusicSlot(Mod, "_Unreleased/Music/Cthulhu"));
            MusicSlots.Add("IZ", MusicLoader.GetMusicSlot(Mod, "_Unreleased/Music/IZ"));
            MusicSlots.Add("IZDeath", MusicLoader.GetMusicSlot(Mod, "_Unreleased/Music/IZDeath"));
            MusicSlots.Add("Maelstrom", MusicLoader.GetMusicSlot(Mod, "_Unreleased/Music/Maelstrom"));
            MusicSlots.Add("Ship", MusicLoader.GetMusicSlot(Mod, "_Unreleased/Music/Ship"));
            MusicSlots.Add("SoC", MusicLoader.GetMusicSlot(Mod, "_Unreleased/Music/SoC"));
        }

        //TODO: Make mod call which invokes this
        public static void ReplaceTrack(string key, int musicSlot)
        {
            if (MusicSlots.ContainsKey(key))
                MusicSlots[key] = musicSlot;
        }

        public override void PostSetupContent()
        {
            MusicLoader.AddMusicBox(Mod, MusicSlots["Monarch"], ModContent.ItemType<MonarchBox>(), ModContent.TileType<MonarchBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Fungus"], ModContent.ItemType<FungusBox>(), ModContent.TileType<FungusBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["GripsTheme"], ModContent.ItemType<GripsBox>(), ModContent.TileType<GripsBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["HydraTheme"], ModContent.ItemType<HydraBox>(), ModContent.TileType<HydraBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["BroodTheme"], ModContent.ItemType<BroodBox>(), ModContent.TileType<BroodBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Shroom"], ModContent.ItemType<MushBox>(), ModContent.TileType<MushBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["InfernoSurface"], ModContent.ItemType<InfernoBox>(), ModContent.TileType<InfernoBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["IN"], ModContent.ItemType<InfernoNightBox>(), ModContent.TileType<InfernoNightBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["MireSurface"], ModContent.ItemType<MireBox>(), ModContent.TileType<MireBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["DM"], ModContent.ItemType<MireDayBox>(), ModContent.TileType<MireDayBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["InfernoUnderground"], ModContent.ItemType<InfernoUBox>(), ModContent.TileType<InfernoUBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["MireUnderground"], ModContent.ItemType<MireUBox>(), ModContent.TileType<MireUBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Void"], ModContent.ItemType<VoidBox>(), ModContent.TileType<VoidBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Djinn"], ModContent.ItemType<DjinnBox>(), ModContent.TileType<DjinnBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["TODE"], ModContent.ItemType<ToadBox>(), ModContent.TileType<ToadBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Boss6"], ModContent.ItemType<SerpentBox>(), ModContent.TileType<SerpentBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Sag"], ModContent.ItemType<SagBox>(), ModContent.TileType<SagBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Anubis"], ModContent.ItemType<AnubisBox>(), ModContent.TileType<AnubisBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Acropolis"], ModContent.ItemType<AcropolisBox>(), ModContent.TileType<AcropolisBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Hoard"], ModContent.ItemType<HoardBox>(), ModContent.TileType<HoardBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Greed"], ModContent.ItemType<GreedBox>(), ModContent.TileType<GreedBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Athena"], ModContent.ItemType<AthenaBox>(), ModContent.TileType<AthenaBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["RajahTheme"], ModContent.ItemType<RajahBox>(), ModContent.TileType<RajahBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["GreedA"], ModContent.ItemType<GreedABox>(), ModContent.TileType<GreedABox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["AthenaA"], ModContent.ItemType<AthenaABox>(), ModContent.TileType<AthenaABox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["AnubisA"], ModContent.ItemType<AnubisFBox>(), ModContent.TileType<AnubisFBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Equinox"], ModContent.ItemType<Equibox>(), ModContent.TileType<Equibox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Stars"], ModContent.ItemType<StarBox>(), ModContent.TileType<StarBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["AH"], ModContent.ItemType<SistersBox>(), ModContent.TileType<SistersBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["VoidButNowItsSpooky"], ModContent.ItemType<FateBox>(), ModContent.TileType<FateBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Shrines"], ModContent.ItemType<LakeBox>(), ModContent.TileType<LakeBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["AkumaShrine"], ModContent.ItemType<PagodaBox>(), ModContent.TileType<PagodaBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Zero"], ModContent.ItemType<ZeroBox>(), ModContent.TileType<ZeroBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Zero2"], ModContent.ItemType<Zero2Box>(), ModContent.TileType<Zero2Box_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Akuma"], ModContent.ItemType<AkumaBox>(), ModContent.TileType<AkumaBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Akuma2"], ModContent.ItemType<AkumaABox>(), ModContent.TileType<AkumaABox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Yamata"], ModContent.ItemType<YamataBox>(), ModContent.TileType<YamataBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Yamata2"], ModContent.ItemType<YamataABox>(), ModContent.TileType<YamataABox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Terrarium"], ModContent.ItemType<TerrariumBox>(), ModContent.TileType<TerrariumBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["SleepingDragon"], ModContent.ItemType<SDBox>(), ModContent.TileType<SDBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["SleepingGiant"], ModContent.ItemType<SGBox>(), ModContent.TileType<SGBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["Shen"], ModContent.ItemType<ShenBox>(), ModContent.TileType<ShenBox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["ShenA"], ModContent.ItemType<ShenABox>(), ModContent.TileType<ShenABox_Tile>());
            MusicLoader.AddMusicBox(Mod, MusicSlots["SupremeRajah"], ModContent.ItemType<SRajahBox>(), ModContent.TileType<SRajahBox_Tile>());
        }
    }
}
