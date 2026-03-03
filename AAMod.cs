using AAModClassic;
using AAModClassic.Backgrounds;
using AAModClassic.Base.BaseMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.NPCs;
using AAModClassic.Base.Projectiles;
using AAModClassic.CrossMod;
using AAModClassic.Globals;
using AAModClassic.Items.Dev.Invoker;
using AAModClassic.UI;
using AAModClassic.UI.Core;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;

namespace AAModClassic
{
    public class AAMod : Mod
    {
        // Miscellaneous
        public static int Coin = -1;
        public static int GoblinSoul = -1;
        public static int BloodRune = -1;
        public static int PirateBooty = -1;
        public static int MonsterSoul = -1;
        public static int HalloweenTreat = -1;
        public static int ChristmasCheer = -1;
        public static int MartianCredit = -1;
        public static int DustIDSlashFX;

        public static int BoneAmmo = 10000;

        // Hotkeys
        public static ModKeybind AccessoryAbilityKey;
        public static ModKeybind ArmorAbilityKey;
        public static ModKeybind Rift;
        public static ModKeybind RiftReturn;

        // Textures
        public static IDictionary<string, Texture2D> Textures = null;
        public static Dictionary<string, Texture2D> precachedTextures = new Dictionary<string, Texture2D>();
        public static string BLANK_TEX = "AAMod/BlankTex";

        // UI
        internal UserInterface TerratoolInterface;
        internal TerratoolTUI TerratoolTState;
        internal TerratoolCUI TerratoolCState;
        internal TerratoolAUI TerratoolAState;
        internal TerratoolYUI TerratoolYState;
        internal TerratoolZUI TerratoolZState;
        internal TerratoolSUI TerratoolSState;
        internal TerratoolKipUI TerratoolKipState;
        internal TerratoolLizUI TerratoolLizState;
        internal TerratoolGroxUI TerratoolGroxState;
        internal TerratoolEXUI TerratoolEXState;

        public static SpriteFont fontMouseText;


        internal static AAMod instance;
        public static AAMod self = null;
        internal ILog Logging = LogManager.GetLogger("AAMod");

        public static bool isFullyReady;

        public AAMod()
        {
            ContentAutoloadingEnabled = true;
            GoreAutoloadingEnabled = true;
            MusicAutoloadingEnabled = true;
            BackgroundAutoloadingEnabled = true;

            instance = this;
        }

        public Texture2D GetTexture(string path) => ModContent.Request<Texture2D>("AAModClassic/" + path).Value;

        public static void SetupBannerItemTextures()
        {
            if (Main.netMode == NetmodeID.Server || Main.dedServ) return; //don't do any texture stuff on a server lol
            try
            {
                int fx = 16;
                Texture2D tex = TextureAssets.Tile[instance.Find<ModTile>("Banners").Type].Value;

                while (Tiles.Banners.Banners.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners.GetBannerName(fx);

                    if (name.Equals("DUMMY"))
                    {
                        fx += 16;
                        continue;
                    }

                    var data = new Color[16 * 16 * 3];
                    BaseDrawing.GetCroppedTex(tex, new Rectangle(fx, 0, 16, 16 * 3)).GetData<Color>(data);
                    TextureAssets.Item[instance.Find<ModItem>(name + "Banner").Type].Value.SetData<Color>(data);
                    fx += 16;
                }
            }
            catch (Exception e)
            {
                instance.Logger.InfoFormat(e.Message);
                instance.Logger.InfoFormat(e.StackTrace);
            }
        }

        public static FieldInfo _bannerField = null;
        public static IDictionary<int, int> BannerToItemDict
        {
            get
            {
                if (_bannerField == null)
                {
                    _bannerField = typeof(NPCLoader).GetField("bannerToItem", BindingFlags.NonPublic | BindingFlags.Static);
                }
                return (IDictionary<int, int>)_bannerField.GetValue(null);
            }
            set
            {
                if (_bannerField != null)
                {
                    _bannerField.SetValue(null, value);
                }
            }
        }

