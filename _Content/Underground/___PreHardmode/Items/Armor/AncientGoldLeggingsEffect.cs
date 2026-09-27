using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Armor
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
                if (!spawnInfo.Player.calmed && !spawnInfo.Player.GetModPlayer<ZAAPlayer>().luckycalm)
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