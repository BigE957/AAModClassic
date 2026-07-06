using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class AncientGoldLeggingsEffect(bool isStripeman) : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<AncientGoldLeggingsPlayer>().effect = true;
            player.GetModPlayer<AncientGoldLeggingsPlayer>().isStripeman = isStripeman;
        }
    }

    public class AncientGoldLeggingsPlayer : EquipmentEffectPlayer
    {
        public bool isStripeman;

        public override void ResetEffects()
        {
            isStripeman = false;
        }
    }

    public class AncientGoldLeggingsNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.GetModPlayer<AncientGoldLeggingsPlayer>().effect && spawnInfo.Player.GetModPlayer<AncientGoldLeggingsPlayer>().isStripeman)
            {
                if (NPC.goldCritterChance >= 30)
                    NPC.goldCritterChance = 30;
                if (!spawnInfo.Player.calmed && !spawnInfo.Player.GetModPlayer<AAPlayer>().luckycalm)
                {
                    foreach (int npctype in AALuckyConfig.ListRareNpc)
                        if (pool.TryGetValue(npctype, out float value) && value <= 0.05f)
                            pool[npctype] = 0.05f;
                }
            }
            else if (spawnInfo.Player.GetModPlayer<AncientGoldLeggingsPlayer>().effect)
            {
                if (NPC.goldCritterChance >= 40)
                    NPC.goldCritterChance = 40;
            }
            else
            {
                NPC.goldCritterChance = 150; //TODO: is this the right value to reset it to? can we make it dynamically pull from vanillas base value?
            }
        }
    }
}