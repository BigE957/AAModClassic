using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfPassionDebuffEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<HeartOfPassionDebuffPlayer>().effect = true;
        }
    }

    public class HeartOfPassionDebuffPlayer : EquipmentEffectPlayer
    {
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (effect && (modifiers.DamageType == DamageClass.Magic || modifiers.DamageType == DamageClass.Summon))
            {
                if (Player.statLife > (Player.statLifeMax / 3))
                    target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 600);
                else if (Player.statLife < (Player.statLifeMax / 3))
                    target.AddBuff(BuffID.Daybreak, 600);
            }
        }
    }
}