        public static void SetupBannerNPCs()
        {
            Mod mod = instance;
            try
            {
                IDictionary<int, int> bannerToItem = BannerToItemDict;
                int fx = 16;

                while (Tiles.Banners.Banners.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners.GetBannerName(fx, false);

                    if (name.Equals("DUMMY"))
                    {
                        fx += 16;
                        continue;
                    }

                    if (name.Contains("Wyrmling"))
                    {
                        for (int m = 0; m < 4; m++)
                        {
                            ModNPC npc = GetNPC(m == 0 ? "Wyrmling" : (m == 1 ? "WyrmlingBody" : (m == 2 ? "WyrmlingTail1" : "WyrmlingTail2")));
                            if (npc != null)
                            {
                                npc.Banner = mod.Find<ModNPC>("Wyrmling").Type;
                                npc.BannerItem = mod.Find<ModItem>("WyrmlingBanner").Type;
                                bannerToItem[npc.Banner] = npc.BannerItem;
                            }
                        }
                    }
                    else if (name.Contains("Wyrm"))
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            ModNPC npc = GetNPC(m == 0 ? "Wyrm" : (m == 1 ? "WyrmBody1" : (m == 2 ? "WyrmBody2" : (m == 3 ? "WyrmBody3" : "WyrmBody4"))));
                            if (npc != null)
                            {
                                npc.Banner = mod.Find<ModNPC>("Wyrm").Type;
                                npc.BannerItem = mod.Find<ModItem>("WyrmBanner").Type;
                                bannerToItem[npc.Banner] = npc.BannerItem;
                            }
                        }
                    }
                    else if (name.Contains("Snake"))
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            ModNPC npc = GetNPC(m == 0 ? "SnakeHead" : (m == 1 ? "SnakeBody" : "SnakeTail"));
                            if (npc != null)
                            {
                                npc.Banner = mod.Find<ModNPC>("SnakeHead").Type;
                                npc.BannerItem = mod.Find<ModItem>("SnakeBanner").Type;
                                bannerToItem[npc.Banner] = npc.BannerItem;
                            }
                        }
                    }
                    else
                    {
                        ModNPC npc = GetNPC(name);
                        if (npc != null)
                        {
                            npc.Banner = mod.Find<ModNPC>(name).Type;
                            npc.BannerItem = mod.Find<ModItem>(name + "Banner").Type;
                            bannerToItem[npc.Banner] = npc.BannerItem;
                        }
                    }
                    fx += 16;
                }
                BannerToItemDict = bannerToItem;
            }
            catch (Exception e)
            {
                instance.Logger.InfoFormat(e.Message);
                instance.Logger.InfoFormat(e.StackTrace);
            }
        }

        private static ModNPC GetNPC(string npcName)
        {
            if (ModContent.TryFind<ModNPC>(npcName, out var modnpc))
                return modnpc;
            return null;
        }

        public override void PostSetupContent()
        {
            WeakReferences.PerformModSupport();

            Array.Resize(ref AASets.Goblins, NPCLoader.NPCCount);

            isFullyReady = true;
        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public static Texture2D BlockBarier = null;

        public override void Load()
        {
            Logger.InfoFormat("{0} AA log", Name);

            instance = this;
            Coin = CustomCurrencyManager.RegisterCurrency(new Items.Currency.ACoin(ModContent.ItemType<Items.Currency.AncientCoin>()));
            GoblinSoul = CustomCurrencyManager.RegisterCurrency(new Items.Currency.GSouls(ModContent.ItemType<Items.Currency.GoblinSoul>()));
            BloodRune = CustomCurrencyManager.RegisterCurrency(new Items.Currency.BRune(ModContent.ItemType<Items.Currency.BloodRune>()));
            PirateBooty = CustomCurrencyManager.RegisterCurrency(new Items.Currency.PBooty(ModContent.ItemType<Items.Currency.PirateBooty>()));
            MonsterSoul = CustomCurrencyManager.RegisterCurrency(new Items.Currency.MSouls(ModContent.ItemType<Items.Currency.MonsterSoul>()));
            HalloweenTreat = CustomCurrencyManager.RegisterCurrency(new Items.Currency.HTreat(ModContent.ItemType<Items.Currency.HalloweenTreat>()));
            ChristmasCheer = CustomCurrencyManager.RegisterCurrency(new Items.Currency.CCheer(ModContent.ItemType<Items.Currency.ChristmasCheer>()));
            MartianCredit = CustomCurrencyManager.RegisterCurrency(new Items.Currency.MCredit(ModContent.ItemType<Items.Currency.MartianCredit>()));

            BoneAmmo = ItemID.Bone;
            if (Main.rand == null)
                Main.rand = new UnifiedRandom();

            GameShaders.Armor.BindShader(Find<ModItem>("BlazingDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye)).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);
            GameShaders.Armor.BindShader(Find<ModItem>("AbyssalDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye).UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(Find<ModItem>("DoomsdayDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.VortexDye)).UseImage("Images/Misc/noise").UseColor(0f, 0f, 0f).UseSecondaryColor(1f, 0f, 0f).UseSaturation(1f);
            GameShaders.Armor.BindShader(Find<ModItem>("DiscordianDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye).UseColor(0.66f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f));
            GameShaders.Armor.BindShader(Find<ModItem>("DiscordianInfernoDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye)).UseColor(0.88f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f);
            GameShaders.Armor.BindShader(Find<ModItem>("AbyssalWrathDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye).UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(Find<ModItem>("BlazingFuryDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye)).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);

            Rift = KeybindLoader.RegisterKeybind(this, Lang.Hotkey("Rifthotkey"), "C");
            RiftReturn = KeybindLoader.RegisterKeybind(this, Lang.Hotkey("RiftReturnhotkey"), "X");

            AccessoryAbilityKey = KeybindLoader.RegisterKeybind(this, Lang.Hotkey("AccessoryAbilityKey"), "U");
            ArmorAbilityKey = KeybindLoader.RegisterKeybind(this, Lang.Hotkey("ArmorAbilityKey"), "Y"); 
            
            Terraria.On_Wiring.ActuateForced += Wiring_ActuateForced;
            Terraria.On_Wiring.Actuate += Actuate;

            if (!Main.dedServ)
            {
                Config.Load();
                LoadClient();
            }
        }

        public void LoadClient()
        {
            Asset<Effect> screenRef = ModContent.Request<Effect>("Effects/Shockwave");
            Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave"].Load();

            BackupVanillaBG(-1);
            BackupVanillaBG(-2);
            BackupVanillaBG(-3);

            BackupVanillaBG(0);
            BackupVanillaBG(171);
            BackupVanillaBG(172);
            BackupVanillaBG(173);
            BackupVanillaBG(24);
            BackupVanillaBG(25);
            BackupVanillaBG(56);
            BackupVanillaBG(57);
            BackupVanillaBG(58);

            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/VoidBH").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/Moon").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/Sun").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/FogTex").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AkumaSun").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/YamataMoon").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/YamataBeam").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AkumaAMeteor").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AkumaMeteor").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/SkyTex").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/ShenMeteor").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AthenaBolt").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AthenaFlash").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/Zero/ZeroShield").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Projectiles/RadiumStar").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Projectiles/Stars").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/Toad/ToadBubble").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/Zero/Protocol/ProtoStar").Value);
            PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Textures/SagittariusShield").Value);

            if (MusicLoader.GetMusicSlot("Sounds/Music/Monarch") != 0) //ensure music was loaded!
            {
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Monarch"), Find<ModItem>("MonarchBox").Type, Find<ModTile>("MonarchBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Fungus"), Find<ModItem>("FungusBox").Type, Find<ModTile>("FungusBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/GripsTheme"), Find<ModItem>("GripsBox").Type, Find<ModTile>("GripsBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/HydraTheme"), Find<ModItem>("HydraBox").Type, Find<ModTile>("HydraBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/BroodTheme"), Find<ModItem>("BroodBox").Type, Find<ModTile>("BroodBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Shroom"), Find<ModItem>("MushBox").Type, Find<ModTile>("MushBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/InfernoSurface"), Find<ModItem>("InfernoBox").Type, Find<ModTile>("InfernoBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/IN"), Find<ModItem>("InfernoNightBox").Type, Find<ModTile>("InfernoNightBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/MireSurface"), Find<ModItem>("MireBox").Type, Find<ModTile>("MireBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/DM"), Find<ModItem>("MireDayBox").Type, Find<ModTile>("MireDayBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/InfernoUnderground"), Find<ModItem>("InfernoUBox").Type, Find<ModTile>("InfernoUBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/MireUnderground"), Find<ModItem>("MireUBox").Type, Find<ModTile>("MireUBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Void"), Find<ModItem>("VoidBox").Type, Find<ModTile>("VoidBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Djinn"), Find<ModItem>("DjinnBox").Type, Find<ModTile>("DjinnBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/TODE"), Find<ModItem>("ToadBox").Type, Find<ModTile>("ToadBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Boss6"), Find<ModItem>("SerpentBox").Type, Find<ModTile>("SerpentBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Sag"), Find<ModItem>("SagBox").Type, Find<ModTile>("SagBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Anubis"), Find<ModItem>("AnubisBox").Type, Find<ModTile>("AnubisBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Acropolis"), Find<ModItem>("AcropolisBox").Type, Find<ModTile>("AcropolisBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Hoard"), Find<ModItem>("HoardBox").Type, Find<ModTile>("HoardBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Greed"), Find<ModItem>("GreedBox").Type, Find<ModTile>("GreedBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Athena"), Find<ModItem>("AthenaBox").Type, Find<ModTile>("AthenaBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/RajahTheme"), Find<ModItem>("RajahBox").Type, Find<ModTile>("RajahBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/GreedA"), Find<ModItem>("GreedABox").Type, Find<ModTile>("GreedABox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/AthenaA"), Find<ModItem>("AthenaABox").Type, Find<ModTile>("AthenaABox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/AnubisA"), Find<ModItem>("AnubisFBox").Type, Find<ModTile>("AnubisFBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Equinox"), Find<ModItem>("Equibox").Type, Find<ModTile>("Equibox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Stars"), Find<ModItem>("StarBox").Type, Find<ModTile>("StarBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/AH"), Find<ModItem>("SistersBox").Type, Find<ModTile>("SistersBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/VoidButNowItsSpooky"), Find<ModItem>("FateBox").Type, Find<ModTile>("FateBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Shrines"), Find<ModItem>("LakeBox").Type, Find<ModTile>("LakeBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/AkumaShrine"), Find<ModItem>("PagodaBox").Type, Find<ModTile>("PagodaBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Zero"), Find<ModItem>("ZeroBox").Type, Find<ModTile>("ZeroBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Zero2"), Find<ModItem>("Zero2Box").Type, Find<ModTile>("Zero2Box").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Akuma"), Find<ModItem>("AkumaBox").Type, Find<ModTile>("AkumaBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Akuma2"), Find<ModItem>("AkumaABox").Type, Find<ModTile>("AkumaABox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Yamata"), Find<ModItem>("YamataBox").Type, Find<ModTile>("YamataBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Yamata2"), Find<ModItem>("YamataABox").Type, Find<ModTile>("YamataABox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Terrarium"), Find<ModItem>("TerrariumBox").Type, Find<ModTile>("TerrariumBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/SleepingDragon"), Find<ModItem>("SDBox").Type, Find<ModTile>("SDBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/SleepingGiant"), Find<ModItem>("SGBox").Type, Find<ModTile>("SGBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/Shen"), Find<ModItem>("ShenBox").Type, Find<ModTile>("ShenBox").Type);
                MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/ShenA"), Find<ModItem>("ShenABox").Type, Find<ModTile>("ShenABox").Type);
                //MusicLoader.AddMusicBox(this, MusicLoader.GetMusicSlot("Sounds/Music/SupremeRajah"), ItemType("SRajahBox"), TileType("SRajahBox"));
            }

            Filters.Scene["AAMod:ShenSky"] = new Filter(new ShenSkyData("FilterMiniTower").UseColor(.5f, 0f, .5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:ShenSky"] = new ShenSky();

            Filters.Scene["AAMod:ShenASky"] = new Filter(new ShenASkyData("FilterMiniTower").UseColor(.7f, 0f, .7f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:ShenASky"] = new ShenASky();

            Filters.Scene["AAMod:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:MireSky"] = new MireSky();

            VoidSky vSky = new VoidSky();
            Filters.Scene["AAMod:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:VoidSky"] = vSky;

            AthenaSky aSky = new AthenaSky();
            Filters.Scene["AAMod:AthenaSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:AthenaSky"] = aSky;

            InfernoSky iSky = new InfernoSky();
            Filters.Scene["AAMod:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAMod:InfernoSky"] = iSky;

            AkumaSky akSky = new AkumaSky();
            Filters.Scene["AAMod:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:AkumaSky"] = akSky;

            Filters.Scene["AAMod:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:YamataSky"] = new YamataSky();

            AnubisSky anSky = new AnubisSky();
            Filters.Scene["AAMod:AnubisSky"] = new Filter(new AnubisSkyData("FilterMiniTower").UseColor(.2f, .5f, .2f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAMod:AnubisSky"] = anSky;

            ReplaceItemTexture(3460, "Resprites/Luminite");
            ReplaceItemTexture(512, "Resprites/SoulOfNight");

            BlockBarier = ModContent.Request<Texture2D>("AAModClassic/Textures/BlockShield").Value;
            sunTextureBackup = TextureAssets.Sun;
            sun3TextureBackup = TextureAssets.Sun3;

            EquipLoader.AddEquipTexture(this, "AAMod/Items/Dev/Invoker/InvokedCaligula_Head", EquipType.Head, name: "InvokedCaligulaHead", equipTexture: new InvokedCaligulaHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Dev/Invoker/InvokedCaligula_Body", EquipType.Body, name: "InvokedCaligulaBody", equipTexture: new InvokedCaligulaBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Dev/Invoker/InvokedCaligula_Legs", EquipType.Legs, name: "InvokedCaligulaLegs", equipTexture: new InvokedCaligulaLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/Ohno/ono_Head", EquipType.Head, name: "onoHead", equipTexture: new Items.Vanity.Ohno.onoHead());
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/Ohno/ono_Body", EquipType.Body, name: "onoBody", equipTexture: new Items.Vanity.Ohno.onoBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/Ohno/ono_Legs", EquipType.Legs, name: "onoLegs", equipTexture: new Items.Vanity.Ohno.onoLegs());

            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/Cerberus/InvokerHood_Head", EquipType.Head, name: "InvokerHead", equipTexture: new InvokerHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/Cerberus/InvokerRobe_Body", EquipType.Body, name: "InvokerBody", equipTexture: new InvokerBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/Cerberus/InvokerPants_Legs", EquipType.Legs, name: "InvokerLegs", equipTexture: new InvokerLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/CC/CCRobe_Legs", EquipType.Legs, name: "CCRobe_Legs");
            EquipLoader.AddEquipTexture(this, "AAMod/Items/Vanity/CC/Shiny/ShinyCCRobe_Legs", EquipType.Legs, name: "ShinyCCRobe_Legs");
        }

        //DO NOT MAKE THESE STATIC! DOING SO WILL PREVENT WHAT IT FIXES FROM HAPPENING.
        private Asset<Texture2D> sunTextureBackup = null;
        private Asset<Texture2D> sun3TextureBackup = null;


        public Dictionary<int, Asset<Texture2D>> vanillaTextureBackups = new Dictionary<int, Asset<Texture2D>>();
        public Dictionary<int, Asset<Texture2D>> vanillaBGBackups = new Dictionary<int, Asset<Texture2D>>();

        public void ReplaceItemTexture(int id, string texturePath)
        {
            vanillaTextureBackups.Add(id, TextureAssets.Item[id]);
            TextureAssets.Item[id] = ModContent.Request<Texture2D>(texturePath);
        }

        public void ResetItemTexture(int id)
        {
            if (vanillaTextureBackups.ContainsKey(id))
            {
                TextureAssets.Item[id] = vanillaTextureBackups[id];
            }
        }

        public void BackupVanillaBG(int id)
        {
            if(id > 0)
            {
                vanillaBGBackups.Add(id, TextureAssets.Background[id]);
            }
            else if(id == -1)
            {
                vanillaBGBackups.Add(-1, TextureAssets.Logo);
            }
            else if(id == -2)
            {
                vanillaBGBackups.Add(-2, TextureAssets.Logo2);
            }
            else if(id == -3)
            {
                vanillaBGBackups.Add(-3, TextureAssets.Sun);
            }
           
        }

        public void ResetBGTexture(int id)
        {
            if (vanillaBGBackups.ContainsKey(id))
            {
                if(id > 0)
                {
                    TextureAssets.Background[id] = vanillaBGBackups[id];
                }
                else if(id == -1)
                {
                    TextureAssets.Logo = vanillaBGBackups[-1];
                }
                else if(id == -2)
                {
                    TextureAssets.Logo2 = vanillaBGBackups[-2];
                }
                else if(id == -3)
                {
                    TextureAssets.Sun = vanillaBGBackups[-3];
                }
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                CleanAAmenu();
                UnloadClient();
            }
            
            CleanupStaticArrays();

            instance = null;
            Rift = null;
            RiftReturn = null;
            AccessoryAbilityKey = null;
            ArmorAbilityKey = null;

            isFullyReady = false;
        }

        public void UnloadClient()
        {
            ResetItemTexture(3460);
            ResetItemTexture(512);

            if (sunTextureBackup != null)
                TextureAssets.Sun = sunTextureBackup;
            if (sun3TextureBackup != null)
                TextureAssets.Sun3 = sun3TextureBackup;
        }

        public void CleanAAmenu()
        {
            ResetBGTexture(-1);
            ResetBGTexture(-2);
            ResetBGTexture(-3);
            //Main.logoTexture = ModContent.Request<Texture2D>("AAModClassic/Logo");
            //Main.logo2Texture = ModContent.Request<Texture2D>("AAModClassic/Logo2");
            ResetBGTexture(0);
            ResetBGTexture(171);
            ResetBGTexture(172);
            ResetBGTexture(173);
            ResetBGTexture(24);
            ResetBGTexture(25);
            ResetBGTexture(56);
            ResetBGTexture(57);
            ResetBGTexture(58);
        }

        public void CleanupStaticArrays()
        {
            if (Main.netMode != NetmodeID.Server) //handle clearing all static texture arrays
            {
                precachedTextures.Clear();

                BlockBarier = null;
            }
        }

        

        public static Texture2D GetGlowmask(string Name)
        {
            return ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + Name + "_Glow").Value;
        }

        private void Wiring_ActuateForced(Terraria.On_Wiring.orig_ActuateForced orig, int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileType == ModContent.TileType<Tiles.AcropolisBlock2>() || tile.TileType == ModContent.TileType<Tiles.AcropolisBlock>() ||
                tile.TileType == ModContent.TileType<Tiles.GreedStone>() || tile.TileType == ModContent.TileType<Tiles.GreedBrick>())
            {
                return;
            }
            orig(i, j);
        }

        private static bool Actuate(Terraria.On_Wiring.orig_Actuate orig, int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileType == ModContent.TileType<Tiles.AcropolisBlock2>() || tile.TileType == ModContent.TileType<Tiles.AcropolisBlock>() ||
                tile.TileType == ModContent.TileType<Tiles.GreedStone>() || tile.TileType == ModContent.TileType<Tiles.GreedBrick>())
            {
                return false;
            }
            return orig(i, j);
        }

        public static void Chat(string s, Color color, bool sync = true)
        {
            Chat(s, color.R, color.G, color.B, sync);
        }

        /*
         * Sends the given string to chat, with the given color values.
         */
        public static void Chat(string s, byte colorR = 255, byte colorG = 255, byte colorB = 255, bool sync = true)
        {
            if (!AAConfigClient.Instance.NoBossDialogue)
            {
                if (Main.netMode == NetmodeID.SinglePlayer) { Main.NewText(s, colorR, colorG, colorB); }
                else
                if (Main.netMode == NetmodeID.MultiplayerClient) { Main.NewText(s, colorR, colorG, colorB); }
                else //if(sync){ NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), Main.myPlayer); } }else
                if (sync && Main.netMode == NetmodeID.Server) { ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(s), new Color(colorR, colorG, colorB), -1); }
            }
        }

        public override object Call(params object[] args)
        {
            if (args.Length <= 0 || !(args[0] is string))
                return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD NAME! First param MUST be a method name!");

            string methodName = (string)args[0];

            if (methodName.Equals("Downed")) //returns a Func which will return a downed value based on player and name.
            {
                Func<string, bool> downed = (name) =>
                {
                    name = name.ToLower();
                    switch (name)
                    {
                        default: return false;
                        case "mushroommonarch": return AAWorld.downedMonarch;
                        case "broodmother": return AAWorld.downedBrood;
                        case "hydra": return AAWorld.downedHydra;
                        case "grips":
                        case "gripsofchaos": return AAWorld.downedGrips;
                        case "tode": return AAWorld.downedToad;
                        case "daybringer": return AAWorld.downedDB;
                        case "nightcrawler": return AAWorld.downedNC;
                        case "equinox": return AAWorld.downedEquinox;
                        case "ancient":
                        case "ancientany": return AAWorld.downedAncient;
                        case "sancient":
                        case "sancientany": return AAWorld.downedSAncient;
                        case "gripsS":
                        case "akuma": return AAWorld.downedAkuma;
                        case "yamata": return AAWorld.downedYamata;
                        case "zero": return AAWorld.downedZero;
                        case "shen":
                        case "shendoragon": return AAWorld.downedShen;
                    }
                };
                return downed;
            }
            else if (methodName.Equals("InZone")) //returns a Func which will return a zone value based on player and name.
            {
                Func<Player, string, bool> inZone = (p, name) =>
                {
                    name = name.ToLower();
                    AAPlayer aap = p.GetModPlayer<AAPlayer>();
                    switch (name)
                    {
                        default: return false;
                        case "mire": return aap.ZoneMire;
                        case "lake": return aap.ZoneRisingMoonLake;
                        case "inferno": return aap.ZoneInferno;
                        case "pagoda": return aap.ZoneRisingSunPagoda;
                        case "ship": return aap.ZoneShip;
                        case "storm": return aap.ZoneStorm;
                        case "void": return aap.ZoneVoid;
                        case "mush": return aap.ZoneMush;
                        case "terrarium": return aap.Terrarium;
                    }
                };
                return inZone;
            }
            return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD FOUND: " + methodName);
        }

        public override void HandlePacket(BinaryReader bb, int whoAmI)
        {
            AANet.HandlePacket(bb, whoAmI);

            MsgType msg = (MsgType)bb.ReadByte();
            if (msg == MsgType.ProjectileHostility) //projectile hostility and ownership
            {
                int owner = bb.ReadInt32();
                int projID = bb.ReadInt32();
                bool friendly = bb.ReadBoolean();
                bool hostile = bb.ReadBoolean();
                if (Main.projectile[projID] != null)
                {
                    Main.projectile[projID].owner = owner;
                    Main.projectile[projID].friendly = friendly;
                    Main.projectile[projID].hostile = hostile;
                }
                if (Main.netMode == NetmodeID.Server) MNet.SendBaseNetMessage(0, owner, projID, friendly, hostile);
            }
            else
            if (msg == MsgType.SyncAI) //sync AI array
            {
                int classID = (int)bb.ReadByte();
                int id = (int)bb.ReadInt16();
                int aitype = (int)bb.ReadByte();
                int arrayLength = (int)bb.ReadByte();
                float[] newAI = new float[arrayLength];
                for (int m = 0; m < arrayLength; m++)
                {
                    newAI[m] = bb.ReadSingle();
                }
                if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].ModNPC != null && Main.npc[id].ModNPC is ParentNPC)
                {
                    ((ParentNPC)Main.npc[id].ModNPC).SetAI(newAI, aitype);
                }
                else
                if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].ModProjectile != null && Main.projectile[id].ModProjectile is ParentProjectile)
                {
                    ((ParentProjectile)Main.projectile[id].ModProjectile).SetAI(newAI, aitype);
                }
                if (Main.netMode == NetmodeID.Server) BaseNet.SyncAI(classID, id, newAI, aitype);
            }
        }
    }

    enum MsgType : byte
    {
        ProjectileHostility,
        SyncAI
    }
}

