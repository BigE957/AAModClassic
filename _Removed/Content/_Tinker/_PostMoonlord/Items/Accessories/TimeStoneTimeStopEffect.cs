using AAModClassic;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Daybringer;
using AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    public class TimeStoneTimeStopEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<TimeStoneTimeStopPlayer>().effect = true;
        }
    }

    public class TimeStoneTimeStopPlayer : EquipmentEffectPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (effect)
            {
                if (AAMod.TimeStoneKey.JustPressed && !NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>()))
                {
                    AAWorld.TimeStopped = false;
                    
                    if (!Main.fastForwardTimeToDawn)
                    {
                        Main.fastForwardTimeToDawn = true;
                    }
                    else
                    {
                        Main.fastForwardTimeToDawn = false;
                    }
                }
            }
        }
    }
}