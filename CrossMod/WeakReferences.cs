using AAModClassic.Items.Blocks;
using AAModClassic.Items.Blocks.Boxes;
using AAModClassic.Items.Boss.AH;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Items.Boss.Anubis;
using AAModClassic.Items.Boss.Anubis.Forsaken;
using AAModClassic.Items.Boss.Athena.Olympian;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Boss.Djinn;
using AAModClassic.Items.Boss.Equinox;
using AAModClassic.Items.Boss.Greed;
using AAModClassic.Items.Boss.Greed.WKG;
using AAModClassic.Items.Boss.Grips;
using AAModClassic.Items.Boss.Hydra;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.Items.Boss.Rajah;
using AAModClassic.Items.Boss.Rajah.Supreme;
using AAModClassic.Items.Boss.Sagittarius;
using AAModClassic.Items.Boss.Serpent;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Items.Boss.Toad;
using AAModClassic.Items.Boss.Yamata;
using AAModClassic.Items.Boss.Zero;
using AAModClassic.Items.BossSummons;
using AAModClassic.Items.Flasks;
using AAModClassic.Items.Materials;
using AAModClassic.Items.Usable;
using AAModClassic.Items.Vanity.Mask;
using AAModClassic.UI.WorldGen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.CrossMod
{
    internal class WeakReferences
    {
        public static void PerformModSupport()
        {
            PerformHealthBarSupport();
            PerformBossChecklistSupport();
            //PerformCencusSupport();
            PerformFargosSetup();
        }

        private static void PerformHealthBarSupport()
        {
            if (ModLoader.TryGetMod("FKBossHealthBar", out var yabhb))
            {
                // Mushroom Monarch
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.GetTexture("Healthbars/MBarHead"),
                    AAMod.GetTexture("Healthbars/MBarBody"),
                    AAMod.GetTexture("Healthbars/MBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/FBarHead"),
                    AAMod.GetTexture("Healthbars/FBarBody"),
                    AAMod.GetTexture("Healthbars/FBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/RGCBarHead"),
                    AAMod.GetTexture("Healthbars/RGCBarBody"),
                    AAMod.GetTexture("Healthbars/RGCBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/BGCBarHead"),
                    AAMod.GetTexture("Healthbars/BGCBarBody"),
                    AAMod.GetTexture("Healthbars/BGCBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/BMBarHead"),
                    AAMod.GetTexture("Healthbars/BMBarBody"),
                    AAMod.GetTexture("Healthbars/BMBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/HydraBarHead"),
                    AAMod.GetTexture("Healthbars/HydraBarBody"),
                    AAMod.GetTexture("Healthbars/HydraBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/SSBarHead"),
                    AAMod.GetTexture("Healthbars/SSBarBody"),
                    AAMod.GetTexture("Healthbars/SSBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/DDBarHead"),
                    AAMod.GetTexture("Healthbars/DDBarBody"),
                    AAMod.GetTexture("Healthbars/DDBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/SagBarHead"),
                    AAMod.GetTexture("Healthbars/SagBarBody"),
                    AAMod.GetTexture("Healthbars/SagBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/AnuBarHead"),
                    AAMod.GetTexture("Healthbars/AnuBarBody"),
                    AAMod.GetTexture("Healthbars/AnuBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/GreedBarHead"),
                    AAMod.GetTexture("Healthbars/GreedBarBody"),
                    AAMod.GetTexture("Healthbars/GreedBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                        AAMod.GetTexture("Healthbars/RajahBarHead"),
                        AAMod.GetTexture("Healthbars/RajahBarBody"),
                        AAMod.GetTexture("Healthbars/RajahBarTail"),
                        AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/FAnuBarHead"),
                    AAMod.GetTexture("Healthbars/FAnuBarBody"),
                    AAMod.GetTexture("Healthbars/FAnuBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/WKGBarHead"),
                    AAMod.GetTexture("Healthbars/WKGBarBody"),
                    AAMod.GetTexture("Healthbars/WKGBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/DBBarHead"),
                    AAMod.GetTexture("Healthbars/DBBarBody"),
                    AAMod.GetTexture("Healthbars/DBBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/NCBarHead"),
                    AAMod.GetTexture("Healthbars/NCBarBody"),
                    AAMod.GetTexture("Healthbars/NCBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/HarukaBarHead"),
                    AAMod.GetTexture("Healthbars/HarukaBarBody"),
                    AAMod.GetTexture("Healthbars/HarukaBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/HarukaBar2Head"),
                    AAMod.GetTexture("Healthbars/HarukaBar2Body"),
                    AAMod.GetTexture("Healthbars/HarukaBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("HarukaY").Type);

                // Wrath Haruka
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.GetTexture("Healthbars/HarukaBar2Head"),
                    AAMod.GetTexture("Healthbars/HarukaBar2Body"),
                    AAMod.GetTexture("Healthbars/HarukaBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    new Color(122, 157, 152),
                    new Color(122, 157, 152),
                    new Color(122, 157, 152));
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("WrathHaruka").Type);

                // Ashe Akuma
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.GetTexture("Healthbars/AsheBar2Head"),
                    AAMod.GetTexture("Healthbars/AsheBar2Body"),
                    AAMod.GetTexture("Healthbars/AsheBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("AsheA").Type);

                // Fury Ashe
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTextureSmall",
                    AAMod.GetTexture("Healthbars/AsheBar2Head"),
                    AAMod.GetTexture("Healthbars/AsheBar2Body"),
                    AAMod.GetTexture("Healthbars/AsheBar2Tail"),
                    null);
                yabhb.Call("hbSetColours",
                    Color.OrangeRed,
                    Color.OrangeRed,
                    Color.OrangeRed);
                yabhb.Call("hbFinishSingle", AAMod.instance.Find<ModNPC>("FuryAshe").Type);

                // Yamata
                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                    AAMod.GetTexture("Healthbars/YamataBarHead"),
                    AAMod.GetTexture("Healthbars/YamataBarBody"),
                    AAMod.GetTexture("Healthbars/YamataBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/YamataABarHead"),
                    AAMod.GetTexture("Healthbars/YamataABarBody"),
                    AAMod.GetTexture("Healthbars/YamataABarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/AkumaBarHead"),
                    AAMod.GetTexture("Healthbars/AkumaBarBody"),
                    AAMod.GetTexture("Healthbars/AkumaBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/AkumaABarHead"),
                    AAMod.GetTexture("Healthbars/AkumaBarBody"),
                    AAMod.GetTexture("Healthbars/AkumaABarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/ZeroBarHead"),
                    AAMod.GetTexture("Healthbars/ZeroBarBody"),
                    AAMod.GetTexture("Healthbars/ZeroBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/ZeroABarHead"),
                    AAMod.GetTexture("Healthbars/ZeroBarBody"),
                    AAMod.GetTexture("Healthbars/ZeroABarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/SRajahBarHead"),
                    AAMod.GetTexture("Healthbars/SRajahBarBody"),
                    AAMod.GetTexture("Healthbars/SRajahBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/ShenBarHead"),
                    AAMod.GetTexture("Healthbars/ShenBarBody"),
                    AAMod.GetTexture("Healthbars/ShenBarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
                    AAMod.GetTexture("Healthbars/ShenABarHead"),
                    AAMod.GetTexture("Healthbars/ShenABarBody"),
                    AAMod.GetTexture("Healthbars/ShenABarTail"),
                    AAMod.GetTexture("Healthbars/BarFill"));
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
            AAMod mod = AAMod.instance;
            bool bossChecklistLoaded = ModLoader.TryGetMod("BossChecklist", out var bossChecklist);
            mod.Logger.Info($"Mod Checklist is {(!bossChecklistLoaded ? "not" : "")} loaded.");

            if (bossChecklistLoaded)
            {
                void AddBoss(Mod bossChecklist, Mod hostMod, string name, float difficulty, Func<bool> downed, object npcTypes, Dictionary<string, object> extraInfo)
                => bossChecklist.Call("LogBoss", hostMod, name, difficulty, downed, npcTypes, extraInfo);

                Action<SpriteBatch, Rectangle, Color> GetPortrait(string name)
                {
                    Action<SpriteBatch, Rectangle, Color> portrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                        Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/CrossMod/BossChecklist/" + name).Value;
                        Vector2 centered = new Vector2(rect.Center.X - (texture.Width / 2), rect.Center.Y - (texture.Height / 2));
                        sb.Draw(texture, centered, color);
                    };
                    return portrait;
                };

                string path = "Mods.AAModClassic.BossChecklistSupport.";

                if (!ContentReplacementSystem.NeedToReplaceContent)
                {
                    #region Mushroom Monarch
                    AddBoss(bossChecklist, mod, "MushroomMonarch", 0f, (Func<bool>)(() => AAWorld.downedMonarch), mod.Find<ModNPC>("MushroomMonarch").Type, new Dictionary<string, object>()
                    {
                        ["displayName"] = Language.GetOrRegister(path + "MonarchTitle"),
                        ["spawnInfo"] = Language.GetOrRegister(path + "SpawnMonarch").WithFormatArgs("[i: " + ModContent.ItemType<IntimidatingMushroom>() + "]"),
                        ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Monarch"),
                        ["spawnItems"] = ModContent.ItemType<IntimidatingMushroom>(),
                        ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<MonarchTrophy>(),
                        ModContent.ItemType<MonarchMask>(),
                        ModContent.ItemType<MonarchBox>()
                    },
                        ["customPortrait"] = GetPortrait("Monarch")
                    });
                    #endregion

                    #region Feudal Fungus
                    AddBoss(bossChecklist, mod, "FeudalFungus", 0.1f, (Func<bool>)(() => AAWorld.downedFungus), mod.Find<ModNPC>("FeudalFungus").Type, new Dictionary<string, object>()
                    {
                        ["displayName"] = Language.GetOrRegister(path + "FungusTitle"),
                        ["spawnInfo"] = Language.GetOrRegister(path + "SpawnFungus").WithFormatArgs("[i: " + ModContent.ItemType<ConfusingMushroom>() + "]"),
                        ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Fungus"),
                        ["spawnItems"] = ModContent.ItemType<ConfusingMushroom>(),
                        ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.MushroomMonarch.FungusTrophy>(),
                        ModContent.ItemType<FungusMask>(),
                        ModContent.ItemType<FungusBox>()
                    },
                        ["customPortrait"] = GetPortrait("Fungus")
                    });
                    #endregion

                    #region Grips
                    AddBoss(bossChecklist, mod, "GripsOfChaos", 2f, (Func<bool>)(() => AAWorld.downedGrips), new List<int>() { mod.Find<ModNPC>("GripOfChaosRed").Type, mod.Find<ModNPC>("GripOfChaosBlue").Type }, new Dictionary<string, object>()
                    {
                        ["displayName"] = Language.GetOrRegister(path + "GripsTitle"),
                        ["spawnInfo"] = Language.GetOrRegister(path + "SpawnGrips").Format("[i:" + AAMod.instance.Find<ModItem>("CuriousClaw").Type + "]", "[i:" + AAMod.instance.Find<ModItem>("InterestingClaw").Type + "]"),
                        ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Grips"),
                        ["spawnItems"] = new List<int> { ModContent.ItemType<CuriousClaw>(), ModContent.ItemType<InterestingClaw>() },
                        ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Grips.GripTrophyBlue>(),
                        ModContent.ItemType<Items.Boss.Grips.GripTrophyRed>(),
                        ModContent.ItemType<GripMaskBlue>(),
                        ModContent.ItemType<GripMaskRed>(),
                        ModContent.ItemType<GripsBox>()
                    },
                        ["customPortrait"] = GetPortrait("Grips")
                    });
                    #endregion

                    #region Truffle Toad
                    AddBoss(bossChecklist, mod, "TruffleToad", 2.5f, (Func<bool>)(() => AAWorld.downedToad), mod.Find<ModNPC>("TruffleToad").Type, new Dictionary<string, object>()
                    {
                        ["displayName"] = Language.GetOrRegister(path + "ToadTitle"),
                        ["spawnInfo"] = Language.GetOrRegister(path + "SpawnToad").WithFormatArgs("[i: " + ModContent.ItemType<Toadstool>() + "]"),
                        ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Toad"),
                        ["spawnItems"] = ModContent.ItemType<Toadstool>(),
                        ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<ToadTrophy>(),
                        ModContent.ItemType<ToadMask>(),
                        ModContent.ItemType<ToadBox>()
                    },
                        ["customPortrait"] = GetPortrait("Toad"),
                        //["availability"] = (Func<bool>)(() => WorldTypeSystem.WorldType != AAWorldType.Beta)
                    });
                    #endregion
                }

                #region Broodmother
                AddBoss(bossChecklist, mod, "Broodmother", 3.5f, (Func<bool>)(() => AAWorld.downedBrood), mod.Find<ModNPC>("Broodmother").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "BroodmotherTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnBroodmother").WithFormatArgs("[i: " + ModContent.ItemType<DragonBell>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Broodmother"),
                    ["spawnItems"] = ModContent.ItemType<DragonBell>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<BroodmotherTrophy>(),
                        ModContent.ItemType<BroodmotherMask>(),
                        ModContent.ItemType<BroodBox>()
                    },
                    ["customPortrait"] = GetPortrait("Brood")
                });
                #endregion

                #region Hydra
                AddBoss(bossChecklist, mod, "Hydra", 3.5f, (Func<bool>)(() => AAWorld.downedHydra), mod.Find<ModNPC>("Hydra").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "HydraTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnHydra").WithFormatArgs("[i: " + ModContent.ItemType<HydraChow>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Hydra"),
                    ["spawnItems"] = ModContent.ItemType<HydraChow>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<HydraTrophy>(),
                        ModContent.ItemType<HydraMask1>(),
                        ModContent.ItemType<HydraBox>()
                    },
                    ["customPortrait"] = GetPortrait("Hydra")
                });
                #endregion

                #region Serpent
                AddBoss(bossChecklist, mod, "SubzeroSerpent", 5.5f, (Func<bool>)(() => AAWorld.downedSerpent), new List<int>() { mod.Find<ModNPC>("SerpentHead").Type, mod.Find<ModNPC>("SerpentBody").Type, mod.Find<ModNPC>("SerpentTail").Type }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "SubzeroTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnSubzero").WithFormatArgs("[i: " + ModContent.ItemType<SubzeroCrystal>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Subzero"),
                    ["spawnItems"] = ModContent.ItemType<CuriousClaw>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentTrophy>(),
                        ModContent.ItemType<SerpentMask>(),
                        ModContent.ItemType<SerpentBox>()
                    },
                    ["customPortrait"] = GetPortrait("Serpent1"),
                    //["availability"] = (Func<bool>)(() => WorldTypeSystem.WorldType != AAWorldType.Beta)
                });
                #endregion

                #region Djinn
                AddBoss(bossChecklist, mod, "DesertDjinn", 5.5f, (Func<bool>)(() => AAWorld.downedDjinn), mod.Find<ModNPC>("Djinn").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "DjinnTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnDjinn").WithFormatArgs("[i: " + ModContent.ItemType<DjinnLamp>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Djinn"),
                    ["spawnItems"] = ModContent.ItemType<DjinnLamp>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Djinn.DjinnTrophy>(),
                        ModContent.ItemType<DjinnMask>(),
                        ModContent.ItemType<DjinnBox>()
                    },
                    ["customPortrait"] = GetPortrait("Djinn"),
                    //["availability"] = (Func<bool>)(() => WorldTypeSystem.WorldType != AAWorldType.Beta)
                });
                #endregion

                #region Sagittarius
                AddBoss(bossChecklist, mod, "Sagittarius", 6.3f, (Func<bool>)(() => AAWorld.downedSag), mod.Find<ModNPC>("Sag").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "SagTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnSag").WithFormatArgs("[i: " + ModContent.ItemType<Lifescanner>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Sag"),
                    ["spawnItems"] = ModContent.ItemType<Lifescanner>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<SagTrophy>(),
                        ModContent.ItemType<SagMask>(),
                        ModContent.ItemType<SagBox>()
                    },
                    ["customPortrait"] = GetPortrait("Sag")
                });
                #endregion

                #region Anubis
                AddBoss(bossChecklist, mod, "Anubis", 11.7f, (Func<bool>)(() => AAWorld.downedAnubis), mod.Find<ModNPC>("Anubis").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AnubisTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAnubis").WithFormatArgs("[i: " + ModContent.ItemType<Scepter>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("AnubisInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Scepter>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AnubisTrophy>(),
                        ModContent.ItemType<AnubisMask>(),
                        ModContent.ItemType<AnubisBox>()
                    },
                    ["customPortrait"] = GetPortrait("Anubis")
                });
                #endregion

                #region Athena
                AddBoss(bossChecklist, mod, "Athena", 13.5f, (Func<bool>)(() => AAWorld.downedAthena), mod.Find<ModNPC>("Athena").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AthenaTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAthena").WithFormatArgs("[i: " + ModContent.ItemType<Owl>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Owl>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Athena.AthenaTrophy>(),
                        ModContent.ItemType<AthenaMask>(),
                        ModContent.ItemType<AthenaBox>()
                    },
                    ["customPortrait"] = GetPortrait("Athena")
                });
                #endregion

                #region Greed
                AddBoss(bossChecklist, mod, "Greed", 13.5f, (Func<bool>)(() => AAWorld.downedGreed), new List<int>() { mod.Find<ModNPC>("Greed").Type, mod.Find<ModNPC>("GreedBody").Type }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "GreedTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnGreed").WithFormatArgs("[i: " + ModContent.ItemType<GoldenGrub>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("GreedInfo2"),
                    ["spawnItems"] = ModContent.ItemType<GoldenGrub>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Greed.GreedTrophy>(),
                        ModContent.ItemType<GreedMask>(),
                        ModContent.ItemType<GreedBox>()
                    },
                    ["customPortrait"] = GetPortrait("Greed")
                });
                #endregion

                #region Rajah Rabbit
                AddBoss(bossChecklist, mod, "RajahRabbit", 13.5f, (Func<bool>)(() => AAWorld.downedRajah), mod.Find<ModNPC>("Rajah").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "RajahTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnRajah").WithFormatArgs("[i: " + ModContent.ItemType<GoldenCarrot>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<GoldenCarrot>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Rajah.RajahTrophy>(),
                        ModContent.ItemType<RajahMask>(),
                        ModContent.ItemType<RajahBox>()
                    },
                    ["customPortrait"] = GetPortrait("Rajah")
                });
                #endregion

                #region Forsaken Anubis
                AddBoss(bossChecklist, mod, "ForsakenAnubis", 19f, (Func<bool>)(() => AAWorld.downedAnubisA), mod.Find<ModNPC>("ForsakenAnubis").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AnubisATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAnubisA"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Scepter>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.FAnubisTrophy>(),
                        ModContent.ItemType<FAnubisMask>(),
                        ModContent.ItemType<AnubisFBox>()
                    },
                    ["customPortrait"] = GetPortrait("FAnubis")
                });
                #endregion

                #region Olympian Athena
                AddBoss(bossChecklist, mod, "AthenaA", 19.1f, (Func<bool>)(() => AAWorld.downedAthenaA), mod.Find<ModNPC>("AthenaA").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AthenaATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAthenaA"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Owl>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AthenaABox>()
                    },
                    ["customPortrait"] = GetPortrait("AthenaA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedAnubisA)
                });
                #endregion

                #region Worm King Greed
                AddBoss(bossChecklist, mod, "GreedA", 19.2f, (Func<bool>)(() => AAWorld.downedGreedA), mod.Find<ModNPC>("GreedA").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "GreedATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnGreedA"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<GoldenGrub>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<GreedABox>()
                    },
                    ["customPortrait"] = GetPortrait("GreedA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedAnubisA)
                });
                #endregion

                #region Equinox Worms
                AddBoss(bossChecklist, mod, "NightcrawlerDaybringer", 20f, (Func<bool>)(() => AAWorld.downedEquinox), new List<int>() { mod.Find<ModNPC>("DaybringerHead").Type, mod.Find<ModNPC>("NightcrawlerHead").Type, mod.Find<ModNPC>("DaybringerBody").Type, mod.Find<ModNPC>("NightcrawlerBody").Type, mod.Find<ModNPC>("DaybringerTail").Type, mod.Find<ModNPC>("NightcrawlerTail").Type }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "EquinoxTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnEquinox").WithFormatArgs("[i: " + ModContent.ItemType<EquinoxWorm>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<EquinoxWorm>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<DBTrophy>(),
                        ModContent.ItemType<Items.Boss.Equinox.NCTrophy>(),
                        ModContent.ItemType<DaybringerMask>(),
                        ModContent.ItemType<NightcrawlerMask>(),
                        ModContent.ItemType<Equibox>()
                    },
                    ["customPortrait"] = GetPortrait("Equinox")
                });
                #endregion

                #region Ashe & Haruka
                AddBoss(bossChecklist, mod, "SistersofDiscord", 21f, (Func<bool>)(() => AAWorld.downedSisters), new List<int>() { mod.Find<ModNPC>("Ashe").Type, mod.Find<ModNPC>("Haruka").Type }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AHTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAH").WithFormatArgs("[i: " + ModContent.ItemType<FlamesOfAnarchy>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("GripsofChaosInfo"),
                    ["spawnItems"] = ModContent.ItemType<FlamesOfAnarchy>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AsheTrophy>(),
                        ModContent.ItemType<Items.Boss.AH.HarukaTrophy>(),
                        ModContent.ItemType<SistersBox>()
                    },
                    ["customPortrait"] = GetPortrait("AH")
                });
                #endregion

                #region Akuma
                AddBoss(bossChecklist, mod, "Akuma", 22f, (Func<bool>)(() => AAWorld.downedAkuma), new List<int>() { mod.Find<ModNPC>("Akuma").Type, mod.Find<ModNPC>("AkumaBody").Type }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AkumaTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAkuma").WithFormatArgs("[i: " + ModContent.ItemType<DraconianSigil>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DraconianSigil>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AkumaTrophy>(),
                        ModContent.ItemType<AkumaMask>(),
                        ModContent.ItemType<AkumaBox>()
                    },
                    ["customPortrait"] = GetPortrait("Akuma")
                });

                AddBoss(bossChecklist, mod, "AkumaA", 22.05f, (Func<bool>)(() => AAWorld.downedAkuma), new List<int>() { mod.Find<ModNPC>("AkumaA").Type, mod.Find<ModNPC>("AkumaABody").Type }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AkumaATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAkuma").WithFormatArgs("[i: " + ModContent.ItemType<DraconianRune>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DraconianRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AkumaATrophy>(),
                        ModContent.ItemType<AkumaAMask>(),
                        ModContent.ItemType<AkumaABox>()
                    },
                    ["customPortrait"] = GetPortrait("AkumaA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode)
                });
                #endregion

                #region Yamata
                AddBoss(bossChecklist, mod, "Yamata", 22.1f, (Func<bool>)(() => AAWorld.downedYamata), mod.Find<ModNPC>("Yamata").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "YamataTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnYamata").WithFormatArgs("[i: " + ModContent.ItemType<DreadSigil>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DreadSigil>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Yamata.YamataTrophy>(),
                        ModContent.ItemType<YamataMask>(),
                        ModContent.ItemType<YamataBox>()
                    },
                    ["customPortrait"] = GetPortrait("Yamata")
                });

                AddBoss(bossChecklist, mod, "YamataA", 22.15f, (Func<bool>)(() => AAWorld.downedYamata), mod.Find<ModNPC>("YamataA").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "YamataATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnYamata").WithFormatArgs("[i: " + ModContent.ItemType<DreadRune>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DreadRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<YamataATrophy>(),
                        ModContent.ItemType<YamataAMask>(),
                        ModContent.ItemType<YamataABox>()
                    },
                    ["customPortrait"] = GetPortrait("YamataA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode)
                });
                #endregion

                #region Zero
                AddBoss(bossChecklist, mod, "Zero", 22.2f, (Func<bool>)(() => AAWorld.downedZero), mod.Find<ModNPC>("Zero").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "ZeroTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnZero").WithFormatArgs("[i: " + ModContent.ItemType<ZeroTesseract>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<ZeroTesseract>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroTrophy>(),
                        ModContent.ItemType<ZeroMask>(),
                        ModContent.ItemType<ZeroBox>()
                    },
                    ["customPortrait"] = GetPortrait("Zero")
                });

                AddBoss(bossChecklist, mod, "ZeroP", 22.25f, (Func<bool>)(() => AAWorld.downedZero), mod.Find<ModNPC>("ZeroProtocol").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "ZeroPTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnZero").WithFormatArgs("[i: " + ModContent.ItemType<ZeroRune>() + "]"),                    
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<ZeroRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Zero.ZeroATrophy>()
                    },
                    ["customPortrait"] = GetPortrait("ZeroProtocol"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedZero && Main.expertMode)
                });
                #endregion

                #region Champion Rajah Rabbit
                AddBoss(bossChecklist, mod, "RajahRabbitRevenge", 23f, (Func<bool>)(() => AAWorld.downedRajahsRevenge), mod.Find<ModNPC>("SupremeRajah").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "RajahRTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnRajahR").WithFormatArgs("[i: " + ModContent.ItemType<DiamondCarrot>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DiamondCarrot>(),
                    ["collectibles"] = new List<int>
                    {
                        ///ModContent.ItemType<SRajahBox>()
                    },
                    ["customPortrait"] = GetPortrait("CRajah"),
                    //["availability"] = (Func<bool>)(() => WorldTypeSystem.WorldType != AAWorldType.Beta)
                });
                #endregion

                #region Shen
                AddBoss(bossChecklist, mod, "ShenDoragon", 24f, (Func<bool>)(() => AAWorld.downedShen), mod.Find<ModNPC>("Shen").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "ShenTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnShen").WithFormatArgs("[i: " + ModContent.ItemType<ChaosSigil>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<ChaosSigil>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<ShenTrophy>(),
                        ModContent.ItemType<ShenMask>(),
                        ModContent.ItemType<ShenBox>()
                    },
                    ["customPortrait"] = GetPortrait("Shen")
                });

                AddBoss(bossChecklist, mod, "ShenDoragonA", 24.1f, (Func<bool>)(() => AAWorld.downedShen), mod.Find<ModNPC>("ShenA").Type, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "ShenATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnShen").WithFormatArgs("[i: " + ModContent.ItemType<ChaosRune>() + "]"),                    
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<ChaosRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<ShenATrophy>(),
                        ModContent.ItemType<ShenAMask>(),
                        ModContent.ItemType<ShenABox>()
                    },
                    ["customPortrait"] = GetPortrait("ShenA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedShen && Main.expertMode)
                });
                #endregion
            }
        }

        /*
        private static void PerformCencusSupport()
        {
            if (ModLoader.TryGetMod("Census", out var censusMod))
            {
                Mod mod = AAMod.instance;
                // Here I am using Chat Tags to make my condition even more interesting.
                // If you localize your mod, pass in a localized string instead of just English.
                //censusMod.Call("TownNPCCondition", mod.NPCType("Anubis"), $"Have [i:{ItemType<Items.ExampleItem>()}] or [i:{ItemType<Items.Placeable.ExampleBlock>()}] in inventory and build a house out of [i:{ItemType<Items.Placeable.ExampleBlock>()}] and [i:{ItemType<Items.Placeable.ExampleWall>()}]");

                censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Legendscribe").Type, Lang.CensusMod("Legendscribe"));
                if (!AAConfigClient.Instance.NoAATownNPC)
                {
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Mushman").Type, Lang.CensusMod("Mushman"));
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Lovecraftian").Type, Lang.CensusMod("Lovecraftian"));
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("Samurai").Type, Lang.CensusMod("Samurai"));
                    censusMod.Call("TownNPCCondition", mod.Find<ModNPC>("GoblinSlayer").Type, Lang.CensusMod("GoblinSlayer"));
                }
            }
        }
        */

        private static void PerformFargosSetup()
        {
            if (ModLoader.TryGetMod("Fargowiltas", out var fargos))
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon   
                //item internal name, inline method for retrieving downed value, price to sell for in copper

                fargos.Call("AddSummon", 0f, "AAModClassic", "IntimidatingMushroom", (Func<bool>)(() => AAWorld.downedMonarch), 20000);
                fargos.Call("AddSummon", 0.1f, "AAModClassic", "ConfusingMushroom",(Func<bool>)(() => AAWorld.downedFungus), 20000);
                fargos.Call("AddSummon", 2f, "AAModClassic", "InterestingClaw", (Func<bool>)(() => AAWorld.downedGrips), 80000);
                fargos.Call("AddSummon", 2.5f, "AAModClassic", "Toadstool", (Func<bool>)(() => AAWorld.downedToad), 80000);
                fargos.Call("AddSummon", 3.5f, "AAModClassic", "DragonBell", (Func<bool>)(() => AAWorld.downedBrood), 100000);
                fargos.Call("AddSummon", 3.5f, "AAModClassic", "HydraChow", (Func<bool>)(() => AAWorld.downedHydra), 100000);
                fargos.Call("AddSummon", 5.5f, "AAModClassic", "SubzeroCrystal", (Func<bool>)(() => AAWorld.downedSerpent), 100000);
                fargos.Call("AddSummon", 5.5f, "AAModClassic", "DjinnLamp", (Func<bool>)(() => AAWorld.downedDjinn), 100000);
                fargos.Call("AddSummon", 5.7f, "AAModClassic", "Lifescanner", (Func<bool>)(() => AAWorld.downedSag), 200000);
                fargos.Call("AddSummon", 9.7f, "AAModClassic", "Scepter", (Func<bool>)(() => AAWorld.downedAnubis), 400000);
                fargos.Call("AddSummon", 9.7f, "AAModClassic", "Scepter", (Func<bool>)(() => AAWorld.downedAnubis), 400000);
                fargos.Call("AddSummon", 11.5f, "AAModClassic", "Owl", (Func<bool>)(() => AAWorld.downedAthena), 500000);
                fargos.Call("AddSummon", 11.5f, "AAModClassic", "GoldenGrub", (Func<bool>)(() => AAWorld.downedGreed), 500000);
                fargos.Call("AddSummon", 11.5f, "AAModClassic", "GoldenCarrot", (Func<bool>)(() => AAWorld.downedRajah), 600000);
                fargos.Call("AddSummon", 16f, "AAModClassic", "EquinoxWorm", (Func<bool>)(() => AAWorld.downedEquinox), 1000000);
                fargos.Call("AddSummon", 17f, "AAModClassic", "FlamesOfAnarchy", (Func<bool>)(() => AAWorld.downedSisters), 1000000);
                fargos.Call("AddSummon", 18f, "AAModClassic", "DraconianSigil", (Func<bool>)(() => AAWorld.downedAkuma), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAModClassic", "DraconianRune", (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.1f, "AAModClassic", "DreadSigil", (Func<bool>)(() => AAWorld.downedYamata), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAModClassic", "DreadRune", (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.2f, "AAModClassic", "ZeroTesseract", (Func<bool>)(() => AAWorld.downedZero), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAModClassic", "ZeroRune", (Func<bool>)(() => AAWorld.downedZero && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 19f, "AAModClassic", "DiamondCarrot", (Func<bool>)(() => AAWorld.downedRajahsRevenge), 2500000);
                fargos.Call("AddSummon", 20f, "AAModClassic", "ChaosSigil", (Func<bool>)(() => AAWorld.downedShen), 2500000);
                fargos.Call("AddSummon", 20.5f, "AAModClassic", "ChaosRune", (Func<bool>)(() => AAWorld.downedShen && Main.expertMode), 4000000);
            }
        }
    }
}