public class AAModSystem : ModSystem
{
    //AA Menu
    public bool AAMenuset = false;
    public bool AAMenuReset = true;

    public static bool AAloadedOnly = true;

    public override void Load()
    {
        ModContent.GetInstance<AAMod>().TerratoolInterface = new UserInterface();
        ModContent.GetInstance<AAMod>().TerratoolTState = new TerratoolTUI();
        ModContent.GetInstance<AAMod>().TerratoolTState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolCState = new TerratoolCUI();
        ModContent.GetInstance<AAMod>().TerratoolCState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolAState = new TerratoolAUI();
        ModContent.GetInstance<AAMod>().TerratoolAState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolYState = new TerratoolYUI();
        ModContent.GetInstance<AAMod>().TerratoolYState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolZState = new TerratoolZUI();
        ModContent.GetInstance<AAMod>().TerratoolZState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolSState = new TerratoolSUI();
        ModContent.GetInstance<AAMod>().TerratoolSState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolKipState = new TerratoolKipUI();
        ModContent.GetInstance<AAMod>().TerratoolKipState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolLizState = new TerratoolLizUI();
        ModContent.GetInstance<AAMod>().TerratoolLizState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolGroxState = new TerratoolGroxUI();
        ModContent.GetInstance<AAMod>().TerratoolGroxState.Activate();
        ModContent.GetInstance<AAMod>().TerratoolEXState = new TerratoolEXUI();
        ModContent.GetInstance<AAMod>().TerratoolEXState.Activate();
    }

