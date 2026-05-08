using AAModClassic._Content._Dev.Invoker;
using AAModClassic._Content._Dev.___PreHardmode.Items.Currency;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs._BossAthenaA.Skies;
using AAModClassic._Content.BloodMoon.___PreHardmode.Items.Currency;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs._BossShen.Skies;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Crimson.__Hardmode.Items.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert._PostMoonlord._BossAnubisA.Skies;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons;
using AAModClassic._Content.Evil.__Hardmode.Items.Weapons;
using AAModClassic._Content.FrostMoon.__Hardmode.Items.Currency;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Currency;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.Wyrmling;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground.Wyrm;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Skies;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.MartianMadness.__Hardmode.Items.Currency;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire.__Hardmode.Items.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened.Skies;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Parthenan.__Hardmode.Items.Weapons;
using AAModClassic._Content.PirateInvasion.__Hardmode.Items.Currency;
using AAModClassic._Content.PumpkinMoon.__Hardmode.Items.Currency;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons;
using AAModClassic._Content.Snow.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Snow.___PreHardmode.NPCs._Night._SnowSerpent;
using AAModClassic._Content.SolarEclipse.__Hardmode.Items.Currency;
using AAModClassic._Content.Stars._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Underground.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Underground.__Hardmode.Items.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.NPCs;
using AAModClassic.Base.Projectiles;
using AAModClassic.CrossMod;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Items.Boss.Zero;
using AAModClassic.Projectiles;
using AAModClassic.UI;
using AAModClassic.UI.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;

namespace AAModClassic
{
    public partial class AAMod : Mod
    {
        // Miscellaneous
        internal static int AncientCoin = -1;
        internal static int GoblinSoul = -1;
        internal static int BloodRune = -1;
        internal static int PirateBooty = -1;
        internal static int MonsterSoul = -1;
        internal static int HalloweenTreat = -1;
        internal static int ChristmasCheer = -1;
        internal static int MartianCredit = -1;
        internal static int DustIDSlashFX; //TODO: This thing never gets set but is used once inside Overhaul cross mod. No clue what to make of it...

        // Hotkeys
        internal static ModKeybind AccessoryAbilityKey;
        internal static ModKeybind ArmorAbilityKey;
        internal static ModKeybind Rift;
        internal static ModKeybind RiftReturn;

        // UI
        internal UserInterface TerratoolInterface;
        internal TerratoolTUI TerratoolTState;
        internal TerratoolCUI TerratoolCState;
        internal TerratoolAUI TerratoolAState;
        internal TerratoolYUI TerratoolYState;
        internal TerratoolZUI TerratoolZState;
        internal TerratoolSUI TerratoolSState;
        internal TerratoolKipUI TerratoolKipState;
        internal TerratoolGroxUI TerratoolGroxState;
        internal TerratoolEXUI TerratoolEXState;

        internal static AAMod instance;

        public AAMod()
        {
            ContentAutoloadingEnabled = true;
            GoreAutoloadingEnabled = true;
            MusicAutoloadingEnabled = true;
            BackgroundAutoloadingEnabled = true;

            instance = this;
        }

        public static void SetupBannerItemTextures()
        {
            if (Main.netMode == NetmodeID.Server || Main.dedServ) return; //don't do any texture stuff on a server lol
            try
            {
                int fx = 16;
                Texture2D tex = TextureAssets.Tile[instance.Find<ModTile>("Banners").Type].Value;

                while (Tiles.Banners.Banners_Tile.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners_Tile.GetBannerName(fx);

                    if (name.Equals("DUMMY"))
                    {
                        fx += 16;
                        continue;
                    }

                    var data = new Color[16 * 16 * 3];
                    GetCroppedTex(tex, new Rectangle(fx, 0, 16, 16 * 3)).GetData(data);
                    TextureAssets.Item[instance.Find<ModItem>(name + "Banner").Type].Value.SetData(data);
                    fx += 16;
                }
            }
            catch (Exception e)
            {
                instance.Logger.InfoFormat(e.Message);
                instance.Logger.InfoFormat(e.StackTrace);
            }
        }

