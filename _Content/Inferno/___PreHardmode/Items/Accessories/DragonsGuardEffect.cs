using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories
{
    public class DragonsGuardEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<DragonsGuardPlayer>().effect = true;
        }
    }

    public class DragonsGuardPlayer : EquipmentEffectPlayer
    {
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (effect)
                npc.AddBuff(BuffID.OnFire, 120);
        }
    }
}
