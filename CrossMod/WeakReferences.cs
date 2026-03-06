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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

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
            if (ModLoader.TryGetMod("FKBossHealthBar", out var yabhb))
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
                }
                ;

                #region Mushroom Monarch
                AddBoss(bossChecklist, mod, "MushroomMonarch", 0f, (Func<bool>)(() => AAWorld.downedMonarch), mod.Find<ModNPC>("MushroomMonarch").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("MushroomMonarch"),
                    ["spawnInfo"] = Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<IntimidatingMushroom>() + "]",
                    ["despawnMessage"] = Lang.BossCheck("MushroomMonarchInfo2"),
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
                    ["DisplayName"] = Lang.BossCheck("FeudalFungus"),
                    ["spawnInfo"] = Lang.BossCheck("Usean") + "[i: " + ModContent.ItemType<ConfusingMushroom>() + "]" + Lang.BossCheck("FeudalFungusInfo"),
                    ["despawnMessage"] = Lang.BossCheck("FeudalFungusInfo2"),
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
                    ["DisplayName"] = Lang.BossCheck("GripsofChaos"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("CuriousClaw").Type + "]" + Lang.BossCheck("or") + "[i:" + AAMod.instance.Find<ModItem>("InterestingClaw").Type + "]" + Lang.BossCheck("atnight"),
                    ["despawnMessage"] = Lang.BossCheck("GripsofChaosInfo"),
                    ["spawnItems"] = ModContent.ItemType<CuriousClaw>(),
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
                    ["DisplayName"] = Lang.BossCheck("TruffleToad"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("Toadstool").Type + "]" + Lang.BossCheck("TruffleToadInfo"),
                    ["despawnMessage"] = Lang.BossCheck("TruffleToadInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Toadstool>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<ToadTrophy>(),
                        ModContent.ItemType<ToadMask>(),
                        ModContent.ItemType<ToadBox>()
                    },
                    ["customPortrait"] = GetPortrait("Toad")
                });
                #endregion

                #region Broodmother
                AddBoss(bossChecklist, mod, "Broodmother", 3.5f, (Func<bool>)(() => AAWorld.downedBrood), mod.Find<ModNPC>("Broodmother").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Broodmother"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DragonBell").Type + "]" + Lang.BossCheck("BroodmotherInfo"),
                    ["despawnMessage"] = Lang.BossCheck("BroodmotherInfo2"),
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
                    ["DisplayName"] = Lang.BossCheck("Hydra"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("HydraChow").Type + "]" + Lang.BossCheck("HydraInfo"),
                    ["despawnMessage"] = Lang.BossCheck("HydraInfo2"),
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
                    ["DisplayName"] = Lang.BossCheck("SubzeroSerpent"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("SubzeroCrystal").Type + "]" + Lang.BossCheck(""),
                    ["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<CuriousClaw>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentTrophy>(),
                        ModContent.ItemType<SerpentMask>(),
                        ModContent.ItemType<SerpentBox>()
                    },
                    ["customPortrait"] = GetPortrait("Serpent1")
                });
                #endregion

                #region Djinn
                AddBoss(bossChecklist, mod, "DesertDjinn", 5.5f, (Func<bool>)(() => AAWorld.downedDjinn), mod.Find<ModNPC>("Djinn").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("DesertDjinn"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DjinnLamp").Type + "]" + Lang.BossCheck("DesertDjinnInfo"),
                    ["despawnMessage"] = Lang.BossCheck("DesertDjinnInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DjinnLamp>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Djinn.DjinnTrophy>(),
                        ModContent.ItemType<DjinnMask>(),
                        ModContent.ItemType<DjinnBox>()
                    },
                    ["customPortrait"] = GetPortrait("Djinn")
                });
                #endregion

                #region Sagittarius
                AddBoss(bossChecklist, mod, "Sagittarius", 5.7f, (Func<bool>)(() => AAWorld.downedSag), mod.Find<ModNPC>("Sag").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Sagittarius"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("Lifescanner").Type + "]" + Lang.BossCheck("SagittariusInfo"),
                    ["despawnMessage"] = Lang.BossCheck("SagittariusInfo2"),
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
                AddBoss(bossChecklist, mod, "Anubis", 9.7f, (Func<bool>)(() => AAWorld.downedAnubis), mod.Find<ModNPC>("Anubis").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Anubis"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("Scepter").Type + "]" + Lang.BossCheck("AnubisInfo"),
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
                AddBoss(bossChecklist, mod, "Athena", 11.5f, (Func<bool>)(() => AAWorld.downedAthena), mod.Find<ModNPC>("Athena").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Athena"),
                    ["spawnInfo"] = Lang.BossCheck("Usean") + "[i:" + AAMod.instance.Find<ModItem>("Owl").Type + "]" + Lang.BossCheck("AthenaInfo"),
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
                AddBoss(bossChecklist, mod, "Greed", 11.5f, (Func<bool>)(() => AAWorld.downedGreed), new List<int>() { mod.Find<ModNPC>("Greed").Type, mod.Find<ModNPC>("GreedBody").Type }, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Greed"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("GoldenGrub").Type + "]" + Lang.BossCheck("GreedInfo"),
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
                AddBoss(bossChecklist, mod, "RajahRabbit", 11.5f, (Func<bool>)(() => AAWorld.downedRajah), mod.Find<ModNPC>("Rajah").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("RajahRabbit"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("GoldenCarrot").Type + "]" + Lang.BossCheck("RajahRabbitInfo"),
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
                AddBoss(bossChecklist, mod, "ForsakenAnubis", 15f, (Func<bool>)(() => AAWorld.downedAnubisA), mod.Find<ModNPC>("ForsakenAnubis").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("AnubisA"),
                    ["spawnInfo"] = Lang.BossCheck("AnubisAInfo"),
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
                AddBoss(bossChecklist, mod, "AthenaA", 15.1f, (Func<bool>)(() => AAWorld.downedAthenaA), mod.Find<ModNPC>("AthenaA").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("AthenaA"),
                    ["spawnInfo"] = Lang.BossCheck("AthenaAInfo"),
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
                AddBoss(bossChecklist, mod, "GreedA", 15.2f, (Func<bool>)(() => AAWorld.downedGreedA), mod.Find<ModNPC>("GreedA").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("GreedA"),
                    ["spawnInfo"] = Lang.BossCheck("GreedAInfo"),
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
                AddBoss(bossChecklist, mod, "NightcrawlerDaybringer", 16f, (Func<bool>)(() => AAWorld.downedEquinox), new List<int>() { mod.Find<ModNPC>("DaybringerHead").Type, mod.Find<ModNPC>("NightcrawlerHead").Type, mod.Find<ModNPC>("DaybringerBody").Type, mod.Find<ModNPC>("NightcrawlerBody").Type, mod.Find<ModNPC>("DaybringerTail").Type, mod.Find<ModNPC>("NightcrawlerTail").Type }, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("NightcrawlerDaybringer"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("EquinoxWorm").Type + "]",
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
                AddBoss(bossChecklist, mod, "SistersofDiscord", 17f, (Func<bool>)(() => AAWorld.downedSisters), new List<int>() { mod.Find<ModNPC>("Ashe").Type, mod.Find<ModNPC>("Haruka").Type }, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("SistersofDiscord"),
                    ["spawnInfo"] = Lang.BossCheck("Usethe") + "[i:" + AAMod.instance.Find<ModItem>("FlamesOfAnarchy").Type + "]",
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
                AddBoss(bossChecklist, mod, "Akuma", 18f, (Func<bool>)(() => AAWorld.downedAkuma), new List<int>() { mod.Find<ModNPC>("Akuma").Type, mod.Find<ModNPC>("AkumaBody").Type }, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Akuma"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DraconianSigil").Type + "]" + Lang.BossCheck("AkumaInfo"),
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

                AddBoss(bossChecklist, mod, "AkumaA", 18.05f, (Func<bool>)(() => AAWorld.downedAkuma), new List<int>() { mod.Find<ModNPC>("AkumaA").Type, mod.Find<ModNPC>("AkumaABody").Type }, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("AkumaA"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DraconianRune").Type + "]" + Lang.BossCheck("AkumaInfo"),
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
                AddBoss(bossChecklist, mod, "Yamata", 18.1f, (Func<bool>)(() => AAWorld.downedYamata), mod.Find<ModNPC>("Yamata").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Yamata"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DreadSigil").Type + "]" + Lang.BossCheck("YamataInfo"),
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

                AddBoss(bossChecklist, mod, "YamataA", 18.15f, (Func<bool>)(() => AAWorld.downedYamata), mod.Find<ModNPC>("YamataA").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("YamataA"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DreadRune").Type + "]" + Lang.BossCheck("YamataInfo"),
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
                AddBoss(bossChecklist, mod, "Zero", 18.2f, (Func<bool>)(() => AAWorld.downedZero), mod.Find<ModNPC>("Zero").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("Zero"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ZeroTesseract").Type + "]" + Lang.BossCheck("ZeroInfo"),
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

                AddBoss(bossChecklist, mod, "ZeroP", 18.25f, (Func<bool>)(() => AAWorld.downedZero), mod.Find<ModNPC>("ZeroProtocol").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("ZeroP"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ZeroRune").Type + "]" + Lang.BossCheck("ZeroInfo"),
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
                AddBoss(bossChecklist, mod, "RajahRabbitRevenge", 19f, (Func<bool>)(() => AAWorld.downedRajahsRevenge), mod.Find<ModNPC>("SupremeRajah").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("RajahRabbitRevenge"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("DiamondCarrot").Type + "]" + Lang.BossCheck("RajahRabbitRevengeInfo"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<GoldenCarrot>(),
                    ["collectibles"] = new List<int>
                    {
                        ///ModContent.ItemType<SRajahBox>()
                    },
                    ["customPortrait"] = GetPortrait("CRajah")
                });
                #endregion

                #region Shen
                AddBoss(bossChecklist, mod, "ShenDoragon", 20f, (Func<bool>)(() => AAWorld.downedShen), mod.Find<ModNPC>("Shen").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("ShenDoragon"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ChaosSigil").Type + "]",
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

                AddBoss(bossChecklist, mod, "ShenDoragonA", 20.1f, (Func<bool>)(() => AAWorld.downedShen), mod.Find<ModNPC>("ShenA").Type, new Dictionary<string, object>()
                {
                    ["DisplayName"] = Lang.BossCheck("ShenDoragonA"),
                    ["spawnInfo"] = Lang.BossCheck("Usea") + "[i:" + AAMod.instance.Find<ModItem>("ChaosRune").Type + "]",
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

        private static void PerformFargosSetup()
        {
            if (ModLoader.TryGetMod("Fargowiltas", out var fargos))
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