        public static Texture2D GetCroppedTex(Texture2D texture, Rectangle rect)
        {
            Rectangle newBounds = texture.Bounds;
            newBounds.X += rect.X;
            newBounds.Y += rect.Y;
            newBounds.Width = rect.Width;
            newBounds.Height = rect.Height;
            Texture2D croppedTexture = new(Main.instance.GraphicsDevice, newBounds.Width, newBounds.Height);
            // Copy the data from the cropped region into a buffer, then into the new texture
            Color[] data = new Color[newBounds.Width * newBounds.Height];
            texture.GetData(0, newBounds, data, 0, newBounds.Width * newBounds.Height);
            croppedTexture.SetData(data);
            return croppedTexture;
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

                while (Tiles.Banners.Banners_Tile.GetBannerName(fx) != null)
                {
                    string name = Tiles.Banners.Banners_Tile.GetBannerName(fx, false);

                    if (name.Equals("DUMMY"))
                    {
                        fx += 16;
                        continue;
                    }

                    if (name.Contains("Wyrmling"))
                    {
                        for (int m = 0; m < 4; m++)
                        {
                            ModNPC npc = m switch
                            {
                                0 => ModContent.GetInstance<WyrmlingHead>(),
                                1 => ModContent.GetInstance<WyrmlingBody>(),
                                2 => ModContent.GetInstance<WyrmlingTail1>(),
                                _ => ModContent.GetInstance<WyrmlingTail2>(),
                            };

                            if (npc != null)
                            {
                                npc.Banner = ModContent.NPCType<WyrmlingHead>();
                                npc.BannerItem = ModContent.ItemType<Items.Banners.WyrmlingBanner>();
                                bannerToItem[npc.Banner] = npc.BannerItem;
                            }
                        }
                    }
                    else if (name.Contains("Wyrm"))
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            ModNPC npc = m switch
                            {
                                0 => ModContent.GetInstance<WyrmHead>(),
                                1 => ModContent.GetInstance<WyrmBody1>(),
                                2 => ModContent.GetInstance<WyrmBody2>(),
                                3 => ModContent.GetInstance<WyrmBody3>(),
                                _ => ModContent.GetInstance<WyrmBody4>(),
                            };

                            if (npc != null)
                            {
                                npc.Banner = ModContent.NPCType<WyrmHead>();
                                npc.BannerItem = ModContent.ItemType<Items.Banners.WyrmBanner>();
                                bannerToItem[npc.Banner] = npc.BannerItem;
                            }
                        }
                    }
                    else if (name.Contains("SnowSerpent"))
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            ModNPC npc = m switch
                            {
                                0 => ModContent.GetInstance<SnowSerpentHead>(),
                                1 => ModContent.GetInstance<SnowSerpentBody>(),
                                _ => ModContent.GetInstance<SnowSerpentTail>(),
                            };

