using AAModClassic._Content._Dev.___PreHardmode.Items.Currency;
using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA.Skies;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic._Content.BloodMoon.___PreHardmode.Items.Currency;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Chaos.__Hardmode.Items.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened.Skies;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Skies;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Crimson.__Hardmode.Items.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons;
using AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA.Skies;
using AAModClassic._Content.Evil.__Hardmode.Items.Weapons;
using AAModClassic._Content.FrostMoon.__Hardmode.Items.Currency;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossTruffleToad;
using AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Currency;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened.Skies;
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
using AAModClassic._Content.SolarEclipse.__Hardmode.Items.Currency;
using AAModClassic._Content.Stars._PostMoonlord.Items.Weapons;
using AAModClassic._Content.Underground.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Underground.__Hardmode.Items.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;
using AAModClassic._Content.Void._PostMoonlord.Items.Accessories.Vanity;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._CrossMod;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons;
using AAModClassic.Assets;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.NPCs;
using AAModClassic.Base.Projectiles;
using AAModClassic.Globals;
using AAModClassic.UI.Core;
using AAModClassic.UI.Tools;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

        public static readonly PropertyInfo valueProp = typeof(LocalizedText).GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
        public static void AddLocalization(string key, string value)
        {
            var text = LanguageManager.Instance.GetOrRegister(key, () => value);
            valueProp.SetValue(text, value);
        }

        public static string GetStatDifferences(Player playerA, Player playerB)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(Player);

            FieldInfo damageDataField = type.GetField("damageData", BindingFlags.NonPublic | BindingFlags.Instance);
            if (damageDataField != null)
            {
                Array arrA = damageDataField.GetValue(playerA) as Array;
                Array arrB = damageDataField.GetValue(playerB) as Array;
                ScanPrivateDamageData(arrA, arrB, sb);
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.Name == "damageStats" || field.Name == "damageData")
                    continue;
                if (!field.FieldType.IsValueType)
                    continue;

                object valA, valB;
                try
                {
                    valA = field.GetValue(playerA);
                    valB = field.GetValue(playerB);
                }
                catch { continue; }

                string formattedLine = FormatValueOrBoolChange(field.FieldType, valA, valB, field.Name);
                if (!string.IsNullOrEmpty(formattedLine))
                    sb.Append(formattedLine);
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanRead || prop.Name == "damageStats" || prop.Name == "damageData")
                    continue;

                object valA, valB;
                try
                {
                    valA = prop.GetValue(playerA);
                    valB = prop.GetValue(playerB);
                }
                catch { continue; }

                string formattedLine = FormatValueOrBoolChange(prop.PropertyType, valA, valB, prop.Name);
                if (!string.IsNullOrEmpty(formattedLine))
                    sb.Append(formattedLine);
            }

            return sb.ToString();
        }

        private static void ScanPrivateDamageData(Array arrA, Array arrB, StringBuilder sb)
        {
            if (arrA == null || arrB == null)
                return;

            int length = Math.Min(arrA.Length, arrB.Length);
            for (int i = 0; i < length; i++)
            {
                object itemA = arrA.GetValue(i);
                object itemB = arrB.GetValue(i);
                if (itemA == null || itemB == null)
                    continue;

                string className = DamageClassLoader.GetDamageClass(i)?.Name ?? $"Class{i}";
                className = className.Replace("DamageClass", "");
                className = FormatFieldName(className);

                Type dataType = itemA.GetType();
                FieldInfo[] subFields = dataType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (FieldInfo subField in subFields)
                {
                    object subValA = subField.GetValue(itemA);
                    object subValB = subField.GetValue(itemB);

                    if (Equals(subValA, subValB))
                        continue;

                    if (subField.FieldType == typeof(StatModifier))
                    {
                        StatModifier modA = (StatModifier)subValA;
                        StatModifier modB = (StatModifier)subValB;

                        if (modA.Additive != modB.Additive)
                        {
                            double percentIncrease = (modB.Additive - modA.Additive) * 100;
                            if (percentIncrease > 0)
                                sb.Append($"{percentIncrease:0.#}% increased {className} {FormatFieldName(subField.Name)}\n");
                        }
                        if (modA.Flat != modB.Flat)
                        {
                            float flatIncrease = modB.Flat - modA.Flat;
                            if (flatIncrease > 0)
                                sb.Append($"{flatIncrease:0.#} {className} Flat {FormatFieldName(subField.Name)}\n");
                        }
                    }
                    else if (subField.FieldType == typeof(float))
                    {
                        float numA = (float)subValA;
                        float numB = (float)subValB;
                        float diff = numB - numA;

                        if (diff > 0)
                        {
                            string fieldName = subField.Name.ToLower();

                            if (fieldName.Contains("crit") || fieldName.Contains("pen"))
                                sb.Append($"{diff:0.#}% increased {className} {FormatFieldName(subField.Name)}\n");
                            else if (fieldName.Contains("speed"))
                            {
                                double percentIncrease = diff * 100;
                                sb.Append($"{percentIncrease:0.#}% increased {className} {FormatFieldName(subField.Name)}\n");
                            }
                            else
                                sb.Append($"{diff:0.#}% increased {className} {FormatFieldName(subField.Name)}\n");
                        }
                    }
                }
            }
        }

        private static string FormatValueOrBoolChange(Type type, object valA, object valB, string displayName)
        {
            if (Equals(valA, valB))
                return null;
            string formattedName = FormatFieldName(displayName);

            // Booleans
            if (type == typeof(bool))
            {
                bool boolB = (bool)valB;
                return $"{formattedName} {(boolB ? "Enabled" : "Disabled")}\n";
            }
            // Integers / Flat Numbers
            if (type == typeof(int) || type == typeof(long) || type == typeof(short))
            {
                long difference = Convert.ToInt64(valB) - Convert.ToInt64(valA);
                if (difference > 0)
                    return $"{difference} {formattedName}\n";
            }
            // Floating point values / Percentages
            else if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
            {
                double numA = Convert.ToDouble(valA);
                double numB = Convert.ToDouble(valB);
                double percentIncrease = (numA == 0) ? numB * 100 : ((numB - numA) / numA) * 100;

                if (percentIncrease > 0)
                    return $"{percentIncrease:0.#}% increased {formattedName}\n";
            }

            return null;
        }

        private static string FormatFieldName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            if (name.StartsWith("stat") && name.Length > 4 && char.IsUpper(name[4]))
                name = name.Substring(4);

            string spaced = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
            spaced = Regex.Replace(spaced, "([A-Z])([A-Z][a-z])", "$1 $2");

            return char.ToUpper(spaced[0]) + spaced.Substring(1);
        }

        public override void PostSetupContent()
        {
            WeakReferences.PerformModSupport();

            //Set Bonus Text Generator

            foreach(ModItem modItem in instance.GetContent<ModItem>())
            {
                if (!modItem.Item.vanity && modItem.Name.Contains("Helmet"))
                {
                    Player dummyPlayerA = new();
                    Player dummyPlayerB = new();

                    modItem.UpdateArmorSet(dummyPlayerB);

                    string statDifferences = GetStatDifferences(dummyPlayerA, dummyPlayerB);

                    if(statDifferences == "" && instance.TryFind<ModItem>(modItem.Name.Replace("Helmet", "Chestplate"), out var chest))
                    {
                        dummyPlayerA = new();
                        dummyPlayerB = new();
                        chest.UpdateArmorSet(dummyPlayerB);
                        statDifferences = GetStatDifferences(dummyPlayerA, dummyPlayerB);
                    }

                    AddLocalization("Mods.AAModClassic.SetBonuses." + modItem.Name.Replace("Helmet", ""), statDifferences);
                }
            }

            Array.Resize(ref AASets.Goblins, NPCLoader.NPCCount);

            if (!Main.dedServ)
            {
                foreach (ModItem modItem in this.GetContent<ModItem>())
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

                profiles.Add(ModContent.ItemType<Ikari>(), new(86f, AAColor.Shen3));
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

            Rift = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.Rifthotkey"), "C");
            RiftReturn = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.RiftReturnhotkey"), "X");

            AccessoryAbilityKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.AccessoryAbilityKey"), "U");
            ArmorAbilityKey = KeybindLoader.RegisterKeybind(this, Language.GetTextValue("Mods.AAModClassic.Keybinds.ArmorAbilityKey"), "Y"); 
            
            On_Wiring.ActuateForced += Wiring_ActuateForced;
            On_Wiring.Actuate += Actuate;

            if (!Main.dedServ)
            {
                AALuckyConfig.Load();
                LoadClient();
            }
        }

        public void LoadClient()
        {
            GameShaders.Armor.BindShader(Find<ModItem>("BlazingDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye)).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);
            GameShaders.Armor.BindShader(Find<ModItem>("AbyssalDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye).UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(Find<ModItem>("DoomsdayDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.VortexDye)).UseImage("Images/Misc/noise").UseColor(0f, 0f, 0f).UseSecondaryColor(1f, 0f, 0f).UseSaturation(1f);
            GameShaders.Armor.BindShader(Find<ModItem>("DiscordianDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.LivingFlameDye).UseColor(0.66f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f));
            GameShaders.Armor.BindShader(Find<ModItem>("DiscordianInfernoDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye)).UseColor(0.88f, 0f, 1f).UseSecondaryColor(0.66f, 0f, 1f);
            GameShaders.Armor.BindShader(Find<ModItem>("AbyssalWrathDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye).UseColor(146f / 255f, 30f / 255f, 68f / 255f).UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f));
            GameShaders.Armor.BindShader(Find<ModItem>("BlazingFuryDye").Type, GameShaders.Armor.GetShaderFromItemId(ItemID.HadesDye)).UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f).UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f);

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
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/Skies/AkumaASky_Sun", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/Skies/YamataASky_Moon", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Mire/_PostMoonlord/NPCs/__BossYamata/Awakened/Skies/YamataASky_Beam", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/Skies/AkumaASky_Meteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/_PostMoonlord/NPCs/__BossAkuma/Awakened/Skies/AkumaASky_Meteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Inferno/World/Biomes/Backgrounds/SkyTex", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Chaos/_PostMoonlord/NPCs/__BossShenDoragon/Skies/ShenDoragonSky_Meteor", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/_PostMoonlord/NPCs/__BossAthenaA/Skies/AthenaASky_Bolt", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Acropolis/_PostMoonlord/NPCs/__BossAthenaA/Skies/AthenaASky_Flash", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/__BossZero/Zero_Shield", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>(ModContent.GetInstance<StarStaff_Star2>().Texture, AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>(ModContent.GetInstance<TruffleToad_LargeFungusBubble>().Texture, AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/_PostMoonlord/NPCs/__BossZero/Awakened/ZeroA_ProtoStarRay", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Content/Void/___PreHardmode/Items/_BossSagittarius/Accessories/SagittariusShield", AssetRequestMode.ImmediateLoad).Value);

                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/World/Biomes/StormBiome_Bolt", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/World/Biomes/StormBiome_Flash", AssetRequestMode.ImmediateLoad).Value);

                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/CthulhuPortal", AssetRequestMode.ImmediateLoad).Value);
                //PremultiplyTexture(ModContent.Request<Texture2D>("NPCs/Bosses/SoC/CthulhuPortal2", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/Portal", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/CthulhuSpawn", AssetRequestMode.ImmediateLoad).Value);
                PremultiplyTexture(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/Items/SoulOfCthulhu/Weapons/CthulhuCannon_CthulhuBoom", AssetRequestMode.ImmediateLoad).Value);
            });

            Filters.Scene["AAModClassic:ShenDoragonSky"] = new Filter(new ShenDoragonSkyData("FilterMiniTower").UseColor(.5f, 0f, .5f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:ShenDoragonSky"] = new ShenDoragonSky();

            Filters.Scene["AAModClassic:ShenDoragonASky"] = new Filter(new ShenDoragonASkyData("FilterMiniTower").UseColor(.7f, 0f, .7f).UseOpacity(0.2f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:ShenDoragonASky"] = new ShenDoragonASky();

            Filters.Scene["AAModClassic:MireSky"] = new Filter(new MireSkyData("FilterMiniTower").UseColor(0f, 0.20f, 1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:MireSky"] = new MireSky();

            Filters.Scene["AAModClassic:VoidSky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0.15f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:VoidSky"] = new VoidSky();

            Filters.Scene["AAModClassic:AthenaASky"] = new Filter(new VoidSkyData("FilterMiniTower").UseColor(0f, 0.1f, 0.1f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:AthenaASky"] = new AthenaASky();

            Filters.Scene["AAModClassic:InfernoSky"] = new Filter(new InfernoSkyData("FilterMiniTower").UseColor(1f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.High);
            SkyManager.Instance["AAModClassic:InfernoSky"] = new InfernoSky();

            Filters.Scene["AAModClassic:AkumaASky"] = new Filter(new AkumaASkyData("FilterMiniTower").UseColor(0f, 0.3f, 0.4f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:AkumaASky"] = new AkumaASky();

            Filters.Scene["AAModClassic:YamataASky"] = new Filter(new YamataASkyData("FilterMiniTower").UseColor(.7f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:YamataASky"] = new YamataASky();

            Filters.Scene["AAModClassic:AnubisASky"] = new Filter(new AnubisASkyData("FilterMiniTower").UseColor(.2f, .5f, .2f).UseOpacity(0.5f), EffectPriority.VeryHigh);
            SkyManager.Instance["AAModClassic:AnubisASky"] = new AnubisASky();

            ReplaceItemTexture(3460, "AAModClassic/Resprites/Luminite");
            ReplaceItemTexture(512, "AAModClassic/Resprites/SoulOfNight");

            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_EX/_PostMoonlord/Items/Accessories/InvokedCaligula_Head", EquipType.Head, name: "InvokedCaligula_Head", equipTexture: new InvokedCaligulaHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_EX/_PostMoonlord/Items/Accessories/InvokedCaligula_Body", EquipType.Body, name: "InvokedCaligula_Body", equipTexture: new InvokedCaligulaBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_EX/_PostMoonlord/Items/Accessories/InvokedCaligula_Legs", EquipType.Legs, name: "InvokedCaligula_Legs", equipTexture: new InvokedCaligulaLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/Void/_PostMoonlord/Items/Accessories/Vanity/Ono_Head", EquipType.Head, name: "Ono_Head", equipTexture: new OnoHead());
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/Void/_PostMoonlord/Items/Accessories/Vanity/Ono_Body", EquipType.Body, name: "Ono_Body", equipTexture: new OnoBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/Void/_PostMoonlord/Items/Accessories/Vanity/Ono_Legs", EquipType.Legs, name: "Ono_Legs", equipTexture: new OnoLegs());

            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/__Hardmode/Items/Armor/Vanity/CerberusHelmet_Head", EquipType.Head, name: "CerberusHelmet_Head", equipTexture: new InvokerHead())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/__Hardmode/Items/Armor/Vanity/CerberusChestplate_Body", EquipType.Body, name: "CerberusChestplate_Body", equipTexture: new InvokerBody())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/__Hardmode/Items/Armor/Vanity/CerberusLeggings_Legs", EquipType.Legs, name: "CerberusLeggings_Legs", equipTexture: new InvokerLegs())/* tModPorter Note: armTexture and femaleTexture now part of new spritesheet. https://github.com/tModLoader/tModLoader/wiki/Armor-Texture-Migration-Guide */;

            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/__Hardmode/Items/Armor/Vanity/CCChestplate_Legs", EquipType.Legs, name: "CCChestplate_Legs");
            EquipLoader.AddEquipTexture(this, "AAModClassic/_Content/_Dev/__Hardmode/Items/Armor/Vanity/CCChestplateS_Legs", EquipType.Legs, name: "CCChestplateS_Legs");

            BackgroundTextureLoader.AddBackgroundTexture(this, AssetDirectory.General.Nothing);
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

            var field = typeof(FinalFractalHelper).GetField("_fractalProfiles", BindingFlags.Static | BindingFlags.NonPublic);
            if (field != null)
            {
                var profiles = (Dictionary<int, FinalFractalHelper.FinalFractalProfile>)field.GetValue(null);
                foreach(int key in profiles.Keys)
                {
                    if (ContentSamples.ItemsByType[key].ModItem != null && ContentSamples.ItemsByType[key].ModItem.Mod is AAMod)
                        profiles.Remove(key);
                }
                profiles.Remove(ItemID.SilverBroadsword);
                profiles.Remove(ItemID.TungstenBroadsword);
                profiles.Remove(ItemID.BeamSword);

                field.SetValue(null, profiles);
            }

            instance = null;
            Rift = null;
            RiftReturn = null;
            AccessoryAbilityKey = null;
            ArmorAbilityKey = null;
        }

        private void Wiring_ActuateForced(Terraria.On_Wiring.orig_ActuateForced orig, int i, int j)
        {
            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return;

            orig(i, j);
        }

        private static bool Actuate(Terraria.On_Wiring.orig_Actuate orig, int i, int j)
        {
            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return false;

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

            On_Player.GetPickaxeDamage += ModifyLuminitePickaxePower;

            BrokenCodeTeleportUnofficialEdits.ApplyEdits();
        }

        private int ModifyLuminitePickaxePower(On_Player.orig_GetPickaxeDamage orig, Player self, int x, int y, int pickPower, int hitBufferIndex, Tile tileTarget)
        {
            if (tileTarget.TileType == TileID.LunarOre && pickPower < 225)
                return 0;

            return orig(self, x, y, pickPower, hitBufferIndex, tileTarget);
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
            AALuckyConfig.SaveConfig();
        }

        private static void LuckyCheckProgress()
        {
            AALuckyConfig.LuckyOre.Clear();
            AALuckyConfig.LuckyPotion.Clear();
            AALuckyConfig.ListRareNpc.Clear();
            Item item = new Item();
            for (int i = -48; i < ItemLoader.ItemCount; i++)
            {
                item.netDefaults(i);
                if (item.createTile > TileID.Dirt && Main.tileOreFinderPriority[item.createTile] > 0 && !Main.tileContainer[item.createTile] && item.createTile != TileID.FakeContainers && item.createTile != TileID.FakeContainers2)
                {
                    AALuckyConfig.LuckyOre.Add(item.type, Main.tileOreFinderPriority[item.createTile]);
                }
                if (item.buffType > 0 && item.buffType != BuffID.WellFed && item.buffTime > 0 && item.type > ItemID.Celeb2)
                {
                    AALuckyConfig.LuckyPotion.Add(item.type, item.value);
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
                    AALuckyConfig.ListRareNpc.Add(i);
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
