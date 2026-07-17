using AAModClassic._CrossMod.StarsAbove;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.Thorium
{
    public class ThoriumMod : ModSystem
    {
        internal static Mod Thorium = null;
        public override void Load()
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out Thorium))
                Thorium = null;
        }

        public override void AddRecipes()
        {
            if (IsEnabled)
            {
                healerClass = Thorium.Find<DamageClass>("HealerDamage");
                bardClass = Thorium.Find<DamageClass>("BardDamage");
            }
        }

        public override void PostSetupContent()
        {
            if(IsEnabled)
            {
                baseThoriumPlayer = Thorium.Find<ModPlayer>("ThoriumPlayer");

                Type thoriumPlayerType = baseThoriumPlayer.GetType();
                var field = thoriumPlayerType.GetField("soulEssence", BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                    thoriumPlayerFieldInfo.Add("soulEssence", field);
                else
                    return;
            }
        }

        public static bool IsEnabled => Thorium != null;
        private static DamageClass healerClass = null;
        private static DamageClass bardClass = null;

        public static DamageClass HealerClass => healerClass ?? (healerClass = Thorium?.Find<DamageClass>("HealerDamage"));
        public static DamageClass BardClass => bardClass ?? (bardClass = Thorium?.Find<DamageClass>("BardDamage"));

        private static readonly Dictionary<string, int> modBuffCache = [];
        private static readonly Dictionary<string, FieldInfo> thoriumPlayerFieldInfo = [];

        public static object Call(params object[] args) => Thorium?.Call(args);

        public static int GetModBuffType(string name)
        {
            if (Thorium == null)
                return -1;

            if (modBuffCache.TryGetValue(name, out int type))
                return type;

            if (Thorium.TryFind(name, out ModBuff buff))
            {
                modBuffCache.Add(name, buff.Type);
                return buff.Type;
            }

            return -1;
        }

        private static ModPlayer baseThoriumPlayer = null;

        public static ModPlayer GetThoriumModPlayer(Player p)
        {
            ModPlayer thoriumPlayer = baseThoriumPlayer ?? (baseThoriumPlayer = Thorium.Find<ModPlayer>("ThoriumPlayer"));
            foreach (ModPlayer mp in p.ModPlayers)
            {
                if (mp.Name == thoriumPlayer.Name)
                    return mp;
            }
            return null;
        }

        public static bool TryGainSoulEssence(Player player, NPC target, int chargeGain, bool canGiveScytheCharge)
        {
            ModPlayer thoriumPlayer = GetThoriumModPlayer(player);
            if (chargeGain > 0 && canGiveScytheCharge && target.IsHostile())
            {
                canGiveScytheCharge = false;
                player.AddBuff(GetModBuffType("SoulEssence"), 1800, true, false);
                CombatText.NewText(player.Hitbox, new Color(100, 255, 200), chargeGain, false, true);

                thoriumPlayerFieldInfo["soulEssence"].SetValue(thoriumPlayer, (int)thoriumPlayerFieldInfo["soulEssence"].GetValue(thoriumPlayer) + chargeGain);
            }

            return canGiveScytheCharge;
        }
    }
}