                            if (npc != null)
                            {
                                npc.Banner = ModContent.NPCType<SnowSerpentHead>();
                                npc.BannerItem = ModContent.ItemType<Items.Banners.SnakeBanner>();
                                bannerToItem[npc.Banner] = npc.BannerItem;
                            }
                        }
                    }
                    else
                    {
                        ModNPC npc = mod.Find<ModNPC>(name);
                        if (npc != null)
                        {
                            npc.Banner = npc.Type;
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

        public override void PostSetupContent()
        {
            WeakReferences.PerformModSupport();

            SetupBannerNPCs();

            SetupBannerItemTextures();

            Array.Resize(ref AASets.Goblins, NPCLoader.NPCCount);

            foreach(ModItem modItem in this.GetContent<ModItem>())
            {
                if (ModContent.RequestIfExists<Texture2D>(modItem.Texture + "_Glow", out var texture))
                {
                    if (BaseAAItem.GlowmaskCache.TryAdd(modItem.Type, texture) == false)
                    {
                        Logger.Warn("some shit did NOT get loaded into the glowmask cache bcuz something was already there.");
                        Logger.Warn("item id: " + modItem.Type);
                        Logger.Warn("item name: " + modItem.DisplayName);
                        Logger.Warn("glowmask in that slot: " + texture.Name);
                    }
                    Logger.Warn("added item name: " + modItem.DisplayName);
                }
            }

            var field = typeof(FinalFractalHelper).GetField("_fractalProfiles", BindingFlags.Static | BindingFlags.NonPublic);
            if(field != null)
            {
                var profiles = (Dictionary<int, FinalFractalHelper.FinalFractalProfile>)field.GetValue(null);

                profiles.Add(ModContent.ItemType<TrueTerraBlade>(), new(74f, AAColor.TerraGlow));
                profiles.Add(ModContent.ItemType<FleshrendClaymore>(), new(52f, Color.Crimson));
                profiles.Add(ModContent.ItemType<TrueFleshrendClaymore>(), new(56f, Color.Crimson));

                profiles.Add(ModContent.ItemType<TheLolkat>(), new(98f, AAColor.Rainbow1));

                profiles.Add(ModContent.ItemType<CosmicFury>(), new(58f, Color.Magenta));

                profiles.Add(ModContent.ItemType<Apocalypse>(), new(54f, Color.Orange));

                profiles.Add(ModContent.ItemType<PrismaticGreatsword>(), new(60f, AAColor.Rainbow1));
                profiles.Add(ModContent.ItemType<Poppy>(), new(44f, AAColor.Rainbow1));
                profiles.Add(ModContent.ItemType<AmethystGreatsword>(), new(50f, Color.MediumPurple));
                profiles.Add(ModContent.ItemType<AmethystSaber>(), new(44f, Color.MediumPurple));
                profiles.Add(ModContent.ItemType<TopazGreatsword>(), new(50f, Main.OurFavoriteColor));
                profiles.Add(ModContent.ItemType<TopazSaber>(), new(44f, Main.OurFavoriteColor));
                profiles.Add(ModContent.ItemType<SapphireGreatsword>(), new(50f, Color.Aquamarine));
                profiles.Add(ModContent.ItemType<SapphireSaber>(), new(44f, Color.Aquamarine));
                profiles.Add(ModContent.ItemType<EmeraldGreatsword>(), new(50f, Color.Green));
                profiles.Add(ModContent.ItemType<EmeraldSaber>(), new(44f, Color.Green));
                profiles.Add(ModContent.ItemType<RubyGreatsword>(), new(50f, Color.IndianRed));
                profiles.Add(ModContent.ItemType<RubySaber>(), new(44f, Color.IndianRed));
                profiles.Add(ModContent.ItemType<AmberGreatsword>(), new(50f, Color.Orange));
                profiles.Add(ModContent.ItemType<AmberSaber>(), new(44f, Color.Orange));
                profiles.Add(ModContent.ItemType<DiamondGreatsword>(), new(50f, Color.White));
                profiles.Add(ModContent.ItemType<DiamondSaber>(), new(44f, Color.White));
                profiles.Add(ItemID.BeamSword, new(52f, Color.Yellow));

                profiles.Add(ModContent.ItemType<BladeOfEvil>(), new(68f, AAColor.Jevil));

                profiles.Add(ModContent.ItemType<ChaosSlayerEX>(), new(86f, AAColor.Shen3));
                profiles.Add(ModContent.ItemType<ChaosSlayer>(), new(90f, AAColor.Shen2));
                profiles.Add(ModContent.ItemType<ReignOfFire>(), new(56f, Color.OrangeRed));
                profiles.Add(ModContent.ItemType<Amenomuraku>(), new(64f, Color.LightBlue));
                profiles.Add(ModContent.ItemType<PerfectChaos>(), new(120f, AAColor.Shen));
                profiles.Add(ModContent.ItemType<Masamune>(), new(62f, AAColor.Yamata));
                profiles.Add(ModContent.ItemType<Chaos_Item>(), new(90f, AAColor.Shen2));
                profiles.Add(ModContent.ItemType<DraconianDawn>(), new(86f, Color.OrangeRed));
                profiles.Add(ModContent.ItemType<BlazingDawn>(), new(62f, Color.OrangeRed));
                profiles.Add(ModContent.ItemType<FlamingFury>(), new(50f, Color.OrangeRed));
                profiles.Add(ModContent.ItemType<OceanRazor>(), new(40f, Color.DarkBlue));
                profiles.Add(ModContent.ItemType<DoomiteSaber>(), new(60f, Color.Red));
                profiles.Add(ModContent.ItemType<DesertScimitar>(), new(70f, Color.SandyBrown));
                profiles.Add(ModContent.ItemType<DreadTwilight>(), new(76f, Color.LightBlue));
                profiles.Add(ModContent.ItemType<AbyssalTwilight>(), new(60f, Color.LightBlue));
                profiles.Add(ModContent.ItemType<ExilesKatana>(), new(66f, Color.LightBlue));
                profiles.Add(ModContent.ItemType<IceLongsword>(), new(64f, Color.AliceBlue));

                profiles.Add(ModContent.ItemType<InfinityBlade>(), new(84f, Color.Red));
                profiles.Add(ModContent.ItemType<RiftShredder>(), new(56f, Color.Red));
                profiles.Add(ModContent.ItemType<BreakingDawn>(), new(56f, Color.Yellow));

                profiles.Add(ModContent.ItemType<Verdict>(), new(92f, Color.SeaGreen));
                profiles.Add(ModContent.ItemType<Judgment>(), new(72f, Color.Blue));
                profiles.Add(ModContent.ItemType<SultansScimitar>(), new(66f, Color.SandyBrown));

                profiles.Add(ModContent.ItemType<SagittariusLeg>(), new(92f, Color.Red));

                profiles.Add(ModContent.ItemType<RomulusTazesaber>(), new(54f, Color.Purple));

                profiles.Add(ModContent.ItemType<SubzeroSlasher>(), new(56f, Color.Snow));

                profiles.Add(ModContent.ItemType<Olympia>(), new(52f, Color.Turquoise));
                profiles.Add(ModContent.ItemType<SkycutterKopis>(), new(50f, Color.Turquoise));
                profiles.Add(ItemID.SilverBroadsword, new(38f, Color.Silver));
                profiles.Add(ItemID.TungstenBroadsword, new(38f, Color.LightSeaGreen));

                profiles.Add(ModContent.ItemType<Excalihare>(), new(80f, AAColor.Rainbow1));
                //TODO: this item got a prequel in lost sea. add that here when the time is right

                profiles.Add(ModContent.ItemType<CarnalCrusher>(), new(90f, Color.Crimson));

                profiles.Add(ModContent.ItemType<UltimaShortsword>(), new(36f, AAColor.Rainbow1));
                profiles.Add(ModContent.ItemType<TrueCopperShortsword>(), new(64f, AAColor.Rainbow1));

                field.SetValue(null, profiles);
            }
        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public override void Load()
        {
            Logger.InfoFormat("{0} AA log", Name);

            instance = this;
            AncientCoin = CustomCurrencyManager.RegisterCurrency(new ACoin(ModContent.ItemType<AncientCoin>()));
            GoblinSoul = CustomCurrencyManager.RegisterCurrency(new GSouls(ModContent.ItemType<GoblinSoul>()));
            BloodRune = CustomCurrencyManager.RegisterCurrency(new BRune(ModContent.ItemType<BloodRune>()));
            PirateBooty = CustomCurrencyManager.RegisterCurrency(new PBooty(ModContent.ItemType<PirateBooty>()));
            MonsterSoul = CustomCurrencyManager.RegisterCurrency(new MSouls(ModContent.ItemType<MonsterSoul>()));
            HalloweenTreat = CustomCurrencyManager.RegisterCurrency(new HTreat(ModContent.ItemType<HalloweenTreat>()));
            ChristmasCheer = CustomCurrencyManager.RegisterCurrency(new CCheer(ModContent.ItemType<ChristmasCheer>()));
            MartianCredit = CustomCurrencyManager.RegisterCurrency(new MCredit(ModContent.ItemType<MartianCredit>()));

            if (Main.rand == null)
                Main.rand = new UnifiedRandom();

            GameShaders.Armor.BindShader(Find<ModItem>("BlazingDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye)).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);
            GameShaders.Armor.BindShader(Find<ModItem>("AbyssalDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye).UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(Find<ModItem>("DoomsdayDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.VortexDye)).UseImage("Images/Misc/noise").UseColor(0f, 0f, 0f).UseSecondaryColor(1f, 0f, 0f).UseSaturation(1f);
            GameShaders.Armor.BindShader(Find<ModItem>("DiscordianDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye).UseColor(0.66f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f));
            GameShaders.Armor.BindShader(Find<ModItem>("DiscordianInfernoDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye)).UseColor(0.88f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f);
            GameShaders.Armor.BindShader(Find<ModItem>("AbyssalWrathDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye).UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(Find<ModItem>("BlazingFuryDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye)).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);

            Rift = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.Rifthotkey"), "C");
            RiftReturn = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.RiftReturnhotkey"), "X");

            AccessoryAbilityKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.AccessoryAbilityKey"), "U");
            ArmorAbilityKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.ArmorAbilityKey"), "Y"); 
            
            On_Wiring.ActuateForced += Wiring_ActuateForced;
            On_Wiring.Actuate += Actuate;

            if (!Main.dedServ)
            {
                Config.Load();
                LoadClient();
            }
        }

        public void LoadClient()
        {
            Asset<Effect> shader = ModContent.Request<Effect>("AAModClassic/Effects/Shockwave");
            ScreenShaderData shaderdata = new(shader, "Shockwave");
            Filters.Scene["AAModClassic:Shockwave"] = new Filter(shaderdata, EffectPriority.VeryHigh);
            Filters.Scene["AAModClassic:Shockwave"].Load();

            shader = ModContent.Request<Effect>("AAModClassic/Effects/Mask");
            shaderdata = new(shader, "Mask");
            Filters.Scene["AAModClassic:Mask"] = new Filter(shaderdata, EffectPriority.VeryHigh);
            Filters.Scene["AAModClassic:Mask"].Load();

            //TODO: Perhaps move these to their proper content area so the textures are easier to keep track off
            Main.QueueMainThreadAction(() => {
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Biomes/Backgrounds/VoidBH", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/MireBiome_Moon", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/Sun", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/World/Biomes/Backgrounds/FogTex", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Skies/AkumaSun", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/Skies/YamataSky_Moon", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/Skies/YamataSky_Beam", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Skies/AkumaAMeteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Skies/AkumaMeteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/SkyTex", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/_BossShen/Skies/ShenMeteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/_PostMoonlord/NPCs/_BossAthenaA/Skies/AthenaBolt", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/_PostMoonlord/NPCs/_BossAthenaA/Skies/AthenaFlash", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/_BossZero/ZeroShield", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>(ModContent.GetInstance<RadiumStar>().Texture, AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>(ModContent.GetInstance<StarStaff_Star2>().Texture, AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>(ModContent.GetInstance<TruffleToad_LargeFungusBubble>().Texture, AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/_BossZero/Protocol/ProtoStar", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/___PreHardmode/Items/_BossSagittarius/Weapons/SagittariusShield", AssetRequestMode.ImmediateLoad).Value);

                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/Biomes/StormBiome_Bolt", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/Biomes/StormBiome_Flash", AssetRequestMode.ImmediateLoad).Value);
            });

            Filters.Scene["AAModClassic:ShenSky"] = new Filter(new ShenSkyData("FilterMiniTower").UseColor(.5f, 0f, .5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:ShenSky"] = new ShenSky();

            Filters.Scene["AAModClassic:ShenASky"] = new Filter(new ShenASkyData("FilterMiniTower").UseColor(.7f, 0f, .7f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:ShenASky"] = new ShenASky();

            Filters.Scene["AAModClassic:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:MireSky"] = new MireSky();

            Filters.Scene["AAModClassic:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:VoidSky"] = new VoidSky();

            Filters.Scene["AAModClassic:AthenaSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:AthenaSky"] = new AthenaSky();

            Filters.Scene["AAModClassic:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:InfernoSky"] = new InfernoSky();

            Filters.Scene["AAModClassic:AkumaSky"] = new Filter(new AkumaSkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:AkumaSky"] = new AkumaSky();

            Filters.Scene["AAModClassic:YamataSky"] = new Filter(new YamataSkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:YamataSky"] = new YamataSky();

            Filters.Scene["AAModClassic:AnubisSky"] = new Filter(new AnubisSkyData("FilterMiniTower").UseColor(.2f, .5f, .2f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:AnubisSky"] = new AnubisSky();

            ReplaceItemTexture(3460, "AAModClassic/Resprites/Luminite");
            ReplaceItemTexture(512, "AAModClassic/Resprites/SoulOfNight");

            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/Invoker/InvokedCaligula_Head", EquipType.Head, name: "InvokedCaligulaHead", equipTexture: new InvokedCaligulaHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/Invoker/InvokedCaligula_Body", EquipType.Body, name: "InvokedCaligulaBody", equipTexture: new InvokedCaligulaBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/Invoker/InvokedCaligula_Legs", EquipType.Legs, name: "InvokedCaligulaLegs", equipTexture: new InvokedCaligulaLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/Ohno/ono_Head", EquipType.Head, name: "onoHead", equipTexture: new Items.Vanity.Ohno.OnoHead());
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/Ohno/ono_Body", EquipType.Body, name: "onoBody", equipTexture: new Items.Vanity.Ohno.OnoBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/Ohno/ono_Legs", EquipType.Legs, name: "onoLegs", equipTexture: new Items.Vanity.Ohno.OnoLegs());

            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/Cerberus/InvokerHood_Head", EquipType.Head, name: "InvokerHead", equipTexture: new InvokerHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/Cerberus/InvokerRobe_Body", EquipType.Body, name: "InvokerBody", equipTexture: new InvokerBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/Cerberus/InvokerPants_Legs", EquipType.Legs, name: "InvokerLegs", equipTexture: new InvokerLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/CC/CCRobe_Legs", EquipType.Legs, name: "CCRobe_Legs");
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Vanity/CC/Shiny/ShinyCCRobe_Legs", EquipType.Legs, name: "ShinyCCRobe_Legs");

            BackgroundTextureLoader.AddBackgroundTexture(this, "AAModClassic/BlankTex");
        }

        public Dictionary<int, Asset<Texture2D>> vanillaTextureBackups = [];

        public void ReplaceItemTexture(int id, string texturePath)
        {
            vanillaTextureBackups.Add(id, TextureAssets.Item[id]);
            TextureAssets.Item[id] = ModContent.Request<Texture2D>(texturePath);
        }

        public void ResetItemTexture(int id)
        {
            if (vanillaTextureBackups.TryGetValue(id, out Asset<Texture2D> value))
                TextureAssets.Item[id] = value;
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                ResetItemTexture(3460);
                ResetItemTexture(512);
            }

            instance = null;
            Rift = null;
            RiftReturn = null;
            AccessoryAbilityKey = null;
            ArmorAbilityKey = null;
        }

        private void Wiring_ActuateForced(Terraria.On_Wiring.orig_ActuateForced orig, int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileType == ModContent.TileType<Tiles.AcropolisBlock2_Tile>() || tile.TileType == ModContent.TileType<Tiles.AcropolisBlock_Tile>() ||
                tile.TileType == ModContent.TileType<Tiles.GreedStone_Tile>() || tile.TileType == ModContent.TileType<Tiles.GreedBrick_Tile>())
            {
                return;
            }
            orig(i, j);
        }

        private static bool Actuate(Terraria.On_Wiring.orig_Actuate orig, int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileType == ModContent.TileType<Tiles.AcropolisBlock2_Tile>() || tile.TileType == ModContent.TileType<Tiles.AcropolisBlock_Tile>() ||
                tile.TileType == ModContent.TileType<Tiles.GreedStone_Tile>() || tile.TileType == ModContent.TileType<Tiles.GreedBrick_Tile>())
            {
                return false;
            }
            return orig(i, j);
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
                if (Main.netMode == NetmodeID.Server)
                    BaseNet.WriteToPacket(AAMod.instance.GetPacket(), 0, owner, projID, friendly, hostile).Send();
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

    public class AAModSystem : ModSystem
    {
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
            ModContent.GetInstance<AAMod>().TerratoolGroxState = new TerratoolGroxUI();
            ModContent.GetInstance<AAMod>().TerratoolGroxState.Activate();
            ModContent.GetInstance<AAMod>().TerratoolEXState = new TerratoolEXUI();
            ModContent.GetInstance<AAMod>().TerratoolEXState.Activate();
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

        private static void LuckyCheckProgress()
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

        private static GameTime lastUpdateUIGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            lastUpdateUIGameTime = gameTime;

            if (AAMod.instance.TerratoolInterface?.CurrentState != null)
            {
                AAMod.instance.TerratoolInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int wireSelectionLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Wire Selection"));
            if (wireSelectionLayerIndex != -1)
            {
                layers.Insert(wireSelectionLayerIndex, new LegacyGameInterfaceLayer(
                "AAModClassic: Radial UIs",
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
    }
}
