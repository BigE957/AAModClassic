using AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata;
using AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata.Awakened;
using AAModClassic.___Content.Mire._PreHardmode.Items._BossHydra;
using AAModClassic.___Content.Mire._PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic.___Content.Mire._PreHardmode.NPCs._BossHydra;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.BossStandard;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Globals;
using AAModClassic.Items.Blocks.Boxes;
using AAModClassic.Items.Boss.AH;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Items.Boss.Anubis;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Boss.Equinox;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.Items.Boss.Sagittarius;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Items.Boss.Toad;
using AAModClassic.Items.BossSummons;
using AAModClassic.Items.Vanity.Mask;
using AAModClassic.NPCs.Bosses.AH.Ashe;
using AAModClassic.NPCs.Bosses.AH.Haruka;
using AAModClassic.NPCs.Bosses.Akuma;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;
using AAModClassic.NPCs.Bosses.Anubis;
using AAModClassic.NPCs.Bosses.Anubis.Forsaken;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic.NPCs.Bosses.Broodmother;
using AAModClassic.NPCs.Bosses.Djinn;
using AAModClassic.NPCs.Bosses.Equinox;
using AAModClassic.NPCs.Bosses.FeudalFungus;
using AAModClassic.NPCs.Bosses.Greed;
using AAModClassic.NPCs.Bosses.Grips;
using AAModClassic.NPCs.Bosses.MushroomMonarch;
using AAModClassic.NPCs.Bosses.Rajah;
using AAModClassic.NPCs.Bosses.Sag;
using AAModClassic.NPCs.Bosses.Serpent;
using AAModClassic.NPCs.Bosses.Shen;
using AAModClassic.NPCs.Bosses.Toad;
using AAModClassic.NPCs.Bosses.Zero;
using AAModClassic.NPCs.Bosses.Zero.Protocol;
using AAModClassic.NPCs.TownNPCs;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
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
                    AddBoss(bossChecklist, mod, "MushroomMonarch", 0f, (Func<bool>)(() => NPCExtensions.BeenKilled<MushroomMonarch>()), ModContent.NPCType<MushroomMonarch>(), new Dictionary<string, object>()
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
                    AddBoss(bossChecklist, mod, "FeudalFungus", 0.1f, (Func<bool>)(() => NPCExtensions.BeenKilled<FeudalFungus>()), ModContent.NPCType<FeudalFungus>(), new Dictionary<string, object>()
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
                    AddBoss(bossChecklist, mod, "GripsOfChaos", 2f, (Func<bool>)(() => AAWorld.downedGrips), new List<int>() { ModContent.NPCType<GripOfChaosRed>(), ModContent.NPCType<GripOfChaosBlue>() }, new Dictionary<string, object>()
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
                    AddBoss(bossChecklist, mod, "TruffleToad", 2.69f, (Func<bool>)(() => NPCExtensions.BeenKilled<TruffleToad>()), ModContent.NPCType<TruffleToad>(), new Dictionary<string, object>()
                    {
                        ["displayName"] = Language.GetOrRegister(path + "ToadTitle"),
                        ["spawnInfo"] = Language.GetOrRegister(path + "SpawnToad").WithFormatArgs("[i: " + ModContent.ItemType<Toadstool>() + "]"),
                        ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Toad"),
                        ["spawnItems"] = ModContent.ItemType<Toadstool>(),
                        ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<ToadTrophy>(),
                        ModContent.ItemType<ToadMask>(),
                        ModContent.ItemType<TruffleToadBox>()
                    },
                        ["customPortrait"] = GetPortrait("Toad"),
                        //["availability"] = (Func<bool>)(() => WorldTypeSystem.WorldType != AAWorldType.Beta)
                    });
                    #endregion
                }

                #region Broodmother
                AddBoss(bossChecklist, mod, "Broodmother", 3.97f, (Func<bool>)(() => NPCExtensions.BeenKilled<Broodmother>()), ModContent.NPCType<Broodmother>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "BroodmotherTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnBroodmother").WithFormatArgs("[i: " + ModContent.ItemType<DragonBell>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Broodmother"),
                    ["spawnItems"] = ModContent.ItemType<DragonBell>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<BroodmotherTrophy>(),
                        ModContent.ItemType<BroodmotherMask>(),
                        ModContent.ItemType<BroodmotherBox>()
                    },
                    ["customPortrait"] = GetPortrait("Brood")
                });
                #endregion

                #region Hydra
                AddBoss(bossChecklist, mod, "Hydra", 3.971f, (Func<bool>)(() => NPCExtensions.BeenKilled<HydraBody>()), ModContent.NPCType<HydraBody>(), new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "SubzeroSerpent", 5.5f, (Func<bool>)(() => NPCExtensions.BeenKilled<SerpentHead>()), new List<int>() { ModContent.NPCType<SerpentHead>(), ModContent.NPCType<SerpentBody>(), ModContent.NPCType<SerpentTail>() }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "SubzeroTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnSubzero").WithFormatArgs("[i: " + ModContent.ItemType<SubzeroCrystal>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Subzero"),
                    ["spawnItems"] = ModContent.ItemType<CuriousClaw>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Serpent.SerpentTrophy>(),
                        ModContent.ItemType<SerpentMask>(),
                        ModContent.ItemType<SubzeroBox>()
                    },
                    ["customPortrait"] = GetPortrait("Serpent1"),
                    //["availability"] = (Func<bool>)(() => WorldTypeSystem.WorldType != AAWorldType.Beta)
                });
                #endregion

                #region Djinn
                AddBoss(bossChecklist, mod, "DesertDjinn", 5.5f, (Func<bool>)(() => NPCExtensions.BeenKilled<Djinn>()), ModContent.NPCType<Djinn>(), new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "Sagittarius", 6.6f, (Func<bool>)(() => NPCExtensions.BeenKilled<Sag>()), ModContent.NPCType<Sag>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "SagTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnSag").WithFormatArgs("[i: " + ModContent.ItemType<Lifescanner>() + "]"),
                    ["despawnMessage"] = Language.GetOrRegister(path + "Despawn" + "Sag"),
                    ["spawnItems"] = ModContent.ItemType<Lifescanner>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<SagTrophy>(),
                        ModContent.ItemType<SagMask>(),
                        ModContent.ItemType<SagittariusBox>()
                    },
                    ["customPortrait"] = GetPortrait("Sag")
                });
                #endregion

                #region Anubis
                AddBoss(bossChecklist, mod, "Anubis", 11.69f, (Func<bool>)(() => NPCExtensions.BeenKilled<Anubis>()), ModContent.NPCType<Anubis>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AnubisTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAnubis").WithFormatArgs("[i: " + ModContent.ItemType<Items.BossSummons.Scepter>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("AnubisInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Items.BossSummons.Scepter>(),
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
                AddBoss(bossChecklist, mod, "Athena", 13.551f, (Func<bool>)(() => NPCExtensions.BeenKilled<Athena>()), ModContent.NPCType<Athena>(), new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "Greed", 13.552f, (Func<bool>)(() => NPCExtensions.BeenKilled<Greed>()), new List<int>() { ModContent.NPCType<Greed>(), ModContent.NPCType<GreedBody>() }, new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "RajahRabbit", 13.99f, (Func<bool>)(() => NPCExtensions.BeenKilled<Rajah>()), ModContent.NPCType<Rajah>(), new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "ForsakenAnubis", 18.99f, (Func<bool>)(() => NPCExtensions.BeenKilled<ForsakenAnubis>()), ModContent.NPCType<ForsakenAnubis>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AnubisATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAnubisA"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Items.BossSummons.Scepter>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<Items.Boss.Anubis.Forsaken.FAnubisTrophy>(),
                        ModContent.ItemType<FAnubisMask>(),
                        ModContent.ItemType<AnubisAwakenedBox>()
                    },
                    ["customPortrait"] = GetPortrait("FAnubis")
                });
                #endregion

                #region Olympian Athena
                AddBoss(bossChecklist, mod, "AthenaA", 19.1f, (Func<bool>)(() => NPCExtensions.BeenKilled<AthenaA>()), ModContent.NPCType<AthenaA>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AthenaATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAthenaA"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<Owl>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AthenaAwakenedBox>()
                    },
                    ["customPortrait"] = GetPortrait("AthenaA"),
                    ["availability"] = (Func<bool>)(() => NPCExtensions.BeenKilled<ForsakenAnubis>())
                });
                #endregion

                #region Worm King Greed
                AddBoss(bossChecklist, mod, "GreedA", 19.2f, (Func<bool>)(() => NPCExtensions.BeenKilled<GreedA>()), ModContent.NPCType<GreedA>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "GreedATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnGreedA"),
                    //["despawnMessage"] = Lang.BossCheck("AthenaInfoInfo2"),
                    ["spawnItems"] = ModContent.ItemType<GoldenGrub>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<GreedAwakenedBox>()
                    },
                    ["customPortrait"] = GetPortrait("GreedA"),
                    ["availability"] = (Func<bool>)(() => NPCExtensions.BeenKilled<ForsakenAnubis>())
                });
                #endregion

                #region Equinox Worms
                AddBoss(bossChecklist, mod, "NightcrawlerDaybringer", 20.1f, (Func<bool>)(() => AAWorld.downedEquinox), new List<int>() { ModContent.NPCType<DaybringerHead>(), ModContent.NPCType<NightcrawlerHead>(), ModContent.NPCType<DaybringerBody>(), ModContent.NPCType<NightcrawlerBody>(), ModContent.NPCType<DaybringerTail>(), ModContent.NPCType<NightcrawlerTail>() }, new Dictionary<string, object>()
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
                        ModContent.ItemType<EquinoxBox>()
                    },
                    ["customPortrait"] = GetPortrait("Equinox")
                });
                #endregion

                #region Ashe & Haruka
                AddBoss(bossChecklist, mod, "SistersofDiscord", 20.99f, (Func<bool>)(() => AAWorld.downedSisters), new List<int>() { ModContent.NPCType<Ashe>(), ModContent.NPCType<Haruka>() }, new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "Akuma", 22.01f, () => AAWorld.downedAkuma, new List<int>() { ModContent.NPCType<Akuma>(), ModContent.NPCType<AkumaBody>() }, new Dictionary<string, object>()
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

                AddBoss(bossChecklist, mod, "AkumaA", 22.02f, (Func<bool>)(() => AAWorld.downedAkuma), new List<int>() { ModContent.NPCType<AkumaA>(), ModContent.NPCType<AkumaABody>() }, new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "AkumaATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnAkuma").WithFormatArgs("[i: " + ModContent.ItemType<DraconianRune>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DraconianRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<AkumaATrophy>(),
                        ModContent.ItemType<AkumaAMask>(),
                        ModContent.ItemType<AkumaAwakenedBox>()
                    },
                    ["customPortrait"] = GetPortrait("AkumaA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode)
                });
                #endregion

                #region Yamata
                AddBoss(bossChecklist, mod, "Yamata", 22.1f, (Func<bool>)(() => AAWorld.downedYamata), ModContent.NPCType<YamataBody>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "YamataTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnYamata").WithFormatArgs("[i: " + ModContent.ItemType<DreadSigil>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DreadSigil>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<YamataTrophy>(),
                        ModContent.ItemType<YamataMask>(),
                        ModContent.ItemType<YamataBox>()
                    },
                    ["customPortrait"] = GetPortrait("Yamata")
                });

                AddBoss(bossChecklist, mod, "YamataA", 22.11f, (Func<bool>)(() => AAWorld.downedYamata), ModContent.NPCType<YamataABody>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "YamataATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnYamata").WithFormatArgs("[i: " + ModContent.ItemType<DreadRune>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<DreadRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<YamataATrophy>(),
                        ModContent.ItemType<YamataAMask>(),
                        ModContent.ItemType<YamataAwakenedBox>()
                    },
                    ["customPortrait"] = GetPortrait("YamataA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode)
                });
                #endregion

                #region Zero
                AddBoss(bossChecklist, mod, "Zero", 22.2f, (Func<bool>)(() => AAWorld.downedZero), ModContent.NPCType<Zero>(), new Dictionary<string, object>()
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

                AddBoss(bossChecklist, mod, "ZeroP", 22.21f, (Func<bool>)(() => AAWorld.downedZero), ModContent.NPCType<ZeroProtocol>(), new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "RajahRabbitRevenge", 22.999f, (Func<bool>)(() => NPCExtensions.BeenKilled<SupremeRajah>()), ModContent.NPCType<SupremeRajah>(), new Dictionary<string, object>()
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
                AddBoss(bossChecklist, mod, "ShenDoragon", 24f, (Func<bool>)(() => AAWorld.downedShen), ModContent.NPCType<Shen>(), new Dictionary<string, object>()
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

                AddBoss(bossChecklist, mod, "ShenDoragonA", 24.1f, (Func<bool>)(() => AAWorld.downedShen), ModContent.NPCType<ShenA>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "ShenATitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnShen").WithFormatArgs("[i: " + ModContent.ItemType<ChaosRune>() + "]"),                    
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<ChaosRune>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<ShenATrophy>(),
                        ModContent.ItemType<ShenAMask>(),
                        ModContent.ItemType<ShenAwakenedBox>()
                    },
                    ["customPortrait"] = GetPortrait("ShenA"),
                    ["availability"] = (Func<bool>)(() => AAWorld.downedShen && Main.expertMode)
                });
                #endregion

                AddBoss(bossChecklist, mod, "InfinityZero", 24.11f, (Func<bool>)(() => NPCExtensions.BeenKilled<InfinityZero>()), ModContent.NPCType<InfinityZero>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "InfinityZeroTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnInfinityZero").WithFormatArgs("[i: " + ModContent.ItemType<InfinityOverloader>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<InfinityOverloader>(),
                    ["collectibles"] = new List<int>
                    {
                        ModContent.ItemType<InfinityZeroTrophy>(),
                        //TODO: Add these chuds
                        //ModContent.ItemType<InfinityZeroMask>(),
                        //ModContent.ItemType<InfinityZeroMusicBox>()
                    },
                    ["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) => {
                        Texture2D texture = ModContent.Request<Texture2D>(ModContent.GetInstance<InfinityZero>().Texture).Value;
                        Rectangle frame = texture.Frame(1, 4, 0, 0);
                        Vector2 centered = new(rect.Center.X, rect.Center.Y);
                        sb.Draw(texture, centered, frame, color, 0, frame.Size() * 0.5f, 0.75f, 0, 0);
                    },
                    ["availability"] = (Func<bool>)(() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                });

                AddBoss(bossChecklist, mod, "SoulOfCthulhu", 24.12f, (Func<bool>)(() => NPCExtensions.BeenKilled<SoulOfCthulhu>()), ModContent.NPCType<SoulOfCthulhu>(), new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetOrRegister(path + "SoulOfCthulhuTitle"),
                    ["spawnInfo"] = Language.GetOrRegister(path + "SpawnSoulOfCthulhu").WithFormatArgs("[i: " + ModContent.ItemType<CursedCompass>() + "]"),
                    //["despawnMessage"] = Lang.BossCheck("SubzeroSerpentInfo2"),
                    ["spawnItems"] = ModContent.ItemType<CursedCompass>(),
                    ["collectibles"] = new List<int>
                    {
                        //TODO: Add these chuds
                        //ModContent.ItemType<SoulOfCthulhuTrophy>(),
                        //ModContent.ItemType<SoulOfCthulhuMask>(),
                        //ModContent.ItemType<SoulOfCthulhuBox>()
                    },
                    ["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) => {
                        string path = ModContent.GetInstance<SoulOfCthulhu>().Texture;
                        Texture2D texture2D13 = ModContent.Request<Texture2D>(path).Value;
                        Texture2D WheelTex = ModContent.Request<Texture2D>(path + "_Wheel").Value;
                        Vector2 centered = new(rect.Center.X, rect.Center.Y);
                        Main.spriteBatch.Draw(WheelTex, centered, null, color, 0, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), 1.5f, SpriteEffects.None, 0f);
                        Main.spriteBatch.Draw(texture2D13, centered, null, color, 0, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), 1.5f, SpriteEffects.None, 0f);
                    },
                    ["availability"] = (Func<bool>)(() => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased))
                });
            }
        }

        private static void PerformFargosSetup()
        {
            if (ModLoader.TryGetMod("Fargowiltas", out var fargos))
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon   
                //item internal name, inline method for retrieving downed value, price to sell for in copper

                fargos.Call("AddSummon", 0f, "AAModClassic", "IntimidatingMushroom", (Func<bool>)(() => NPCExtensions.BeenKilled<MushroomMonarch>()), 20000);
                fargos.Call("AddSummon", 0.1f, "AAModClassic", "ConfusingMushroom",(Func<bool>)(() => NPCExtensions.BeenKilled<FeudalFungus>()), 20000);
                fargos.Call("AddSummon", 2f, "AAModClassic", "InterestingClaw", (Func<bool>)(() => AAWorld.downedGrips), 80000);
                fargos.Call("AddSummon", 2.5f, "AAModClassic", "Toadstool", (Func<bool>)(() => NPCExtensions.BeenKilled<TruffleToad>()), 80000);
                fargos.Call("AddSummon", 3.5f, "AAModClassic", "DragonBell", (Func<bool>)(() => NPCExtensions.BeenKilled<Broodmother>()), 100000);
                fargos.Call("AddSummon", 3.5f, "AAModClassic", "HydraChow", (Func<bool>)(() => NPCExtensions.BeenKilled<HydraBody>()), 100000);
                fargos.Call("AddSummon", 5.5f, "AAModClassic", "SubzeroCrystal", (Func<bool>)(() => NPCExtensions.BeenKilled<SerpentHead>()), 100000);
                fargos.Call("AddSummon", 5.5f, "AAModClassic", "DjinnLamp", (Func<bool>)(() => NPCExtensions.BeenKilled<Djinn>()), 100000);
                fargos.Call("AddSummon", 5.7f, "AAModClassic", "Lifescanner", (Func<bool>)(() => NPCExtensions.BeenKilled<Sag>()), 200000);
                fargos.Call("AddSummon", 9.7f, "AAModClassic", "Scepter", (Func<bool>)(() => NPCExtensions.BeenKilled<Anubis>()), 400000);
                fargos.Call("AddSummon", 9.7f, "AAModClassic", "Scepter", (Func<bool>)(() => NPCExtensions.BeenKilled<Anubis>()), 400000);
                fargos.Call("AddSummon", 11.5f, "AAModClassic", "Owl", (Func<bool>)(() => NPCExtensions.BeenKilled<Athena>()), 500000);
                fargos.Call("AddSummon", 11.5f, "AAModClassic", "GoldenGrub", (Func<bool>)(() => NPCExtensions.BeenKilled<Greed>()), 500000);
                fargos.Call("AddSummon", 11.5f, "AAModClassic", "GoldenCarrot", (Func<bool>)(() => NPCExtensions.BeenKilled<Rajah>()), 600000);
                fargos.Call("AddSummon", 16f, "AAModClassic", "EquinoxWorm", (Func<bool>)(() => AAWorld.downedEquinox), 1000000);
                fargos.Call("AddSummon", 17f, "AAModClassic", "FlamesOfAnarchy", (Func<bool>)(() => AAWorld.downedSisters), 1000000);
                fargos.Call("AddSummon", 18f, "AAModClassic", "DraconianSigil", (Func<bool>)(() => AAWorld.downedAkuma), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAModClassic", "DraconianRune", (Func<bool>)(() => AAWorld.downedAkuma && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.1f, "AAModClassic", "DreadSigil", (Func<bool>)(() => AAWorld.downedYamata), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAModClassic", "DreadRune", (Func<bool>)(() => AAWorld.downedYamata && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 18.2f, "AAModClassic", "ZeroTesseract", (Func<bool>)(() => AAWorld.downedZero), 1000000);
                fargos.Call("AddSummon", 18.05f, "AAModClassic", "ZeroRune", (Func<bool>)(() => AAWorld.downedZero && Main.expertMode), 2000000);
                fargos.Call("AddSummon", 19f, "AAModClassic", "DiamondCarrot", (Func<bool>)(() => NPCExtensions.BeenKilled<SupremeRajah>()), 2500000);
                fargos.Call("AddSummon", 20f, "AAModClassic", "ChaosSigil", (Func<bool>)(() => AAWorld.downedShen), 2500000);
                fargos.Call("AddSummon", 20.5f, "AAModClassic", "ChaosRune", (Func<bool>)(() => AAWorld.downedShen && Main.expertMode), 4000000);
            }
        }
    }
}
