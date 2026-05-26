using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using static AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.ShenDoragonUtils;
using static System.Net.Mime.MediaTypeNames;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon
{
    public static class ShenDoragonUtils
    {
        public enum ChaosType
        {
            SomethingWentWrongAndItsAllYourFault = -1,
            Inferno = 0,
            Mire = 1, 
            Discord = 2
        }

        public static bool AddShenCrossmodDialogue(string key, LocalizedText text, Func<bool> condition) => CrossModDialogue.TryAdd(key, new(text, condition));

        public static string GetCrossModDialogue()
        {
            List<LocalizedText> crossModText = [];
            foreach (var (text, condition) in CrossModDialogue.Values)
            {
                if (condition.Invoke())
                    crossModText.Add(text);
            }

            if (crossModText.Count > 0)
                return crossModText[Main.rand.Next(crossModText.Count)].Value;

            return Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.ShenDoragon.Awakened.Health.50.NoMod");
        }

        public static readonly Dictionary<string, (LocalizedText text, Func<bool> condition)> CrossModDialogue = [];

        /// <summary>
        /// spwawns a proj that matches shens current color and theme... 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="npc"></param>
        /// <param name="spawnSource"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="Damage"></param>
        /// <param name="KnockBack"></param>
        /// <param name="Owner"></param>
        /// <param name="ai0"></param>
        /// <param name="ai1"></param>
        /// <param name="ai2"></param>
        public static void NewProjectileFlipped<T>(this NPC npc, IEntitySource spawnSource, Vector2 position, Vector2 velocity, int Damage, float KnockBack, int Owner = 1, float ai0 = 0, float ai1 = 0, float ai2 = 0, ChaosType? chaosType = null) where T : ShenDoragon_ChaosFireballAbstract
        {
            ShenDoragon_ChaosFireballAbstract proj = Projectile.NewProjectileDirect(spawnSource, position, velocity, ModContent.ProjectileType<T>(), Damage, KnockBack, Owner, ai0, ai1, ai2).ModProjectile as ShenDoragon_ChaosFireballAbstract;

            if (chaosType != null)
                proj.Chaos = (ChaosType)chaosType;
            else if (npc.type == ModContent.NPCType<ShenDoragon>())
                proj.Chaos = npc.spriteDirection == 1 ? ChaosType.Inferno : ChaosType.Mire;
            else if (npc.type == ModContent.NPCType<ShenDoragonA>())
                proj.Chaos = ChaosType.Discord;
            else
                proj.Chaos = ChaosType.SomethingWentWrongAndItsAllYourFault;

            if (typeof(T) == typeof(ShenDoragon_ChaosFireballFrag) || typeof(T) == typeof(ShenDoragon_ChaosFireballSpread))
                proj.IsSmall = true;
        }
    }
}
