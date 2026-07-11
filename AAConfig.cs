using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AAModClassic
{
    public class AAConfigClient : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AAConfigClient Instance; // See ExampleConfigServer.Instance for info.

        public bool NoAATownNPC;

        public bool NoBossDialogue;

        [DefaultValue(true)]
        public bool AncientIntroText;

        [DefaultValue(false)]
        public bool DisableNewAAReminderMessage;

        [DefaultValue(true)]
        public bool EnableContentReplacement;

        [DefaultValue(false)]
        public bool DisableAnticheat;

        [DefaultValue(false)]
        public bool DisablePinchThemes;

        [DefaultValue(false)]
        public bool HideIdentifiableInfo;
    }

    public static class AALuckyConfig
	{
		public static void Load()
		{
			if (!ReadConfig())
			{
                SetDefaults();
				ModContent.GetInstance<AAMod>().Logger.Warn("Couldn't find config file! Creating a new one...");
			}
            SaveConfig();
		}

        private static readonly string ConfigPath = Path.Combine(Main.SavePath, "ModConfigs", "AALuckyConfig.json");
		private static readonly Preferences Configuration = new(ConfigPath, false, false);

        public static void SetDefaults()
		{
            LuckyOre = [];
            LuckyPotion = [];
            ListRareNpc = [];
        }

        public static bool ReadConfig()
        {
            if (!Configuration.Load())
                return false;

            bool allGood = true;
            allGood &= TryReadField("LuckyOreMine", ref LuckyOre);
            allGood &= TryReadField("LuckyPotionGet", ref LuckyPotion);
            allGood &= TryReadField("RareNpcList", ref ListRareNpc);
            return allGood;
        }

        private static bool TryReadField<T>(string key, ref T field) where T : new()
        {
            try
            {
                string json = null;
                Configuration.Get(key, ref json);
                field = json != null
                    ? JsonConvert.DeserializeObject<T>(json) ?? new T()
                    : new T();
                return json != null;
            }
            catch
            {
                field = new T();
                return false;
            }
        }

        public static void SaveConfig()
        {
            Configuration.Clear();
            Configuration.Put("LuckyOreMine", JsonConvert.SerializeObject(LuckyOre));
            Configuration.Put("LuckyPotionGet", JsonConvert.SerializeObject(LuckyPotion));
            Configuration.Put("RareNpcList", JsonConvert.SerializeObject(ListRareNpc));
            Configuration.Save(true);
        }

        public static Dictionary<int, int> LuckyOre = [];
        public static Dictionary<int, int> LuckyPotion = [];
        public static HashSet<int> ListRareNpc = [];
        
    }
}