    public override void Unload()
    {
        AAMenuset = false;
    }

    public override void AddRecipeGroups()
    {
        AARecipes.AddRecipeGroups();
    }

    public override void AddRecipes()
    {
        AARecipes.AddRecipes();
    }

    public override void PostAddRecipes()
    {
        LuckyCheckProgress();
        foreach (Mod mo in ModLoader.Mods)
        {
            if (mo.Name != "ModLoader" && mo.Name != "AAMod" && mo.Name != "AAMod")
            {
                AAloadedOnly = false;
            }
        }
        Config.SaveConfig();
    }

    private void LuckyCheckProgress()
    {
        Config.LuckyOre.Clear();
        Config.LuckyPotion.Clear();
        Config.ListRareNpc.Clear();
        Item item = new Item();
        for (int i = -48; i < ItemLoader.ItemCount; i++)
        {
            item.netDefaults(i);
            if (item.createTile > TileID.Dirt && Main.tileOreFinderPriority[item.createTile] > 0 && !Main.tileContainer[item.createTile] && item.createTile != TileID.FakeContainers && item.createTile != TileID.FakeContainers2)
            {
                Config.LuckyOre.Add(item.type, Main.tileOreFinderPriority[item.createTile]);
            }
            if (item.buffType > 0 && item.buffType != BuffID.WellFed && item.buffTime > 0 && item.type > ItemID.Celeb2)
            {
                Config.LuckyPotion.Add(item.type, item.value);
            }
        }
        NPC npc = new NPC();
        for (int i = -65; i < NPCLoader.NPCCount; i++)
        {
            if (i != 0)
            {
                npc.SetDefaults(i);
            }
            if (npc.rarity >= 1)
            {
                Config.ListRareNpc.Add(i);
            }
        }
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)/* tModPorter Note: Removed. Use ModSystem.ModifyInterfaceLayers */
    {
        int wireSelectionLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Wire Selection"));
        if (wireSelectionLayerIndex != -1)
        {
            layers.Insert(wireSelectionLayerIndex, new LegacyGameInterfaceLayer(
            "AAMod: Radial UIs",
            delegate
            {
                if (AAMod.instance.TerratoolInterface?.CurrentState is ToggableUI && lastUpdateUIGameTime != null)
                {
                    AAMod.instance.TerratoolInterface.Draw(Main.spriteBatch, lastUpdateUIGameTime);
                }

                return true;
            },
            InterfaceScaleType.UI));
        }
    }

