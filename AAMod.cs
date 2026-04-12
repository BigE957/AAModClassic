using AAModClassic.___Content.Inferno._Hardmode.NPCs._Underground.Wyrm;
using AAModClassic.___Content.Inferno._PreHardmode.NPCs.Wyrmling;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs._BossYamata.Awakened.Skies;
using AAModClassic.___Content.Mire.World.Biomes;
using AAModClassic.Backgrounds;
using AAModClassic.Base.BaseMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.NPCs;
using AAModClassic.Base.Projectiles;
using AAModClassic.CrossMod;
using AAModClassic.Globals;
using AAModClassic.Items.Dev.Invoker;
using AAModClassic.NPCs.Enemies.Snow;
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
using Terraria.Audio;
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
    public partial class AAMod : Mod
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
        public static AAMod self = null;

        public static bool isFullyReady;

        public AAMod()
        {
            ContentAutoloadingEnabled = true;
            GoreAutoloadingEnabled = true;
            MusicAutoloadingEnabled = true;
            BackgroundAutoloadingEnabled = true;

            instance = this;
        }

        public static Texture2D GetTexture(string path) => ModContent.Request<Texture2D>("AAModClassic/" + path).Value;

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
                    GetCroppedTex(tex, new Rectangle(fx, 0, 16, 16 * 3)).GetData<Color>(data);
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
                            ModNPC npc = GetNPC(m == 0 ? "Wyrmling" : (m == 1 ? "WyrmlingBody" : (m == 2 ? "WyrmlingTail1" : "WyrmlingTail2")));
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
                            ModNPC npc = GetNPC(m == 0 ? "Wyrm" : (m == 1 ? "WyrmBody1" : (m == 2 ? "WyrmBody2" : (m == 3 ? "WyrmBody3" : "WyrmBody4"))));
                            if (npc != null)
                            {
                                npc.Banner = ModContent.NPCType<WyrmHead>();
                                npc.BannerItem = ModContent.ItemType<Items.Banners.WyrmBanner>();
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
                                npc.Banner = ModContent.NPCType<SnakeHead>();
                                npc.BannerItem = ModContent.ItemType<Items.Banners.SnakeBanner>();
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

        //TODO: Maim and Murder, maybe..
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
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

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

            Rift = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.Rifthotkey"), "C");
            RiftReturn = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.RiftReturnhotkey"), "X");

            AccessoryAbilityKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.AccessoryAbilityKey"), "U");
            ArmorAbilityKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.ArmorAbilityKey"), "Y"); 
            
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
            Asset<Effect> shader = ModContent.Request<Effect>("AAModClassic/Effects/Shockwave");
            ScreenShaderData shaderdata = new(shader, "Shockwave");
            Filters.Scene["AAModClassic:Shockwave"] = new Filter(shaderdata, EffectPriority.VeryHigh);
            Filters.Scene["AAModClassic:Shockwave"].Load();

            //TODO: Perhaps move these to their proper content area so the textures are easier to keep track off
            Main.QueueMainThreadAction(() => {
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/VoidBH", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/___Content/Mire/World/Biomes/Backgrounds/MireBiome_Moon", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/Sun", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/FogTex", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AkumaSun", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/___Content/Mire/_PostMoonlord/NPCs/_BossYamata/Awakened/Skies/YamataSky_Moon", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/___Content/Mire/_PostMoonlord/NPCs/_BossYamata/Awakened/Skies/YamataSky_Beam", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AkumaAMeteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AkumaMeteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/SkyTex", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/ShenMeteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AthenaBolt", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Backgrounds/AthenaFlash", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/Zero/ZeroShield", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Projectiles/RadiumStar", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Projectiles/Stars", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/Toad/ToadBubble", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/Zero/Protocol/ProtoStar", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/Textures/SagittariusShield", AssetRequestMode.ImmediateLoad).Value);

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

            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Dev/Invoker/InvokedCaligula_Head", EquipType.Head, name: "InvokedCaligulaHead", equipTexture: new InvokedCaligulaHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Dev/Invoker/InvokedCaligula_Body", EquipType.Body, name: "InvokedCaligulaBody", equipTexture: new InvokedCaligulaBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/Items/Dev/Invoker/InvokedCaligula_Legs", EquipType.Legs, name: "InvokedCaligulaLegs", equipTexture: new InvokedCaligulaLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

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

        public Dictionary<int, Asset<Texture2D>> vanillaTextureBackups = new Dictionary<int, Asset<Texture2D>>();

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

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                UnloadClient();
            }

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
        }

        public static Texture2D GetGlowmask(string Name)
        {
            return ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + Name + "_Glow").Value;
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

    public static class ModUtils
    {
        public static Texture2D GetTexture(this Mod mod, string path) => ModContent.Request<Texture2D>("AAModClassic/" + path).Value;

        public static SoundStyle GetLegacySoundSlot(this Mod mod, SoundType type, string path)
        {
            if (type == SoundType.Sound)
                return new SoundStyle("AAModClassic/" + path);
            return new();
        }
    }
}
