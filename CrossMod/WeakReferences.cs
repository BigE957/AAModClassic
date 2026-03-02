using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.Items.Boss.Yamata;
using AAModClassic.Items.Boss.Rajah.Supreme;
using AAModClassic.Items.Boss.Djinn;
using AAModClassic.Items.Boss.Anubis;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Items.Blocks.Boxes;
using AAModClassic.Items.Boss.Anubis.Forsaken;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.BossSummons;
using AAModClassic.Items.Boss.Rajah;
using AAModClassic.Items.Boss.Greed;
using AAModClassic.Items.Boss.AH;
using AAModClassic.Items.Boss.Athena.Olympian;
using AAModClassic.Items.Boss.Toad;
using AAModClassic.Items.Boss.Sagittarius;
using AAModClassic.Items.Boss.Serpent;
using AAModClassic.Items.Flasks;
using AAModClassic.Items.Boss.Zero;
using AAModClassic.Items.Boss.Equinox;
using AAModClassic.Items.Vanity.Mask;
using AAModClassic.Items.Materials;
using AAModClassic.Items.Boss.Hydra;
using AAModClassic.Items.Boss.Greed.WKG;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.Items.Boss.Grips;
using AAModClassic.Items.Blocks;
using AAModClassic.Items.Usable;

namespace AAModClassic.CrossMod
{
    internal class WeakReferences
    {
        public static void PerformModSupport()
        {
            PerformHealthBarSupport();
            PerformBossChecklistSupport();
            PerformCencusSupport();
            PerformFargosSetup();
        }

        private static void PerformHealthBarSupport()
        {
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");

            if (yabhb != null)
            {
                // Mushroom Monarch
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/MBarHead"),
                    AAMod.instance.GetTexture("Healthbars/MBarBody"),
                    AAMod.instance.GetTexture("Healthbars/MBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Firebrick,
                    Color.Firebrick,
                    Color.Firebrick);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("MushroomMonarch").Type);

                // Feudal Fungus
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/FBarHead"),
                    AAMod.instance.GetTexture("Healthbars/FBarBody"),
                    AAMod.instance.GetTexture("Healthbars/FBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkCyan,
                    Color.DarkCyan,
                    Color.DarkCyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("FeudalFungus").Type);

                // Grip of Chaos (Red)
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/RGCBarHead"),
                    AAMod.instance.GetTexture("Healthbars/RGCBarBody"),
                    AAMod.instance.GetTexture("Healthbars/RGCBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("GripOfChaosRed").Type);

                // Grip of Chaos (Blue)
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/BGCBarHead"),
                    AAMod.instance.GetTexture("Healthbars/BGCBarBody"),
                    AAMod.instance.GetTexture("Healthbars/BGCBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("GripOfChaosBlue").Type);

                // The Broodmother
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/BMBarHead"),
                    AAMod.instance.GetTexture("Healthbars/BMBarBody"),
                    AAMod.instance.GetTexture("Healthbars/BMBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DarkOrange,
                    Color.DarkOrange,
                    Color.DarkOrange);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Broodmother").Type);

                // Hydra
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/HydraBarHead"),
                    AAMod.instance.GetTexture("Healthbars/HydraBarBody"),
                    AAMod.instance.GetTexture("Healthbars/HydraBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Indigo,
                    Color.Indigo,
                    Color.Indigo);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Hydra").Type);

                // Subzero Serpent
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/SSBarHead"),
                    AAMod.instance.GetTexture("Healthbars/SSBarBody"),
                    AAMod.instance.GetTexture("Healthbars/SSBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("SerpentHead").Type);

                // Desert Djinn
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/DDBarHead"),
                    AAMod.instance.GetTexture("Healthbars/DDBarBody"),
                    AAMod.instance.GetTexture("Healthbars/DDBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.IndianRed,
                    Color.IndianRed,
                    Color.IndianRed);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Djinn").Type);

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/SagBarHead"),
                    AAMod.instance.GetTexture("Healthbars/SagBarBody"),
                    AAMod.instance.GetTexture("Healthbars/SagBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Sag").Type);

                //Anubis
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/AnuBarHead"),
                    AAMod.instance.GetTexture("Healthbars/AnuBarBody"),
                    AAMod.instance.GetTexture("Healthbars/AnuBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Anubis").Type);

                // Greed
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/GreedBarHead"),
                    AAMod.instance.GetTexture("Healthbars/GreedBarBody"),
                    AAMod.instance.GetTexture("Healthbars/GreedBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Goldenrod,
                    Color.Goldenrod,
                    Color.Goldenrod);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Greed").Type);

                // Rajah
                    yabhb.Call("hbStart");
                    yabhb.Call("hbSetTexture",
                        AAMod.instance.GetTexture("Healthbars/RajahBarHead"),
                        AAMod.instance.GetTexture("Healthbars/RajahBarBody"),
                        AAMod.instance.GetTexture("Healthbars/RajahBarTail"),
                        AAMod.instance.GetTexture("Healthbars/BarFill"));
                    yabhb.Call("hbSetColours",
                        Color.Orange,
                        Color.Orange,
                        Color.Orange);
                    yabhb.Call("hbSetMidBarOffset", -30, 10);
                    yabhb.Call("hbSetBossHeadCentre", 50, 32);
                    yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                    yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Rajah").Type);
                
                //Forsaken Anubis
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/FAnuBarHead"),
                    AAMod.instance.GetTexture("Healthbars/FAnuBarBody"),
                    AAMod.instance.GetTexture("Healthbars/FAnuBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumAquamarine,
                    Color.MediumAquamarine,
                    Color.MediumAquamarine);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("ForsakenAnubis").Type);

                // Worm King Greed
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/WKGBarHead"),
                    AAMod.instance.GetTexture("Healthbars/WKGBarBody"),
                    AAMod.instance.GetTexture("Healthbars/WKGBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Goldenrod,
                    Color.Goldenrod,
                    Color.Goldenrod);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Greed").Type);

                // Daybringer
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/DBBarHead"),
                    AAMod.instance.GetTexture("Healthbars/DBBarBody"),
                    AAMod.instance.GetTexture("Healthbars/DBBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Cyan,
                    Color.Cyan,
                    Color.Cyan);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("DaybringerHead").Type);