    private static GameTime lastUpdateUIGameTime;

    public override void UpdateUI(GameTime gameTime)/* tModPorter Note: Removed. Use ModSystem.UpdateUI */
    {
        lastUpdateUIGameTime = gameTime;

        if (AAMod.instance.TerratoolInterface?.CurrentState != null)
        {
            AAMod.instance.TerratoolInterface.Update(gameTime);
        }
    }

    public override void PostUpdateInput()/* tModPorter Note: Removed. Use ModSystem.PostUpdateInput */
    {
        if (!AAMod.isFullyReady)
        {
            return;
        }
        if (Main.gameMenu && Main.menuMode >= 0)
        {
            if (Main.menuMode == 0)
            {
                AAMenuset = true;
            }
            if (Main.menuMode >= 10002 && Main.menuMode <= 10006)
            {
                AAMenuset = false;
            }
            if (AAConfigClient.Instance != null && AAConfigClient.Instance.AAStyleMainPage && (Main.bgStyle == 1 || Main.bgStyle == 0) && AAMenuset)
            {
                AAMenuReset = true;
                WorldGen.setBG(0, 6);
                switch (Main.moonPhase % 3)
                {
                    case 0:
                        Main.numClouds = 10;
                        if (Main.LogoB <= 255)
                        {
                            TextureAssets.Logo = ModContent.Request<Texture2D>("Terraria/Logo");
                        }
                        if (Main.LogoB < 10 || (!Main.dayTime && Main.LogoA <= 255))
                        {
                            TextureAssets.Logo2 = ModContent.Request<Texture2D>("Terraria/Logo2");
                        }
                        TextureAssets.Sun = ModContent.Request<Texture2D>("Terraria/Sun");
                        if (SkyManager.Instance["AAMod:MireSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:MireSky", new object[0]);
                        if (SkyManager.Instance["AAMod:VoidSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:VoidSky", new object[0]);

                        if (Main.dayTime && (Main.bgAlphaFarBackLayer[0] < 0.10f || Main.bgAlphaFarBackLayer[0] == 1f))
                        {
                            Main.treeBGSet1[0] = 173;
                            TextureAssets.Background[0] = ModContent.Request<Texture2D>("Terraria/Background_" + 0);
                            TextureAssets.Background[171] = ModContent.Request<Texture2D>("Terraria/Background_" + 171);
                            TextureAssets.Background[172] = ModContent.Request<Texture2D>("Terraria/Background_" + 172);
                            TextureAssets.Background[173] = ModContent.Request<Texture2D>("Terraria/Background_" + 173);
                            TextureAssets.Background[24] = ModContent.Request<Texture2D>("Terraria/Background_" + 24);
                            TextureAssets.Background[25] = ModContent.Request<Texture2D>("Terraria/Background_" + 25);
                            TextureAssets.Background[56] = ModContent.Request<Texture2D>("Terraria/Background_" + 56);
                            TextureAssets.Background[57] = ModContent.Request<Texture2D>("Terraria/Background_" + 57);
                            TextureAssets.Background[58] = ModContent.Request<Texture2D>("Terraria/Background_" + 58);
                        }
                        break;
                    case 1:
                        Main.numClouds = 0;
                        if (Main.dayTime)
                        {
                            TextureAssets.Sun = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/Sun");
                        }
                        else
                        {
                            if (SkyManager.Instance["AAMod:MireSky"] != null) SkyManager.Instance.Activate("AAMod:MireSky", default, new object[0]);
                        }
                        if (Main.LogoB <= 255)
                        {
                            TextureAssets.Logo = ModContent.Request<Texture2D>("AAModClassic/UI/LogoInferno");
                        }
                        if (Main.LogoB < 10 || (!Main.dayTime && Main.LogoA <= 255))
                        {
                            TextureAssets.Logo2 = ModContent.Request<Texture2D>("AAModClassic/UI/LogoMire");
                        }
                        if (Main.dayTime && (Main.bgAlphaFarBackLayer[0] < 0.10f || Main.bgAlphaFarBackLayer[0] == 1f))
                        {
                            TextureAssets.Background[0] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/InfernoSky");
                            TextureAssets.Background[171] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/InfernoBG");
                            TextureAssets.Background[172] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/InfernoBG");
                            TextureAssets.Background[173] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/InfernoBG");
                            TextureAssets.Background[24] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/MireBG");
                            TextureAssets.Background[25] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/MireFG2");
                            TextureAssets.Background[56] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/MireFG1");
                            TextureAssets.Background[57] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/MireFG1");
                            TextureAssets.Background[58] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/MireFG1");
                        }
                        if (!Main.dayTime && (Main.bgAlphaFarBackLayer[2] < 0.10f || Main.bgAlphaFarBackLayer[2] == 1f))
                        {
                            TextureAssets.Background[0] = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/YamataStars");
                        }
                        break;
                    case 2:
                        Main.numClouds = 0;
                        if (Main.LogoB <= 255)
                        {
                            TextureAssets.Logo = ModContent.Request<Texture2D>("AAModClassic/UI/LogoVoid");
                        }
                        if (Main.LogoB < 10 || (!Main.dayTime && Main.LogoA <= 255))
                        {
                            TextureAssets.Logo2 = ModContent.Request<Texture2D>("UI/LogoVoid");
                        }
                        if (SkyManager.Instance["AAMod:VoidSky"] != null) SkyManager.Instance.Activate("AAMod:VoidSky", default, new object[0]);
                        if (Main.dayTime && (Main.bgAlphaFarBackLayer[0] < 0.10f || Main.bgAlphaFarBackLayer[0] == 1f))
                        {
                            TextureAssets.Background[0] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[171] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[172] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[173] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[24] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[25] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[56] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[57] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                            TextureAssets.Background[58] = ModContent.Request<Texture2D>("AAModClassic/BlankTex");
                        }
                        break;
                    default:
                        goto case 0;
                }
            }
            else if (AAMenuReset)
            {
                if (SkyManager.Instance["AAMod:MireSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:MireSky", new object[0]);
                if (SkyManager.Instance["AAMod:VoidSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:VoidSky", new object[0]);
                TextureAssets.Sun = ModContent.Request<Texture2D>("Terraria/Sun");
                TextureAssets.Logo = ModContent.Request<Texture2D>("Terraria/Logo");
                TextureAssets.Logo2 = ModContent.Request<Texture2D>("Terraria/Logo2");
                AAMenuReset = false;
                TextureAssets.Background[0] = ModContent.Request<Texture2D>("Terraria/Background_" + 0);
                TextureAssets.Background[171] = ModContent.Request<Texture2D>("Terraria/Background_" + 171);
                TextureAssets.Background[172] = ModContent.Request<Texture2D>("Terraria/Background_" + 172);
                TextureAssets.Background[173] = ModContent.Request<Texture2D>("Terraria/Background_" + 173);
                TextureAssets.Background[24] = ModContent.Request<Texture2D>("Terraria/Background_" + 24);
                TextureAssets.Background[25] = ModContent.Request<Texture2D>("Terraria/Background_" + 25);
                TextureAssets.Background[56] = ModContent.Request<Texture2D>("Terraria/Background_" + 56);
                TextureAssets.Background[57] = ModContent.Request<Texture2D>("Terraria/Background_" + 57);
                TextureAssets.Background[58] = ModContent.Request<Texture2D>("Terraria/Background_" + 58);
            }
        }
        else if (AAMenuReset)
        {
            AAMenuReset = false;
            TextureAssets.Sun = ModContent.Request<Texture2D>("Terraria/Sun");
            if (SkyManager.Instance["AAMod:MireSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:MireSky", new object[0]);
            if (SkyManager.Instance["AAMod:VoidSky"].IsActive()) SkyManager.Instance.Deactivate("AAMod:VoidSky", new object[0]);
            TextureAssets.Background[0] = ModContent.Request<Texture2D>("Terraria/Background_" + 0);
            TextureAssets.Background[171] = ModContent.Request<Texture2D>("Terraria/Background_" + 171);
            TextureAssets.Background[172] = ModContent.Request<Texture2D>("Terraria/Background_" + 172);
            TextureAssets.Background[173] = ModContent.Request<Texture2D>("Terraria/Background_" + 173);
            TextureAssets.Background[24] = ModContent.Request<Texture2D>("Terraria/Background_" + 24);
            TextureAssets.Background[25] = ModContent.Request<Texture2D>("Terraria/Background_" + 25);
            TextureAssets.Background[56] = ModContent.Request<Texture2D>("Terraria/Background_" + 56);
            TextureAssets.Background[57] = ModContent.Request<Texture2D>("Terraria/Background_" + 57);
            TextureAssets.Background[58] = ModContent.Request<Texture2D>("Terraria/Background_" + 58);
        }
    }
}
public static class ModUtils
{
    public static Texture2D GetTexture(this Mod mod, string path) => ModContent.Request<Texture2D>("AAModClassic/" + path).Value;
}

