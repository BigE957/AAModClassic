using System;
using System.IO;
using System.Collections.Generic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Unreleased;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic
{
    public class AADowned : ModSystem
    {
        #region downed bools
        public static bool downedMushroomMonarch = false;
        public static bool downedFeudalFungus = false;
        public static bool downedGripsOfChaos = false;

        public static bool downedTruffleToad = false;
        public static bool downedBroodmother = false;
        public static bool downedHydra = false;

        public static bool downedSubzeroSerpent = false;
        public static bool downedDesertDjinn = false;
        public static bool downedSagittarius = false;

        public static bool downedTechnoTruffle = false;
        public static bool downedRetriever = false;
        public static bool downedOrthrusX = false;

        public static bool downedRaiderUltima = false;
        public static bool downedAnubis = false;
        public static bool downedBiomiteCore = false;

        public static bool downedAthena = false;
        public static bool downedGreed = false;
        public static bool downedRajahRabbit = false;

        public static bool downedForsakenAnubis = false;
        public static bool downedAthenaA = false;
        public static bool downedGreedA = false;

        public static bool downedEquinoxWorms = false;
        public static bool downedSistersOfDiscord = false;
        public static bool downedAkuma = false;

        public static bool downedAkumaA = false;
        public static bool downedYamata = false;
        public static bool downedYamataA = false;

        public static bool downedZero = false;
        public static bool downedZeroP = false;
        public static bool downedRajahRabbitR = false;

        public static bool downedShen = false;
        public static bool downedShenA = false;
        public static bool downedInfinityZero = false;

        public static bool downedSoulOfCthulhu = false;
        public static bool downedCthulhu = false;

        public static bool downedDB = false;
        public static bool downedNC = false;
        public static bool zeroUS = false;

        public static bool Ancients = false;
        public static bool ShenSummoned = false;
        public static bool SistersSummoned = false;
        public static bool AthenaHerald = false;
        public static bool AnubisAwakened = false;
        public static bool AthenaAwakened = false;
        public static bool GreedAwakened = false;
        public static bool WormActive = false;
        public static bool StarActive = false;
        public static bool GravActive = false;
        public static bool spawnGrips = false;
        #endregion

        #region replicated AAWorld downed helpers
        public static bool DownedAkuma => (downedAkuma && !Main.expertMode) || downedAkumaA;
        public static bool DownedYamata => (downedYamata && !Main.expertMode) || downedYamataA;
        public static bool DownedZero => (downedZero && !Main.expertMode) || downedZeroP;
        public static bool DownedShen => (downedShen && !Main.expertMode) || downedShenA;
        public static bool DownedAncient => DownedAkuma || DownedYamata || DownedZero;
        public static bool DownedSAncient => DownedShen || AAWorld_Unreleased.DownedIZ || AAWorld_Unreleased.DownedSoC;
        public static bool DownedAllAncients => DownedAkuma && DownedYamata && DownedZero;
        public static bool DownedAnySiegeUnits => downedRetriever || downedOrthrusX || downedRaiderUltima;
        public static bool DownedAllSiegeUnits => downedRetriever && downedOrthrusX && downedRaiderUltima;
        public static bool Terra1 => downedBroodmother || downedHydra || NPC.downedBoss2;
        public static bool Terra2 => NPC.downedPlantBoss;
        public static bool Terra3 => DownedShen;
        private static bool previousDownedAllAncients = false;
        #endregion

        private void FInvokeByBools(Func<bool, bool> func)
        {
            downedMushroomMonarch = func(downedMushroomMonarch);
            downedFeudalFungus = func(downedFeudalFungus);
            downedGripsOfChaos = func(downedGripsOfChaos);

            downedTruffleToad = func(downedTruffleToad);
            downedBroodmother = func(downedBroodmother);
            downedHydra = func(downedHydra);

            downedSubzeroSerpent = func(downedSubzeroSerpent);
            downedDesertDjinn = func(downedDesertDjinn);
            downedSagittarius = func(downedSagittarius);

            downedTechnoTruffle = func(downedTechnoTruffle);
            downedRetriever = func(downedRetriever);
            downedOrthrusX = func(downedOrthrusX);

            downedRaiderUltima = func(downedRaiderUltima);
            downedAnubis = func(downedAnubis);
            downedBiomiteCore = func(downedBiomiteCore);

            downedAthena = func(downedAthena);
            downedGreed = func(downedGreed);
            downedRajahRabbit = func(downedRajahRabbit);

            downedForsakenAnubis = func(downedForsakenAnubis);
            downedAthenaA = func(downedAthenaA);
            downedGreedA = func(downedGreedA);

            downedEquinoxWorms = func(downedEquinoxWorms);
            downedSistersOfDiscord = func(downedSistersOfDiscord);
            downedAkuma = func(downedAkuma);

            downedAkumaA = func(downedAkumaA);
            downedYamata = func(downedYamata);
            downedYamataA = func(downedYamataA);

            downedZero = func(downedZero);
            downedZeroP = func(downedZeroP);
            downedRajahRabbitR = func(downedRajahRabbitR);

            downedShen = func(downedShen);
            downedShenA = func(downedShenA);
            downedInfinityZero = func(downedInfinityZero);

            downedSoulOfCthulhu = func(downedSoulOfCthulhu);
            downedCthulhu = func(downedCthulhu);
            zeroUS = func(zeroUS);
            Ancients = func(Ancients);
            ShenSummoned = func(ShenSummoned);
            SistersSummoned = func(SistersSummoned);
            AthenaHerald = func(AthenaHerald);
            AnubisAwakened = func(AnubisAwakened);
            AthenaAwakened = func(AthenaAwakened);
            GreedAwakened = func(GreedAwakened);
            WormActive = func(WormActive);
            StarActive = func(StarActive);
            GravActive = func(GravActive);
        }
        private void AInvokeByBools(Action<bool> action)
        {
            action(downedMushroomMonarch);
            action(downedFeudalFungus);
            action(downedGripsOfChaos);

            action(downedTruffleToad);
            action(downedBroodmother);
            action(downedHydra);

            action(downedSubzeroSerpent);
            action(downedDesertDjinn);
            action(downedSagittarius);

            action(downedTechnoTruffle);
            action(downedRetriever);
            action(downedOrthrusX);

            action(downedRaiderUltima);
            action(downedAnubis);
            action(downedBiomiteCore);

            action(downedAthena);
            action(downedGreed);
            action(downedRajahRabbit);

            action(downedForsakenAnubis);
            action(downedAthenaA);
            action(downedGreedA);

            action(downedEquinoxWorms);
            action(downedSistersOfDiscord);
            action(downedAkuma);

            action(downedAkumaA);
            action(downedYamata);
            action(downedYamataA);

            action(downedZero);
            action(downedZeroP);
            action(downedRajahRabbitR);

            action(downedShen);
            action(downedShenA);
            action(downedInfinityZero);

            action(downedSoulOfCthulhu);
            action(downedCthulhu);
            action(zeroUS);
            action(Ancients);
            action(ShenSummoned);
            action(SistersSummoned);
            action(AthenaHerald);
            action(AnubisAwakened);
            action(AthenaAwakened);
            action(GreedAwakened);
            action(WormActive);
            action(StarActive);
            action(GravActive);
        }

        public override void ClearWorld()
        {
            downedDB = false;
            downedNC = false;
            spawnGrips = false;
            FInvokeByBools(_ => false);
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedMushroomMonarch) tag["downedMushroomMonarch"] = true;
            if (downedFeudalFungus) tag["downedFeudalFungus"] = true;
            if (downedGripsOfChaos) tag["downedGripsOfChaos"] = true;

            if (downedTruffleToad) tag["downedTruffleToad"] = true;
            if (downedBroodmother) tag["downedBroodmother"] = true;
            if (downedHydra) tag["downedHydra"] = true;

            if (downedSubzeroSerpent) tag["downedSubzeroSerpent"] = true;
            if (downedDesertDjinn) tag["downedDesertDjinn"] = true;
            if (downedSagittarius) tag["downedSagittarius"] = true;

            if (downedTechnoTruffle) tag["downedTechnoTruffle"] = true;
            if (downedRetriever) tag["downedRetriever"] = true;
            if (downedOrthrusX) tag["downedOrthrusX"] = true;

            if (downedRaiderUltima) tag["downedRaiderUltima"] = true;
            if (downedAnubis) tag["downedAnubis"] = true;
            if (downedBiomiteCore) tag["downedBiomiteCore"] = true;

            if (downedAthena) tag["downedAthena"] = true;
            if (downedGreed) tag["downedGreed"] = true;
            if (downedRajahRabbit) tag["downedRajahRabbit"] = true;

            if (downedForsakenAnubis) tag["downedForsakenAnubis"] = true;
            if (downedAthenaA) tag["downedAthenaA"] = true;
            if (downedGreedA) tag["downedGreedA"] = true;

            if (downedEquinoxWorms) tag["downedEquinoxWorms"] = true;
            if (downedSistersOfDiscord) tag["downedSistersOfDiscord"] = true;
            if (downedAkuma) tag["downedAkuma"] = true;

            if (downedAkumaA) tag["downedAkumaA"] = true;
            if (downedYamata) tag["downedYamata"] = true;
            if (downedYamataA) tag["downedYamataA"] = true;

            if (downedZero) tag["downedZero"] = true;
            if (downedZeroP) tag["downedZeroP"] = true;
            if (downedRajahRabbitR) tag["downedRajahRabbitR"] = true;

            if (downedShen) tag["downedShen"] = true;
            if (downedShenA) tag["downedShenA"] = true;
            if (downedInfinityZero) tag["downedInfinityZero"] = true;

            if (downedSoulOfCthulhu) tag["downedSoulOfCthulhu"] = true;
            if (downedCthulhu) tag["downedCthulhu"] = true;
            if (zeroUS) tag["zeroUS"] = true;
            if (Ancients) tag["Ancients"] = true;
            if (ShenSummoned) tag["ShenSummoned"] = true;
            if (SistersSummoned) tag["SistersSummoned"] = true;
            if (AthenaHerald) tag["AthenaHerald"] = true;
            if (AnubisAwakened) tag["AnubisAwakened"] = true;
            if (AthenaAwakened) tag["AthenaAwakened"] = true;
            if (GreedAwakened) tag["GreedAwakened"] = true;
            if (WormActive) tag["WormActive"] = true;
            if (StarActive) tag["StarActive"] = true;
            if (GravActive) tag["GravActive"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var legacyDowned = tag.ContainsKey("downed") ? tag.GetList<string>("downed") : new List<string>();

            downedMushroomMonarch = tag.ContainsKey("downedMushroomMonarch");
            downedFeudalFungus = tag.ContainsKey("downedFeudalFungus");
            downedGripsOfChaos = tag.ContainsKey("downedGripsOfChaos") || legacyDowned.Contains("GrabbyHands");

            downedTruffleToad = tag.ContainsKey("downedTruffleToad");
            downedBroodmother = tag.ContainsKey("downedBroodmother");
            downedHydra = tag.ContainsKey("downedHydra");

            downedSubzeroSerpent = tag.ContainsKey("downedSubzeroSerpent");
            downedDesertDjinn = tag.ContainsKey("downedDesertDjinn");
            downedSagittarius = tag.ContainsKey("downedSagittarius");

            downedTechnoTruffle = tag.ContainsKey("downedTechnoTruffle");
            downedRetriever = tag.ContainsKey("downedRetriever");
            downedOrthrusX = tag.ContainsKey("downedOrthrusX");

            downedRaiderUltima = tag.ContainsKey("downedRaiderUltima");
            downedAnubis = tag.ContainsKey("downedAnubis");
            downedBiomiteCore = tag.ContainsKey("downedBiomiteCore");

            downedAthena = tag.ContainsKey("downedAthena");
            downedGreed = tag.ContainsKey("downedGreed");
            downedRajahRabbit = tag.ContainsKey("downedRajahRabbit");

            downedForsakenAnubis = tag.ContainsKey("downedForsakenAnubis");
            downedAthenaA = tag.ContainsKey("downedAthenaA");
            downedGreedA = tag.ContainsKey("downedGreedA");

            downedEquinoxWorms = tag.ContainsKey("downedEquinoxWorms") || legacyDowned.Contains("Equinox");
            downedSistersOfDiscord = tag.ContainsKey("downedSistersOfDiscord") || legacyDowned.Contains("Sisters");
            downedAkuma = tag.ContainsKey("downedAkuma");

            downedAkumaA = tag.ContainsKey("downedAkumaA");
            downedYamata = tag.ContainsKey("downedYamata");
            downedYamataA = tag.ContainsKey("downedYamataA");

            downedZero = tag.ContainsKey("downedZero");
            downedZeroP = tag.ContainsKey("downedZeroP");
            downedRajahRabbitR = tag.ContainsKey("downedRajahRabbitR");

            downedShen = tag.ContainsKey("downedShen");
            downedShenA = tag.ContainsKey("downedShenA");
            downedInfinityZero = tag.ContainsKey("downedInfinityZero");

            downedSoulOfCthulhu = tag.ContainsKey("downedSoulOfCthulhu");
            downedCthulhu = tag.ContainsKey("downedCthulhu");
            zeroUS = tag.ContainsKey("zeroUS") || legacyDowned.Contains("ZUS");
            Ancients = tag.ContainsKey("Ancients") || legacyDowned.Contains("AA");
            ShenSummoned = tag.ContainsKey("ShenSummoned") || legacyDowned.Contains("ShenS");
            SistersSummoned = tag.ContainsKey("SistersSummoned") || legacyDowned.Contains("Summoned");
            AthenaHerald = tag.ContainsKey("AthenaHerald") || legacyDowned.Contains("BitchBird");
            AnubisAwakened = tag.ContainsKey("AnubisAwakened") || legacyDowned.Contains("AnuA");
            AthenaAwakened = tag.ContainsKey("AthenaAwakened");
            GreedAwakened = tag.ContainsKey("GreedAwakened");
            WormActive = tag.ContainsKey("WormActive") || legacyDowned.Contains("WormA");
            StarActive = tag.ContainsKey("StarActive") || legacyDowned.Contains("StarA");
            GravActive = tag.ContainsKey("GravActive") || legacyDowned.Contains("GravA");

            previousDownedAllAncients = DownedAllAncients;
        }

        public static void SyncWorldData()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
        }

        public override void PostUpdateWorld()
        {
            if (NPC.downedMoonlord && !Ancients)
            {
                Ancients = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedMoonlordInfo1"), Color.ForestGreen);
                SyncWorldData();
            }

            if (DownedAllAncients && !previousDownedAllAncients)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedAllAncientsInfo1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.downedAllAncientsInfo2"), Color.Cyan);
                    }
                }
            }

            previousDownedAllAncients = DownedAllAncients;
        }

        public override void NetSend(BinaryWriter writer)
        {
            AInvokeByBools(_ => {
                writer.WriteFlags(_);
            });
        }

        public override void NetReceive(BinaryReader reader)
        {
            FInvokeByBools(_ =>
            {
                reader.ReadFlags(out _);
                return _;
            });
        }
    }
}
