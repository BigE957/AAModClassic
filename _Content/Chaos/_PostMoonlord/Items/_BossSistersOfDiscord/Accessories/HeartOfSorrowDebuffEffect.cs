using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfSorrowDebuffEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<HeartOfSorrowDebuffPlayer>().effect = true;
        }
    }

    public class HeartOfSorrowDebuffPlayer : EquipEffectAbstract
    {
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (effect && (modifiers.DamageType == DamageClass.Melee || modifiers.DamageType == DamageClass.Ranged))
            {
                if (Player.statLife > (Player.statLifeMax / 3))
                    target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 600);
                else if (Player.statLife < (Player.statLifeMax / 3))
                    target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
            }
        }
    }
}