                // Nightcrawler
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/NCBarHead"),
                    AAMod.instance.GetTexture("Healthbars/NCBarBody"),
                    AAMod.instance.GetTexture("Healthbars/NCBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumBlue,
                    Color.MediumBlue,
                    Color.MediumBlue);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("NightcrawlerHead").Type);

                // Haruka Yamata
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/HarukaBarHead"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBarBody"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Haruka").Type);

                // Haruka Yamata (Awakened)
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("HarukaY").Type);

                // Wrath Haruka
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/HarukaBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("WrathHaruka").Type);

                // Ashe Akuma
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("AsheA").Type);

                // Fury Ashe
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Head"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Body"),
                    AAMod.instance.GetTexture("Healthbars/AsheBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("FuryAshe").Type);

                // Yamata
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/YamataBarHead"),
                    AAMod.instance.GetTexture("Healthbars/YamataBarBody"),
                    AAMod.instance.GetTexture("Healthbars/YamataBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Purple,
                    Color.Purple,
                    Color.Purple);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Yamata").Type);

                // Yamata Awakened
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/YamataABarHead"),
                    AAMod.instance.GetTexture("Healthbars/YamataABarBody"),
                    AAMod.instance.GetTexture("Healthbars/YamataABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.MediumVioletRed,
                    Color.MediumVioletRed,
                    Color.MediumVioletRed);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("YamataA").Type);

                // Akuma; Draconian Demon
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/AkumaBarHead"),
                    AAMod.instance.GetTexture("Healthbars/AkumaBarBody"),
                    AAMod.instance.GetTexture("Healthbars/AkumaBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Yellow,
                    Color.Yellow,
                    Color.Yellow);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Akuma").Type);

                // Akuma Awakened; Blazing Fury Incarnate
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/AkumaABarHead"),
                    AAMod.instance.GetTexture("Healthbars/AkumaBarBody"),
                    AAMod.instance.GetTexture("Healthbars/AkumaABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.DeepSkyBlue,
                    Color.DeepSkyBlue,
                    Color.DeepSkyBlue);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("AkumaA").Type);

                // Zero
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ZeroBarHead"),
                    AAMod.instance.GetTexture("Healthbars/ZeroBarBody"),
                    AAMod.instance.GetTexture("Healthbars/ZeroBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Zero").Type);

                // ZER0 PR0T0C0L
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ZeroABarHead"),
                    AAMod.instance.GetTexture("Healthbars/ZeroBarBody"),
                    AAMod.instance.GetTexture("Healthbars/ZeroABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Red,
                    Color.Red,
                    Color.Red);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("ZeroProtocol").Type);

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/SRajahBarHead"),
                    AAMod.instance.GetTexture("Healthbars/SRajahBarBody"),
                    AAMod.instance.GetTexture("Healthbars/SRajahBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Gold,
                    Color.Gold,
                    Color.Gold);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("SupremeRajah").Type);

                // Shen
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ShenBarHead"),
                    AAMod.instance.GetTexture("Healthbars/ShenBarBody"),
                    AAMod.instance.GetTexture("Healthbars/ShenBarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Purple,
                    Color.Purple,
                    Color.Purple);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("Shen").Type);

                //Shen Awakened
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.instance.GetTexture("Healthbars/ShenABarHead"),
                    AAMod.instance.GetTexture("Healthbars/ShenABarBody"),
                    AAMod.instance.GetTexture("Healthbars/ShenABarTail"),
                    AAMod.instance.GetTexture("Healthbars/BarFill"));
                yabhb.Call("hbSetColours",
                    Color.Silver,
                    Color.Silver,
                    Color.Silver);
                yabhb.Call("hbSetMidBarOffset", -30, 10);
                yabhb.Call("hbSetBossHeadCentre", 50, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 16);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("ShenA").Type);
            }
        }

        private static void PerformBossChecklistSupport()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");

            AAMod mod = AAMod.instance;

            if (bossChecklist != null)
            {
                #region Mushroom Monarch
                bossChecklist.Call("AddBoss", 0f, mod.Find<ModNPC>("MushroomMonarch").Type, mod,
                    Lang.BossCheck("MushroomMonarch"),
                    (Func<bool>)(() => AAWorld.downedMonarch),
                    ModContent.ItemType<IntimidatingMushroom>(),
                    new List<int>
                    {
                        ModContent.ItemType<MonarchTrophy>(),
                        ModContent.ItemType<MonarchMask>(),
                        ModContent.ItemType<MonarchBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.MonarchBag>(),
                        ModContent.ItemType<Items.Boss.MushroomMonarch.HeartyTruffle>(),
                        ModContent.ItemType<Mushium>(),
                        ModContent.ItemType<SporeSac>()
                    },
                    Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<IntimidatingMushroom>() + "]",
                    Lang.BossCheck("MushroomMonarchInfo2"),
                    "AAMod/CrossMod/BossChecklist/Monarch",
                    "AAMod/NPCs/Bosses/MushroomMonarch/MushroomMonarch_Head_Boss");
                #endregion

                #region Feudal Fungus
                bossChecklist.Call("AddBoss", 0.1f, mod.Find<ModNPC>("FeudalFungus").Type, mod,
                    Lang.BossCheck("FeudalFungus"),
                    (Func<bool>)(() => AAWorld.downedFungus),
                    ModContent.ItemType<ConfusingMushroom>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.FungusTrophy>(),
                        ModContent.ItemType<FungusMask>(),
                        ModContent.ItemType<FungusBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<FungusBag>(),
                        ModContent.ItemType<MagicTruffle>(),
                        ModContent.ItemType<Items.Boss.MushroomMonarch.GlowingMushium>(),
                        ModContent.ItemType<GlowingSporeSac>()
                    },
                    Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<ConfusingMushroom>() + "]" + Lang.BossCheck("FeudalFungusInfo"),
                    Lang.BossCheck("FeudalFungusInfo2"),
                    "AAMod/CrossMod/BossChecklist/Fungus",
                    "AAMod/NPCs/Bosses/MushroomMonarch/FeudalFungus_Head_Boss");
                #endregion

                #region Grips
                bossChecklist.Call("AddBoss", 2f, mod.Find<ModNPC>("GripOfChaosRed").Type, mod,
                    Lang.BossCheck("GripsofChaos"),
                    (Func<bool>)(() => AAWorld.downedGrips),
                    ModContent.ItemType<CuriousClaw>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Grips.GripTrophyBlue>(),
                        ModContent.ItemType<Items.Boss.Grips.GripTrophyRed>(),
                        ModContent.ItemType<GripMaskBlue>(),
                        ModContent.ItemType<GripMaskRed>(),
                        ModContent.ItemType<GripsBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<GripBag>(),
                        ModContent.ItemType<Items.Boss.Grips.ClawOfChaos>(),
                        ModContent.ItemType<Items.Boss.Grips.ClawBaton>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("CuriousClaw").Type + "]" + Lang.BossCheck("or") + "[i:" + AAMod.instance.Find<ModItem>("InterestingClaw").Type + "]" + Lang.BossCheck("atnight"),
                    Lang.BossCheck("GripsofChaosInfo"),
                    "AAMod/CrossMod/BossChecklist/Grips",
                    "AAMod/CrossMod/BossChecklist/GripsHead");
                #endregion

                #region Truffle Toad
                bossChecklist.Call("AddBoss", 2.5f, mod.Find<ModNPC>("TruffleToad").Type, mod,
                    Lang.BossCheck("TruffleToad"),
                    (Func<bool>)(() => AAWorld.downedToad),
                    ModContent.ItemType<Toadstool>(),
                    new List<int>
                    {
                        ModContent.ItemType<ToadTrophy>(),
                        ModContent.ItemType<ToadMask>(),
                        ModContent.ItemType<ToadBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Toad.ToadBag>(),
                        ModContent.ItemType<Items.Boss.Toad.ToadLeg>(),
                        ModContent.ItemType<Items.Boss.Toad.ToadTongue>(),
                        ModContent.ItemType<Todegun>(),
                        ModContent.ItemType<Items.Boss.Toad.MushrockStaff>(),
                        ModContent.ItemType<GlowingSporeSac>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("Toadstool").Type + "]" + Lang.BossCheck("TruffleToadInfo"),
                    Lang.BossCheck("TruffleToadInfo2"),
                    "AAMod/CrossMod/BossChecklist/Toad",
                    "AAMod/NPCs/Bosses/Toad/TruffleToad_Head_Boss");
                #endregion

                #region Broodmother
                bossChecklist.Call("AddBoss", 3.5f, mod.Find<ModNPC>("Broodmother").Type, mod,
                    Lang.BossCheck("Broodmother"),
                    (Func<bool>)(() => AAWorld.downedBrood),
                    ModContent.ItemType<DragonBell>(),
                    new List<int>
                    {
                        ModContent.ItemType<BroodmotherTrophy>(),
                        ModContent.ItemType<BroodmotherMask>(),
                        ModContent.ItemType<BroodBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Broodmother.BroodBag>(),
                        ModContent.ItemType<DragonCape>(),
                        ModContent.ItemType<BroodScale>(),
                        ModContent.ItemType<Incinerite>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DragonBell").Type + "]" + Lang.BossCheck("BroodmotherInfo"),
                    Lang.BossCheck("BroodmotherInfo2"),
                    "AAMod/CrossMod/BossChecklist/Brood",
                    "AAMod/NPCs/Bosses/Broodmother/Broodmother_Head_Boss");
                #endregion

                #region Hydra
                bossChecklist.Call("AddBoss", 3.5f, mod.Find<ModNPC>("Hydra").Type, mod,
                    Lang.BossCheck("Hydra"),
                    (Func<bool>)(() => AAWorld.downedHydra),
                    ModContent.ItemType<HydraChow>(),
                    new List<int>
                    {
                        ModContent.ItemType<HydraTrophy>(),
                        ModContent.ItemType<HydraMask1>(),
                        ModContent.ItemType<HydraBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Hydra.HydraBag>(),
                        ModContent.ItemType<Items.Boss.Hydra.HydraPendant>(),
                        ModContent.ItemType<Items.Boss.Hydra.HydraHide>(),
                        ModContent.ItemType<Abyssium>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("HydraChow").Type + "]" + Lang.BossCheck("HydraInfo"),
                    Lang.BossCheck("HydraInfo2"),
                    "AAMod/CrossMod/BossChecklist/Hydra",
                    "AAMod/NPCs/Bosses/Hydra/HydraHead1_Head_Boss",
                    null);
                #endregion

                #region Serpent
                bossChecklist.Call("AddBoss", 5.5f, mod.Find<ModNPC>("SerpentHead").Type, mod,
                    Lang.BossCheck("SubzeroSerpent"),
                    (Func<bool>)(() => AAWorld.downedSerpent),
                    ModContent.ItemType<SubzeroCrystal>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentTrophy>(),
                        ModContent.ItemType<SerpentMask>(),
                        ModContent.ItemType<SerpentBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentBag>(),
                        ModContent.ItemType<Items.Boss.Serpent.ArcticMedallion>(),
                        ModContent.ItemType<Items.Boss.Serpent.BlizzardBuster>(),
                        ModContent.ItemType<Icepick>(),
                        ModContent.ItemType<SerpentSpike>(),
                        ModContent.ItemType<Items.Boss.Serpent.SerpentSting>(),
                        ModContent.ItemType<Sickle>(),
                        ModContent.ItemType<Items.Boss.Serpent.SickleShot>(),
                        ModContent.ItemType<Items.Boss.Serpent.SnakeStaff>(),
                        ModContent.ItemType<SnowflakeShuriken>(),
                        ModContent.ItemType<Items.Boss.Serpent.SubzeroSlasher>(),
                        ModContent.ItemType<SnowMana>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("SubzeroCrystal").Type + "]" + Lang.BossCheck("SubzeroSerpentInfo"),
                    Lang.BossCheck("SubzeroSerpentInfo2"),
                    "AAMod/CrossMod/BossChecklist/Serpent1",
                    "AAMod/NPCs/Bosses/Serpent/SerpentHead_Head_Boss");
                #endregion

                #region Djinn
                bossChecklist.Call("AddBoss", 5.5f, mod.Find<ModNPC>("Djinn").Type, mod,
                    Lang.BossCheck("DesertDjinn"),
                    (Func<bool>)(() => AAWorld.downedDjinn),
                    ModContent.ItemType<DjinnLamp>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Djinn.DjinnTrophy>(),
                        ModContent.ItemType<DjinnMask>(),
                        ModContent.ItemType<DjinnBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<DjinnBag>(),
                        ModContent.ItemType<Items.Boss.Djinn.SandstormMedallion>(),
                        ModContent.ItemType<DjinnBag>(),
                        ModContent.ItemType<Items.Boss.Djinn.Djinnerang>(),
                        ModContent.ItemType<Items.Boss.Djinn.Sandagger>(),
                        ModContent.ItemType<SandLamp>(),
                        ModContent.ItemType<SandScepter>(),
                        ModContent.ItemType<Items.Boss.Djinn.SandstormCrossbow>(),
                        ModContent.ItemType<Items.Boss.Djinn.SultanScimitar>(),
                        ModContent.ItemType<DesertMana>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DjinnLamp").Type + "]" + Lang.BossCheck("DesertDjinnInfo"),
                    Lang.BossCheck("DesertDjinnInfo2"),
                    "AAMod/CrossMod/BossChecklist/Djinn",
                    "AAMod/NPCs/Bosses/Djinn/Djinn_Head_Boss");
                #endregion

                #region Sagittarius
                bossChecklist.Call("AddBoss", 5.7f, mod.Find<ModNPC>("Sag").Type, mod,
                    Lang.BossCheck("Sagittarius"),
                    (Func<bool>)(() => AAWorld.downedSag),
                    ModContent.ItemType<Lifescanner>(),
                    new List<int>
                    {
                        ModContent.ItemType<SagTrophy>(),
                        ModContent.ItemType<SagMask>(),
                        ModContent.ItemType<SagBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Sagittarius.SagBag>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.SagShield>(),
                        ModContent.ItemType<Legg>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.NeutronStaff>(),
                        ModContent.ItemType<Items.Boss.Sagittarius.SagCore>(),
                        ModContent.ItemType<Doomite>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("Lifescanner").Type + "]" + Lang.BossCheck("SagittariusInfo"),
                    Lang.BossCheck("SagittariusInfo2"),
                    "AAMod/CrossMod/BossChecklist/Sag",
                    "AAMod/NPCs/Bosses/Sagittarius/Sagittarius_Head_Boss");
                #endregion

                #region Anubis
                bossChecklist.Call("AddBoss", 9.7f, mod.Find<ModNPC>("Anubis").Type, mod,
                    Lang.BossCheck("Anubis"),
                    (Func<bool>)(() => AAWorld.downedAnubis),
                    ModContent.ItemType<Scepter>(),
                    new List<int>
                    {
                        ModContent.ItemType<AnubisTrophy>(),
                        ModContent.ItemType<AnubisMask>(),
                        ModContent.ItemType<AnubisBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.AnubisBag>(),
                        ModContent.ItemType<ArtifactOfJudgement>(),
                        ModContent.ItemType<Items.Boss.Anubis.Judgment>(),
                        ModContent.ItemType<Items.Boss.Anubis.JackalsWrath>(),
                        ModContent.ItemType<Items.Boss.Anubis.NeithsString>(),
                        ModContent.ItemType<SandstormThrower>(),
                        ModContent.ItemType<Items.Boss.Anubis.DesertStaff>(),
                        ModContent.ItemType<Items.Boss.Anubis.SentryOfTheEye>(),
                        ModContent.ItemType<Items.Boss.Anubis.ForsakenFragment>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("Scepter").Type + "]" + Lang.BossCheck("AnubisInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Anubis",
                    "AAMod/NPCs/Bosses/Anubis/Anubis_Head_Boss");
                #endregion

                #region Athena
                bossChecklist.Call("AddBoss", 11.5f, mod.Find<ModNPC>("Athena").Type, mod,
                    Lang.BossCheck("Athena"),
                    (Func<bool>)(() => AAWorld.downedAthena),
                    ModContent.ItemType<Owl>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Athena.AthenaTrophy>(),
                        ModContent.ItemType<AthenaMask>(),
                        ModContent.ItemType<AthenaBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Athena.AthenaBag>(),
                        ModContent.ItemType<Items.Boss.Athena.SeraphHarp>(),
                        ModContent.ItemType<Items.Boss.Athena.SkycutterKopis>(),
                        ModContent.ItemType<Items.Boss.Athena.RazorwindLongbow>(),
                        ModContent.ItemType<Items.Boss.Athena.GaleOfWings>(),
                        ModContent.ItemType<Items.Boss.Athena.DivineWindCharm>(),
                        ModContent.ItemType<Items.Boss.Athena.GoddessFeather>()
                    },
                    Lang.BossCheck("Usean") + "[i:" + AAMod.instance.Find<ModItem>("Owl").Type + "]" + Lang.BossCheck("AthenaInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Athena",
                    "AAMod/NPCs/Bosses/Athena/Athena_Head_Boss");
                #endregion

                #region Greed
                bossChecklist.Call("AddBoss", 11.5f, mod.Find<ModNPC>("Greed").Type, mod,
                    Lang.BossCheck("Greed"),
                    (Func<bool>)(() => AAWorld.downedGreed),
                    ModContent.ItemType<GoldenGrub>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.GreedTrophy>(),
                        ModContent.ItemType<GreedMask>(),
                        ModContent.ItemType<GreedBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.GreedBag>(),
                        ModContent.ItemType<DesireCharm>(),
                        ModContent.ItemType<StoneSlammer>(),
                        ModContent.ItemType<Items.Boss.Greed.GildedGlock>(),
                        ModContent.ItemType<GoldDigger>(),
                        ModContent.ItemType<Miner>(),
                        ModContent.ItemType<StoneShell>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("GoldenGrub").Type + "]" + Lang.BossCheck("GreedInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Greed",
                    "AAMod/NPCs/Bosses/Greed/Greed_Head_Boss");
                #endregion

                #region Rajah Rabbit
                bossChecklist.Call("AddBoss", 11.5f, mod.Find<ModNPC>("Rajah").Type, mod,
                    Lang.BossCheck("RajahRabbit"),
                    (Func<bool>)(() => AAWorld.downedRajah),
                    ModContent.ItemType<GoldenCarrot>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Rajah.RajahTrophy>(),
                        ModContent.ItemType<RajahMask>(),
                        ModContent.ItemType<RajahBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<RajahBag>(),
                        ModContent.ItemType<Items.Boss.Rajah.RajahSash>(),
                        ModContent.ItemType<Items.Boss.Rajah.BaneOfTheBunny>(),
                        ModContent.ItemType<Punisher>(),
                        ModContent.ItemType<Items.Boss.Rajah.Bunzooka>(),
                        ModContent.ItemType<Items.Boss.Rajah.RoyalScepter>(),
                        ModContent.ItemType<Items.Boss.Rajah.CottonCane>(),
                        ModContent.ItemType<RabbitcopterEars>(),
                        ModContent.ItemType<Items.Boss.Rajah.RajahPelt>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("GoldenCarrot").Type + "]" + Lang.BossCheck("RajahRabbitInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Rajah",
                    "AAMod/NPCs/Bosses/Rajah/Rajah_Head_Boss");
                #endregion

                #region Forsaken Anubis
                bossChecklist.Call("AddBoss", 15f, mod.Find<ModNPC>("ForsakenAnubis").Type, mod,
                    Lang.BossCheck("AnubisA"),
                    (Func<bool>)(() => AAWorld.downedAnubisA),
                    ModContent.ItemType<Scepter>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.FAnubisTrophy>(),
                        ModContent.ItemType<FAnubisMask>(),
                        ModContent.ItemType<AnubisFBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<FAnubisBag>(),
                        ModContent.ItemType<ArtifactOfGuilt>(),
                        ModContent.ItemType<Verdict>(),
                        ModContent.ItemType<Soulsplitter>(),
                        ModContent.ItemType<Lifeline>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.CursedFury>(),
                        ModContent.ItemType<ForsakenStaff>(),
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.HorusCane>(),
                        ModContent.ItemType<SoulFragment>()
                    },
                    Lang.BossCheck("AnubisAInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/FAnubis",
                    "AAMod/NPCs/Bosses/Anubis/Forsaken/ForsakenAnubis_Head_Boss");
                #endregion

                #region Olympian Athena
                bossChecklist.Call("AddBoss", 15.1f, mod.Find<ModNPC>("AthenaA").Type, mod,
                    Lang.BossCheck("AthenaA"),
                    (Func<bool>)(() => AAWorld.downedAthenaA),
                    ModContent.ItemType<Owl>(),
                    new List<int>
                    {
                        ModContent.ItemType<AthenaABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<AthenaABag>(),
                        ModContent.ItemType<GoddessHarp>(),
                        ModContent.ItemType<Olympia>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.Windfury>(),
                        ModContent.ItemType<GaleForce>(),
                        ModContent.ItemType<Items.Boss.Athena.Olympian.HurricaneStone>(),
                        ModContent.ItemType<StarChart>()
                    },
                    Lang.BossCheck("AthenaAInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/AthenaA",
                    "AAMod/NPCs/Bosses/Athena/Olympian/AthenaA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedAnubisA));
                #endregion

                #region Worm King Greed
                bossChecklist.Call("AddBoss", 15.2f, mod.Find<ModNPC>("GreedA").Type, mod,
                    Lang.BossCheck("GreedA"),
                    (Func<bool>)(() => AAWorld.downedGreedA),
                    ModContent.ItemType<GoldenGrub>(),
                    new List<int>
                    {
                        ModContent.ItemType<GreedABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.WKG.GreedABag>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.DesireTalisman>(),
                        ModContent.ItemType<Earthbreaker>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.OreCannon>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.OreStaff>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.Unearther>(),
                        ModContent.ItemType<Items.Boss.Greed.WKG.GravitySphere>()
                    },
                    Lang.BossCheck("GreedAInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/GreedA",
                    "AAMod/NPCs/Bosses/Greed/GreedA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedAnubisA));
                #endregion

                #region Equinox Worms
                bossChecklist.Call("AddBoss", 16f, mod.Find<ModNPC>("DaybringerHead").Type, mod,
                    Lang.BossCheck("NightcrawlerDaybringer"),
                    (Func<bool>)(() => AAWorld.downedEquinox),
                    ModContent.ItemType<EquinoxWorm>(),
                    new List<int>
                    {
                        ModContent.ItemType<DBTrophy>(),
                        ModContent.ItemType<Items.Boss.Equinox.NCTrophy>(),
                        ModContent.ItemType<DaybringerMask>(),
                        ModContent.ItemType<NightcrawlerMask>(),
                        ModContent.ItemType<Equibox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Equinox.EquinoxBag>(),
                        ModContent.ItemType<RadiantStar>(),
                        ModContent.ItemType<DarkVoid>(),
                        ModContent.ItemType<Stardust>(),
                        ModContent.ItemType<DarkEnergy>(),
                        ModContent.ItemType<DarkmatterOre>(),
                        ModContent.ItemType<RadiumOre>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("EquinoxWorm").Type + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/Equinox",
                    "AAMod/CrossMod/BossChecklist/EquinoxHead");
                #endregion

                #region Ashe & Haruka
                bossChecklist.Call("AddBoss", 17f, mod.Find<ModNPC>("Ashe").Type, mod,
                    Lang.BossCheck("SistersofDiscord"),
                    (Func<bool>)(() => AAWorld.downedSisters),
                    ModContent.ItemType<FlamesOfAnarchy>(),
                    new List<int>
                    {
                        ModContent.ItemType<AsheTrophy>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaTrophy>(),
                        ModContent.ItemType<SistersBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<AHBag>(),
                        ModContent.ItemType<HeartOfPassion>(),
                        ModContent.ItemType<HeartOfSorrow>(),
                        ModContent.ItemType<AshRain>(),
                        ModContent.ItemType<Items.Boss.AH.FuryFlame>(),
                        ModContent.ItemType<Items.Boss.AH.FireSpiritStaff>(),
                        ModContent.ItemType<AsheSatchel>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaKunai>(),
                        ModContent.ItemType<Items.Boss.AH.Masamune>(),
                        ModContent.ItemType<Items.Boss.AH.MizuArashi>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaBox>()
                    },
                    Lang.BossCheck("Usethe") + "[i:" + AAMod.instance.Find<ModItem>("FlamesOfAnarchy").Type + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/AH",
                    "AAMod/CrossMod/BossChecklist/AHHead");
                #endregion

                #region Akuma
                bossChecklist.Call("AddBoss", 18f, mod.Find<ModNPC>("Akuma").Type, mod,
                    Lang.BossCheck("Akuma"),
                    (Func<bool>)(() => AAWorld.downedAkuma),
                    ModContent.ItemType<DraconianSigil>(),
                    new List<int>
                    {
                        ModContent.ItemType<AkumaTrophy>(),
                        ModContent.ItemType<AkumaMask>(),
                        ModContent.ItemType<AkumaBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Akuma.ReignOfFire>(),
                        ModContent.ItemType<Items.Boss.Akuma.DragonSlasher>(),
                        ModContent.ItemType<Items.Boss.Akuma.Daycrusher>(),
                        ModContent.ItemType<Items.Boss.Akuma.SunSpear>(),
                        ModContent.ItemType<Items.Boss.Akuma.Solar>(),
                        ModContent.ItemType<Items.Boss.Akuma.MorningGlory>(),
                        ModContent.ItemType<Items.Boss.Akuma.RadiantDawn>(),
                        ModContent.ItemType<YOTD>(),
                        ModContent.ItemType<Items.Boss.Akuma.DaybreakArrow>(),
                        ModContent.ItemType<Items.Boss.Akuma.Dawnstrike>(),
                        ModContent.ItemType<Items.Boss.Akuma.SunStorm>(),
                        ModContent.ItemType<Items.Boss.Akuma.Daystorm>(),
                        ModContent.ItemType<Items.Boss.Akuma.LungStaff>(),
                        ModContent.ItemType<Items.Boss.Akuma.AkumaTerratool>(),
                        ModContent.ItemType<CrucibleScale>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DraconianSigil").Type + "]" + Lang.BossCheck("AkumaInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Akuma",
                    "AAMod/NPCs/Bosses/Akuma/Akuma_Head_Boss");

                bossChecklist.Call("AddBoss", 18.05f, mod.Find<ModNPC>("AkumaA").Type, mod,
                    Lang.BossCheck("AkumaA"),
                    (Func<bool>)(() => AAWorld.downedAkuma),
                    ModContent.ItemType<DraconianRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<AkumaATrophy>(),
                        ModContent.ItemType<AkumaAMask>(),
                        ModContent.ItemType<AkumaABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<AkumaBag>(),
                        ModContent.ItemType<TaiyangBaolei>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DraconianRune").Type + "]" + Lang.BossCheck("AkumaInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/AkumaA",
                    "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode));
                #endregion

                #region Yamata
                bossChecklist.Call("AddBoss", 18.1f, mod.Find<ModNPC>("Yamata").Type, mod,
                    Lang.BossCheck("Yamata"),
                    (Func<bool>)(() => AAWorld.downedYamata),
                    ModContent.ItemType<DreadSigil>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.YamataTrophy>(),
                        ModContent.ItemType<YamataMask>(),
                        ModContent.ItemType<YamataBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.AbyssalYari>(),
                        ModContent.ItemType<Hydraslayer>(),
                        ModContent.ItemType<Flairdra>(),
                        ModContent.ItemType<Items.Boss.Yamata.HydraStabber>(),
                        ModContent.ItemType<Crescent>(),
                        ModContent.ItemType<Items.Boss.Yamata.AE>(),
                        ModContent.ItemType<Items.Boss.Yamata.Darksprayer>(),
                        ModContent.ItemType<FallingTwilight>(),
                        ModContent.ItemType<MidnightWrath>(),
                        ModContent.ItemType<Items.Boss.Yamata.Sevenshot>(),
                        ModContent.ItemType<ThrowingCrescent>(),
                        ModContent.ItemType<Items.Boss.Yamata.Toxibomb>(),
                        ModContent.ItemType<YamataTerratool>(),
                        ModContent.ItemType<DreadScale>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DreadSigil").Type + "]" + Lang.BossCheck("YamataInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Yamata",
                    "AAMod/NPCs/Bosses/Yamata/YamataHead_Head_Boss");

                bossChecklist.Call("AddBoss", 18.15f, mod.Find<ModNPC>("YamataA").Type, mod,
                    Lang.BossCheck("YamataA"),
                    (Func<bool>)(() => AAWorld.downedYamata),
                    ModContent.ItemType<DreadRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<YamataATrophy>(),
                        ModContent.ItemType<YamataAMask>(),
                        ModContent.ItemType<YamataABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<YamataBag>(),
                        ModContent.ItemType<Items.Boss.Yamata.Naitokurosu>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DreadRune").Type + "]" + Lang.BossCheck("YamataInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/YamataA",
                    "AAMod/NPCs/Bosses/Yamata/Awakened/YamataAHead_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode));
                #endregion
                
                #region Zero
                bossChecklist.Call("AddBoss", 18.2f, mod.Find<ModNPC>("Zero").Type, mod,
                    Lang.BossCheck("Zero"),
                    (Func<bool>)(() => AAWorld.downedZero),
                    ModContent.ItemType<ZeroTesseract>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroTrophy>(),
                        ModContent.ItemType<ZeroMask>(),
                        ModContent.ItemType<ZeroBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.RiftShredder>(),
                        ModContent.ItemType<EventHorizon>(),
                        ModContent.ItemType<Vortex>(),
                        ModContent.ItemType<Items.Boss.Zero.BHB>(),
                        ModContent.ItemType<Items.Boss.Zero.GenocideCannon>(),
                        ModContent.ItemType<Items.Boss.Zero.Gigataser>(),
                        ModContent.ItemType<Neutralizer>(),
                        ModContent.ItemType<OmegaVolley>(),
                        ModContent.ItemType<RealityCannon>(),
                        ModContent.ItemType<Items.Boss.Zero.TeslaHand>(),
                        ModContent.ItemType<ZeroArrow>(),
                        ModContent.ItemType<Items.Boss.Zero.Battery>(),
                        ModContent.ItemType<VoidStar>(),
                        ModContent.ItemType<Items.Boss.Zero.DoomRay>(),
                        ModContent.ItemType<Items.Boss.Zero.DoomPortal>(),
                        ModContent.ItemType<ZeroTerratool>(),
                        ModContent.ItemType<Items.Boss.Zero.UnstableSingularity>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ZeroTesseract").Type + "]" + Lang.BossCheck("ZeroInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/Zero",
                    "AAMod/NPCs/Bosses/Zero/Zero_Head_Boss");

                bossChecklist.Call("AddBoss", 18.25f, mod.Find<ModNPC>("ZeroProtocol").Type, mod,
                    Lang.BossCheck("ZeroP"),
                    (Func<bool>)(() => AAWorld.downedZero),
                    ModContent.ItemType<ZeroRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroATrophy>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<ZeroBag>(),
                        ModContent.ItemType<BrokenCode>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ZeroRune").Type + "]" + Lang.BossCheck("ZeroInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/ZeroProtocol",
                    "AAMod/NPCs/Bosses/Zero/Protocol/ZeroProtocol_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedZero && Main.expertMode));
                #endregion

                #region Champion Rajah Rabbit
                bossChecklist.Call("AddBoss", 19f, mod.Find<ModNPC>("SupremeRajah").Type, mod,
                    Lang.BossCheck("RajahRabbitRevenge"),
                    (Func<bool>)(() => AAWorld.downedRajahsRevenge),
                    ModContent.ItemType<GoldenCarrot>(),
                    new List<int>
                    {
                            ///ModContent.ItemType<SRajahBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.RajahCache>(),
                        ModContent.ItemType<Items.Boss.Rajah.RajahCape>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.Excalihare>(),
                        ModContent.ItemType<BaneOfTheBunnyEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.PunisherEX>(),
                        ModContent.ItemType<FluffyFury>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.BunzookaEX>(),
                        ModContent.ItemType<RabbitsWrath>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.RoyalScepterEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.CottonCaneEX>(),
                        ModContent.ItemType<Items.Boss.Rajah.Supreme.ChampionPlate>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DiamondCarrot").Type + "]" + Lang.BossCheck("RajahRabbitRevengeInfo"),
                    null,
                    "AAMod/CrossMod/BossChecklist/CRajah",
                    "AAMod/NPCs/Bosses/Rajah/SupremeRajah_Head_Boss");
                #endregion

                #region Shen
                bossChecklist.Call("AddBoss", 20f, mod.Find<ModNPC>("Shen").Type, mod,
                    Lang.BossCheck("ShenDoragon"),
                    (Func<bool>)(() => AAWorld.downedShen),
                    ModContent.ItemType<ChaosSigil>(),
                    new List<int>
                    {
                        ModContent.ItemType<ShenTrophy>(),
                        ModContent.ItemType<ShenMask>(),
                        ModContent.ItemType<ShenBox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<ChaosSlayer>(),
                        ModContent.ItemType<Items.Boss.Shen.Astroid>(),
                        ModContent.ItemType<Items.Boss.Shen.Timesplitter>(),
                        ModContent.ItemType<DraconicRipper>(),
                        ModContent.ItemType<Items.Boss.Shen.FlamingTwilight>(),
                        ModContent.ItemType<Items.Boss.Shen.Skyfall>(),
                        ModContent.ItemType<Items.Boss.Shen.MeteorStrike>(),
                        ModContent.ItemType<Items.Boss.Shen.ShenTerratool>(),
                        ModContent.ItemType<ChaosScale>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ChaosSigil").Type + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/Shen",
                    "AAMod/NPCs/Bosses/Shen/Shen_Head_Boss");

                bossChecklist.Call("AddBoss", 20.1f, mod.Find<ModNPC>("ShenA").Type, mod,
                    Lang.BossCheck("ShenDoragonA"),
                    (Func<bool>)(() => AAWorld.downedShen),
                    ModContent.ItemType<ChaosRune>(),
                    new List<int>
                    {
                        ModContent.ItemType<ShenATrophy>(),
                        ModContent.ItemType<ShenAMask>(),
                        ModContent.ItemType<ShenABox>()
                    },
                    new List<int>
                    {
                        ModContent.ItemType<ShenCache>(),
                        ModContent.ItemType<Items.Boss.Shen.ChaosSoul>()
                    },
                    Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ChaosRune").Type + "]",
                    null,
                    "AAMod/CrossMod/BossChecklist/ShenA",
                    "AAMod/NPCs/Bosses/Shen/Protocol/ShenA_Head_Boss",
                    (Func<bool>)(() => AAWorld.downedShen && Main.expertMode));
                #endregion

                // SlimeKing = 1f;
                // EyeOfCthulhu = 2f;
                // EaterOfWorlds = 3f;
                // QueenBee = 4f;
                // Skeletron = 5f;
                // WallOfFlesh = 6f;
                // TheTwins = 7f;
                // TheDestroyer = 8f;
                // SkeletronPrime = 9f;
                // Plantera = 10f;
                // Golem = 11f;
                // DukeFishron = 12f;
                // LunaticCultist = 13f;
                // Moonlord = 14f;
            }
        }

        private static void PerformCencusSupport()
        {
            Mod censusMod = ModLoader.GetMod("Census");
            if (censusMod != null)
            {
                Mod mod = AAMod.instance;
                // Here I am using Chat Tags to make my condition even more interesting.
                // If you localize your mod, pass in a localized string instead of just English.
                //censusMod.Call("TownNPCCondition", mod.NPCType("Anubis"), $"Have [i:{ItemType<Items.ExampleItem>()}] or [i:{ItemType<Items.Placeable.ExampleBlock>()}] in inventory and build a house out of [i:{ItemType<Items.Placeable.ExampleBlock>()}] and [i:{ItemType<Items.Placeable.ExampleWall>()}]");

                censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Anubis").Type, Lang.CensusMod("Anubis"));
                if (!AAConfigClient.Instance.NoAATownNPC)
                {
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Mushman").Type, Lang.CensusMod("Mushman"));
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Lovecraftian").Type, Lang.CensusMod("Lovecraftian"));
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Samurai").Type, Lang.CensusMod("Samurai"));
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Goblin Slayer").Type, Lang.CensusMod("GoblinSlayer"));
                }
            }
        }

        private static void PerformFargosSetup()
        {
            Mod fargos = ModLoader.GetMod("Fargowiltas");
            if (fargos != null)
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon   
                //item internal name, inline method for retrieving downed value, price to sell for in copper

                fargos.Call("AddSummon", 0f, "AAMod", "IntimidatingMushroom", (Func<bool>)(() => AAWorld.downedMonarch), 20000);
                fargos.Call("AddSummon", 0.1f, "AAMod", "ConfusingMushroom",(Func<bool>)(() => AAWorld.downedFungus), 20000);
                fargos.Call("AddSummon", 2f, "AAMod", "InterestingClaw", (Func<bool>)(() => AAWorld.downedGrips), 80000);
                fargos.Call("AddSummon", 2.5f, "AAMod", "Toadstool", (Func<bool>)(() => AAWorld.downedToad), 80000);
                fargos.Call("AddSummon", 3.5f, "AAMod", "DragonBell", (Func<bool>)(() => AAWorld.downedBrood), 100000);
                fargos.Call("AddSummon", 3.5f, "AAMod", "HydraChow", (Func<bool>)(() => AAWorld.downedHydra), 100000);
                fargos.Call("AddSummon", 5.5f, "AAMod", "SubzeroCrystal", (Func<bool>)(() => AAWorld.downedSerpent), 100000);
                fargos.Call("AddSummon", 5.5f, "AAMod", "DjinnLamp", (Func<bool>)(() => AAWorld.downedDjinn), 100000);
                fargos.Call("AddSummon", 5.7f, "AAMod", "Lifescanner", (Func<bool>)(() => AAWorld.downedSag), 200000);
                fargos.Call("AddSummon", 9.7f, "AAMod", "Scepter", (Func<bool>)(() => AAWorld.downedAnubis), 400000);
                fargos.Call("AddSummon", 9.7f, "AAMod", "Scepter", (Func<bool>)(() => AAWorld.downedAnubis), 400000);
                fargos.Call("AddSummon", 11.5f, "AAMod", "Owl", (Func<bool>)(() => AAWorld.downedAthena), 500000);
                fargos.Call("AddSummon", 11.5f, "AAMod", "GoldenGrub", (Func<bool>)(() => AAWorld.downedGreed), 500000);
                fargos.Call("AddSummon", 11.5f, "AAMod", "GoldenCarrot", (Func<bool>)(() => AAWorld.downedRajah), 600000);
                fargos.Call("AddSummon", 16f, "AAMod", "EquinoxWorm", (Func<bool>)(() => AAWorld.downedEquinox), 1000000);
                fargos.Call("AddSummon", 17f, "AAMod", "FlamesOfAnarchy", (Func<bool>)(() => AAWorld.downedSisters), 1000000);
                fargos.Call("AddSummon", 18f, "AAMod", "DraconianSigil", (Func<bool>)(() => AAWorld.downedAkuma), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAMod", "DraconianRune", (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.1f, "AAMod", "DreadSigil", (Func<bool>)(() => AAWorld.downedYamata), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAMod", "DreadRune", (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.2f, "AAMod", "ZeroTesseract", (Func<bool>)(() => AAWorld.downedZero), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAMod", "ZeroRune", (Func<bool>)(() => AAWorld.downedZero && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 19f, "AAMod", "DiamondCarrot", (Func<bool>)(() => AAWorld.downedRajahsRevenge), 2500000);
                fargos.Call("AddSummon", 20f, "AAMod", "ChaosSigil", (Func<bool>)(() => AAWorld.downedShen), 2500000);
                fargos.Call("AddSummon", 20.5f, "AAMod", "ChaosRune", (Func<bool>)(() => AAWorld.downedShen && Main.expertMode), 4000000);
            }
        }
    }
}
