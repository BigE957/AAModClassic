using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.CrossMod.CalamityMod
{
    public class CalamityMod : ModSystem
    {
        internal static Mod Calamity = null;
        public override void Load()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Calamity))
                Calamity = null;
        }

        public override void PostSetupContent()
        {
            if (Calamity != null)
                astralDust = Calamity.Find<ModDust>("AstralChunkDust").Type;
        }

        private static readonly Dictionary<string, int> modItemCache = []; 
        private static readonly Dictionary<string, int> modProjectileCache = [];
        private static readonly Dictionary<string, int>  modBuffCache = [];

        public static int AstralChunkDust => astralDust;
        private static int astralDust = -1;

        public static bool IsEnabled => Calamity != null;
        public static bool IsRevengance => Calamity != null && (bool)Calamity.Call("GetDifficultyActive", "revengeance");
        public static bool IsDeath => Calamity != null && (bool)Calamity.Call("GetDifficultyActive", "death");
        public static DamageClass RogueClass => Calamity?.Find<DamageClass>("RogueDamageClass");
        
        public static object Call(params object[] args) => Calamity?.Call(args);
        
        public static int GetModItem(string name)
        {
            if (Calamity == null)
                return -1;

            if (modItemCache.TryGetValue(name, out int type))
                return type;

            if (Calamity.TryFind(name, out ModItem item))
            {
                modItemCache.Add(name, item.Type);
                return item.Type;
            }

            return -1;
        }

        public static int GetModProjectileType(string name)
        {
            if (Calamity == null)
                return -1;

            if (modProjectileCache.TryGetValue(name, out int type))
                return type;

            if (Calamity.TryFind(name, out ModProjectile projectile))
            {
                modProjectileCache.Add(name, projectile.Type);
                return projectile.Type;
            }

            return -1;
        }
    
        public static int GetModBuffType(string name)
        {
            if (Calamity == null)
                return -1;

            if (modBuffCache.TryGetValue(name, out int type))
                return type;

            if (Calamity.TryFind(name, out ModBuff buff))
            {
                modBuffCache.Add(name, buff.Type);
                return buff.Type;
            }

            return -1;
        }
    }
}
